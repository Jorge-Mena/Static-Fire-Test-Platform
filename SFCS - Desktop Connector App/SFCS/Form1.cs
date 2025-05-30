using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SFCS
{
    public partial class MainWindow : Form
    {
        // Constants
        private const int TCP_RECEIVE_BUFFER_SIZE = 1024;
        private const int TCP_CONNECT_TIMEOUT = 3000;
        private const int THREAD_JOIN_TIMEOUT = 500;
        private const int MAX_DATA_POINTS = 1000;
        private const int MAX_BUFFER_SIZE = 65536;
        private const string CONFIG_FILE = "config.cfg";
        private const string LOG_FILE_PREFIX = "log_";

        private CancellationTokenSource tcpCancelToken;
        private int reconnectAttempts = 0;
        private const int MAX_RECONNECT_ATTEMPTS = 10;
        private const int RECONNECT_DELAY_MS = 2000;
        private volatile bool wantConnected = false;

        // Application state
        private bool isFullscreen = true;
        private bool alreadyConnected = false;
        private bool isConnected = false;
        private bool isRecording = false;
        private bool isSerialConnected = false;
        private readonly StringBuilder _serialBuffer = new StringBuilder();
        private readonly StringBuilder _tcpBuffer = new StringBuilder();
        private DateTime recordingStartTime;
        private string currentLogFile = "";
        private readonly object dataLock = new object();
        private readonly object connectionLock = new object();

        // Communication objects
        private SerialPort serialPort;
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        // Data structures
        private DataTable liveDataTable = new DataTable();

        // Graph data
        private Series temp1Series;
        private Series temp2Series;
        private Series massSeries;
        private double lastTemp1 = 0;
        private double lastTemp2 = 0;
        private double lastMass = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSerialPort();
            InitializeDataTable();
            InitializeCharts();
            RefreshAvailablePorts();
            LoadConfig();
            ToggleFullscreen(isFullscreen);

            if (cbBaudRate.Items.Contains("115200"))
                cbBaudRate.SelectedItem = "115200";

            lblRecordingStatus.Text = "Not recording";
            lblRecordingTime.Text = "00:00:00";
        }

        #region Data Structures
        public struct WifiConfig
        {
            public string Port;
            public string IpAddress;
            public string Ssid;
            public string Password;
            public string StartCommand;
            public string StopCommand;
            public string PortCommand;
            public string SsidCommand;
            public string PasswordCommand;
            public bool AutoSendEnabled;
            public bool ShowTimecodeSerial;
            public bool ShowTimecodeServer;
            public string LogMetadata;
        }

        public enum MessageTarget
        {
            SerialMonitor,
            ServerMonitor,
            ProgramMessages,
            LogFile
        }
        #endregion

        #region Initialization Methods
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort
            {
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 500,
                WriteTimeout = 500
            };
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.ErrorReceived += SerialPort_ErrorReceived;
        }

        private void InitializeDataTable()
        {
            liveDataTable.Columns.Add("Time", typeof(string));
            liveDataTable.Columns.Add("Temp1", typeof(double));
            liveDataTable.Columns.Add("Temp2", typeof(double));
            liveDataTable.Columns.Add("Mass", typeof(double));
            dgvLiveData.DataSource = liveDataTable;
        }

        private void InitializeCharts()
        {
            chartTemp1.Series.Clear();
            temp1Series = new Series("Temperature 1")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2
            };
            chartTemp1.Series.Add(temp1Series);
            chartTemp1.ChartAreas[0].AxisY.Title = "°C";
            chartTemp1.ChartAreas[0].AxisX.Title = "Time";

            chartTemp2.Series.Clear();
            temp2Series = new Series("Temperature 2")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2
            };
            chartTemp2.Series.Add(temp2Series);
            chartTemp2.ChartAreas[0].AxisY.Title = "°C";
            chartTemp2.ChartAreas[0].AxisX.Title = "Time";

            chartMass.Series.Clear();
            massSeries = new Series("Mass")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };
            chartMass.Series.Add(massSeries);
            chartMass.ChartAreas[0].AxisY.Title = "kg";
            chartMass.ChartAreas[0].AxisX.Title = "Time";
        }

        private void RefreshAvailablePorts()
        {
            cbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbPorts.Items.AddRange(ports);
            if (ports.Length > 0) cbPorts.SelectedIndex = 0;
        }

        private void ClearAllData()
        {
            lock (dataLock)
            {
                liveDataTable.Rows.Clear();
                temp1Series.Points.Clear();
                temp2Series.Points.Clear();
                massSeries.Points.Clear();
            }
        }

        private void clearAllTerminalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the content of all terminals
            rtbMonitorSerial.Clear();
            rtbMonitorServer.Clear();
            programMessages.Clear();
        }

        #endregion

        #region UI Methods
        private void WriteMessage(string content, MessageTarget target, bool includeTime, bool includeDirection, bool isIncoming)
        {
            string timestamp = includeTime ? $"[{DateTime.Now:HH:mm:ss.fff}] " : "";
            string direction = includeDirection ? (isIncoming ? "» " : "« ") : "";
            string fullMessage = $"{timestamp}{direction}{content}";

            if (InvokeRequired)
            {
                Invoke(new Action(() => WriteMessage(content, target, includeTime, includeDirection, isIncoming)));
                return;
            }

            switch (target)
            {
                case MessageTarget.SerialMonitor:
                    rtbMonitorSerial.AppendText(fullMessage + Environment.NewLine);
                    rtbMonitorSerial.ScrollToCaret();
                    break;
                case MessageTarget.ServerMonitor:
                    rtbMonitorServer.AppendText(fullMessage + Environment.NewLine);
                    rtbMonitorServer.ScrollToCaret();
                    break;
                case MessageTarget.ProgramMessages:
                    programMessages.AppendText(fullMessage + Environment.NewLine);
                    programMessages.ScrollToCaret();
                    break;
                case MessageTarget.LogFile:
                    if (isRecording && !string.IsNullOrEmpty(currentLogFile))
                    {
                        try
                        {
                            File.AppendAllText(currentLogFile, fullMessage + Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            WriteMessage($"Error writing to log file: {ex.Message}", MessageTarget.ProgramMessages, true, false, false);
                        }
                    }
                    break;
            }
        }

        private void ToggleFullscreen(bool fullscreen)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ToggleFullscreen(fullscreen)));
                return;
            }

            if (!fullscreen)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                StartPosition = FormStartPosition.WindowsDefaultLocation;
                Quit.Visible = false;
                toolStripSeparator4.Visible = false;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                StartPosition = FormStartPosition.CenterScreen;
                ShowInTaskbar = true;
                Quit.Visible = true;
                toolStripSeparator4.Visible = true;
            }
            isFullscreen = fullscreen;
        }

        private void SafeClose()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SafeClose));
                return;
            }

            if (isRecording)
            {
                if (MessageBox.Show("Are you sure you want to stop recording?", "Stop recording?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                StopRecording();
            }

            try
            {
                if (isConnected) Disconnect();
                if (isSerialConnected) serialPort.Close();
                SaveConfig();
                Close();
            }
            catch (Exception ex)
            {
                WriteMessage($"Error during close: {ex.Message}", MessageTarget.ProgramMessages, true, false, false);
            }
        }

        private void UpdateConnectionStatus(bool connected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateConnectionStatus(connected)));
                return;
            }

            isConnected = connected;
            btnConnectWiFi.Text = connected ? "Disconnect" : "Connect";
            txtIPAddress.Enabled = !connected;
            txtTcpPort.Enabled = !connected;
            lblConnectionStatus.Text = connected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = connected ? Color.Green : Color.Red;
        }

        private void UpdateSerialConnectionStatus(bool connected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateSerialConnectionStatus(connected)));
                return;
            }

            isSerialConnected = connected;
            btnConnectSerial.Text = connected ? "Disconnect" : "Connect";
            cbPorts.Enabled = !connected;
            cbBaudRate.Enabled = !connected;
        }

        private void UpdateRecordingTimer()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateRecordingTimer));
                return;
            }

            if (isRecording)
            {
                TimeSpan duration = DateTime.Now - recordingStartTime;
                lblRecordingTime.Text = duration.ToString(@"hh\:mm\:ss");
            }
        }
        #endregion

        #region Data Processing
        private void ProcessIncomingData(string data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data)) return;

                string[] parts = data.Split(new[] { ';', ';' }, StringSplitOptions.RemoveEmptyEntries);
                double temp1 = lastTemp1;
                double temp2 = lastTemp2;
                double mass = lastMass;
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

                foreach (string part in parts)
                {
                    string[] keyValue = part.Split(':');
                    if (keyValue.Length != 2) continue;

                    string key = keyValue[0].Trim().ToUpper();
                    string value = keyValue[1].Trim();

                    try
                    {
                        switch (key)
                        {
                            case "T1":
                                if (double.TryParse(value, out double t1))
                                {
                                    temp1 = t1;
                                    lastTemp1 = temp1;
                                }
                                break;
                            case "T2":
                                if (double.TryParse(value, out double t2))
                                {
                                    temp2 = t2;
                                    lastTemp2 = temp2;
                                }
                                break;
                            case "M":
                                if (double.TryParse(value, out double m))
                                {
                                    mass = m;
                                    lastMass = mass;
                                }
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        WriteMessage($"Failed to parse value '{value}' for key '{key}'",
                            MessageTarget.ServerMonitor, true, false, false);
                    }
                }

                UpdateDataTable(timestamp, temp1, temp2, mass);
                UpdateCharts(temp1, temp2, mass);

                if (isRecording)
                {
                    string logEntry = $"{timestamp};{temp1};{temp2};{mass}";
                    WriteMessage(logEntry, MessageTarget.LogFile, false, false, true);
                }
            }
            catch (Exception ex)
            {
                WriteMessage($"Data processing error: {ex.Message}",
                    MessageTarget.ServerMonitor, true, false, false);
            }
        }

        private void UpdateDataTable(string timestamp, double temp1, double temp2, double mass)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateDataTable(timestamp, temp1, temp2, mass)));
                return;
            }

            lock (dataLock)
            {
                if (liveDataTable.Rows.Count > MAX_DATA_POINTS)
                {
                    liveDataTable.Rows.RemoveAt(0);
                }

                liveDataTable.Rows.Add(timestamp, temp1, temp2, mass);

                if (dgvLiveData.RowCount > 0)
                    dgvLiveData.FirstDisplayedScrollingRowIndex = dgvLiveData.RowCount - 1;
            }
        }

        private void UpdateCharts(double temp1, double temp2, double mass)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCharts(temp1, temp2, mass)));
                return;
            }

            DateTime now = DateTime.Now;

            if (temp1Series.Points.Count > MAX_DATA_POINTS)
                temp1Series.Points.RemoveAt(0);
            temp1Series.Points.AddXY(now, temp1);

            if (temp2Series.Points.Count > MAX_DATA_POINTS)
                temp2Series.Points.RemoveAt(0);
            temp2Series.Points.AddXY(now, temp2);

            if (massSeries.Points.Count > MAX_DATA_POINTS)
                massSeries.Points.RemoveAt(0);
            massSeries.Points.AddXY(now, mass);
        }
        #endregion

        #region Serial Communication
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadExisting();
                _serialBuffer.Append(data);

                ProcessBuffer(_serialBuffer, (line) =>
                {
                    WriteMessage(line, MessageTarget.SerialMonitor,
                        cbTimecodeSerial.Checked, true, true);
                    Auto_Connect(line);
                    ProcessIncomingData(line);
                });
            }
            catch (Exception ex)
            {
                WriteMessage($"Serial read error: {ex.Message}",
                    MessageTarget.SerialMonitor, true, false, false);
            }
        }

        private void ProcessBuffer(StringBuilder buffer, Action<string> lineProcessor)
        {
            string bufferContent = buffer.ToString();
            int lastNewLine = bufferContent.LastIndexOf('\n');

            if (buffer.Length > MAX_BUFFER_SIZE)
            {
                buffer.Remove(0, buffer.Length - MAX_BUFFER_SIZE);
                WriteMessage("Buffer overflow detected, truncated buffer",
                    MessageTarget.SerialMonitor, true, false, false);
            }

            if (lastNewLine >= 0)
            {
                string completeMessages = bufferContent.Substring(0, lastNewLine + 1);
                buffer.Remove(0, lastNewLine + 1);

                string[] lines = completeMessages.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                Invoke(new Action(() =>
                {
                    foreach (string line in lines)
                    {
                        string trimmedLine = line.Trim('\r');
                        if (!string.IsNullOrEmpty(trimmedLine))
                        {
                            lineProcessor(trimmedLine);
                        }
                    }
                }));
            }
        }

        private void Auto_Connect(string message)
        {
            if (!cbAutoSendWifi.Checked) return;

            WifiConfig wifiTCP = new WifiConfig
            {
                Port = txtTcpPort.Text,
                Ssid = txtSSID.Text,
                Password = txtPassword.Text,
                PortCommand = txtPortCmd.Text,
                SsidCommand = txtSSIDcmd.Text,
                PasswordCommand = txtPSWcmd.Text
            };

            if (message.Trim().Equals(txtPortCmd.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                serialPort.WriteLine(wifiTCP.Port);
            }
            else if (message.Trim().Equals(txtSSIDcmd.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                serialPort.WriteLine(wifiTCP.Ssid);
            }
            else if (message.Trim().Equals(txtPSWcmd.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                serialPort.WriteLine(wifiTCP.Password);
            }
            else if (message.Trim().StartsWith("IP", StringComparison.OrdinalIgnoreCase))
            {
                string ip = message.Substring(2).Trim();
                txtIPAddress.Text = ip;
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            WriteMessage($"Serial port error: {e.EventType}",
                MessageTarget.SerialMonitor, true, false, false);
        }

        private void btnConnectSerial_Click(object sender, EventArgs e)
        {
            if (serialPort == null) InitializeSerialPort();

            if (!serialPort.IsOpen)
            {
                try
                {
                    if (cbPorts.SelectedItem == null || string.IsNullOrWhiteSpace(cbBaudRate.Text))
                    {
                        MessageBox.Show("Please select both port and baud rate.");
                        return;
                    }

                    serialPort.PortName = cbPorts.SelectedItem.ToString();
                    serialPort.BaudRate = int.Parse(cbBaudRate.Text);
                    serialPort.Open();

                    UpdateSerialConnectionStatus(true);
                    WriteMessage("Serial Port Connected",
                        MessageTarget.ProgramMessages, true, false, false);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access to the port is denied. The port may already be in use.", "Connection Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening port: {ex.Message}", "Connection Error");
                }
            }
            else
            {
                try
                {
                    serialPort.Close();
                    UpdateSerialConnectionStatus(false);
                    WriteMessage("Serial Port Disconnected",
                        MessageTarget.ProgramMessages, true, false, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error closing port: {ex.Message}", "Connection Error");
                }
            }
        }

        private void btnSendSerial_Click(object sender, EventArgs e)
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                MessageBox.Show("Please connect to a serial port first.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSendSerial.Text))
            {
                MessageBox.Show("Please enter text to send.", "Error");
                return;
            }

            try
            {
                string dataToSend = txtSendSerial.Text.Trim();
                serialPort.WriteLine(dataToSend);
                WriteMessage(dataToSend, MessageTarget.SerialMonitor,
                    cbTimecodeSerial.Checked, true, false);
                txtSendSerial.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}", "Error");
            }
        }
        #endregion

        #region TCP Communication

        private async Task<bool> IsPortOpen(string host, int port, int timeout = 2000)
        {
            try
            {
                var client = new TcpClient();
                var connectTask = client.ConnectAsync(host, port);
                var completed = await Task.WhenAny(connectTask, Task.Delay(timeout));
                return client.Connected && completed == connectTask;
            }
            catch { return false; }
        }

        private async Task Connect()
        {
            wantConnected = true;
            reconnectAttempts = 0;
            await ConnectOrReconnect();
        }

        private async Task ConnectOrReconnect()
        {
            while (wantConnected)
            {
                lock (connectionLock)
                {
                    if (isConnected)
                        return;
                }
                try
                {
                    UpdateUIConnectingState();

                    var newClient = new TcpClient();
                    var connectTask = newClient.ConnectAsync(txtIPAddress.Text, int.Parse(txtTcpPort.Text));
                    var timeoutTask = Task.Delay(TCP_CONNECT_TIMEOUT);
                    var completed = await Task.WhenAny(connectTask, timeoutTask);

                    if (completed == timeoutTask || !newClient.Connected)
                        throw new TimeoutException("Connection timed out or failed.");

                    lock (connectionLock)
                    {
                        client = newClient;
                        stream = client.GetStream();
                        isConnected = true;
                        tcpCancelToken = new CancellationTokenSource();
                    }

                    UpdateConnectionStatus(true);
                    WriteMessage($"Connected to {txtIPAddress.Text}:{txtTcpPort.Text}",
                        MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);

                    StartReceiveLoop(tcpCancelToken.Token);
                    break;
                }
                catch (Exception ex)
                {
                    SafeDisconnect();
                    reconnectAttempts++;
                    if (reconnectAttempts >= MAX_RECONNECT_ATTEMPTS)
                    {
                        WriteMessage($"Auto-reconnect failed after {reconnectAttempts} tries.", MessageTarget.ServerMonitor, true, false, false);
                        break;
                    }
                    WriteMessage($"TCP connect failed: {ex?.Message ?? "Unknown"}. Retrying in {RECONNECT_DELAY_MS / 1000}s...", MessageTarget.ServerMonitor, true, false, false);
                    await Task.Delay(RECONNECT_DELAY_MS);
                }
            }
        }

        private void Disconnect()
        {
            wantConnected = false;
            lock (connectionLock)
            {
                isConnected = false;
                try
                {
                    tcpCancelToken?.Cancel();
                }
                catch { }
                try { stream?.Dispose(); } catch { }
                try { client?.Dispose(); } catch { }
                stream = null;
                client = null;
                tcpCancelToken = null;
            }
            UpdateConnectionStatus(false);
            WriteMessage("Disconnected", MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
        }

        private void SafeDisconnect()
        {
            lock (connectionLock)
            {
                isConnected = false;
                try { tcpCancelToken?.Cancel(); } catch { }
                try { stream?.Dispose(); } catch { }
                try { client?.Dispose(); } catch { }
                stream = null;
                client = null;
                tcpCancelToken = null;
            }
            UpdateConnectionStatus(false);
        }

        /// <summary>
        /// Starts the receive loop in a background thread. Handles connection loss and triggers reconnect.
        /// </summary>
        private void StartReceiveLoop(CancellationToken cancelToken)
        {
            Task.Run(async () =>
            {
                byte[] buffer = new byte[TCP_RECEIVE_BUFFER_SIZE];
                try
                {
                    while (!cancelToken.IsCancellationRequested && wantConnected)
                    {
                        lock (connectionLock)
                        {
                            if (!isConnected || stream == null || client == null || !client.Connected)
                                throw new IOException("TCP connection lost");
                        }

                        if (stream.DataAvailable)
                        {
                            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancelToken);
                            if (bytesRead > 0)
                            {
                                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                                ProcessReceivedData(receivedData);
                            }
                        }
                        else
                        {
                            await Task.Delay(50, cancelToken);
                        }
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    WriteMessage($"TCP receive error: {ex.Message}", MessageTarget.ServerMonitor, true, false, false);
                    lock (connectionLock)
                    {
                        isConnected = false;
                    }
                    UpdateConnectionStatus(false);
                    WriteMessage("Connection lost, attempting auto-reconnect...", MessageTarget.ServerMonitor, true, false, false);
                    await ConnectOrReconnect();
                }
            }, cancelToken);
        }

        private void ProcessReceivedData(string data)
        {
            _tcpBuffer.Append(data);

            if (_tcpBuffer.Length > MAX_BUFFER_SIZE)
            {
                _tcpBuffer.Remove(0, _tcpBuffer.Length - MAX_BUFFER_SIZE);
                WriteMessage("TCP buffer overflow, truncated", MessageTarget.ServerMonitor, true, false, false);
            }

            ProcessBuffer(_tcpBuffer, line =>
            {
                WriteMessage(line, MessageTarget.ServerMonitor, cbTimecodeServer.Checked, true, true);
                ProcessIncomingData(line);
            });
        }

        private async void btnConnectWiFi_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                await Connect();
            }
        }

        private void btnSendTCP_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected || stream == null)
            {
                MessageBox.Show("Please connect to the TCP server first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSendTCP.Text))
            {
                MessageBox.Show("Please enter text to send.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string dataToSend = txtSendTCP.Text.Trim();
                byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend + "\n");

                lock (connectionLock)
                {
                    if (stream != null && stream.CanWrite)
                    {
                        stream.Write(dataBytes, 0, dataBytes.Length);
                        stream.Flush();
                    }
                }

                WriteMessage(dataToSend, MessageTarget.ServerMonitor, cbTimecodeServer.Checked, true, false);
                txtSendTCP.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (client == null || !client.Connected)
                    Task.Run(() => ConnectOrReconnect());
            }
        }

        #endregion

        #region Recording and File Operations
        private void StartRecording()
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                currentLogFile = $"{LOG_FILE_PREFIX}{timestamp}.csv";
                string header = "Timestamp;Temperature1;Temperature2;Mass";
                File.WriteAllText(currentLogFile, header + Environment.NewLine);

                if (!string.IsNullOrEmpty(txtLogMetadata.Text))
                    File.AppendAllText(currentLogFile, $"# {txtLogMetadata.Text}" + Environment.NewLine);

                recordingStartTime = DateTime.Now;

                isRecording = true;
                btnRecord.Image = Properties.Resources.Recording;
                lblRecordingStatus.Text = "Recording";
                lblRecordingStatus.ForeColor = Color.Red;

                recordingTimer.Start();
                WriteMessage("Recording started", MessageTarget.ProgramMessages, true, false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting recording: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopRecording()
        {
            try
            {
                if (!string.IsNullOrEmpty(currentLogFile))
                {
                    isRecording = false;
                    btnRecord.Image = Properties.Resources.notRecording;
                    lblRecordingStatus.Text = "Not recording";
                    lblRecordingStatus.ForeColor = Color.Black;

                    recordingTimer.Stop();
                    WriteMessage("Recording stopped", MessageTarget.ProgramMessages, true, false, false);

                    TimeSpan duration = DateTime.Now - recordingStartTime;
                    File.AppendAllText(currentLogFile, $"# Recording duration: {duration.TotalSeconds} seconds" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Temp1: {lastTemp1}°C" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Temp2: {lastTemp2}°C" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Mass: {lastMass}kg" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                WriteMessage($"Error finalizing log file: {ex.Message}",
                    MessageTarget.ServerMonitor, true, false, false);
            }
            currentLogFile = "";
        }

        private void SaveConfig()
        {
            try
            {
                WifiConfig config = new WifiConfig
                {
                    IpAddress = txtIPAddress.Text,
                    Port = txtTcpPort.Text,
                    Ssid = txtSSID.Text,
                    Password = txtPassword.Text,
                    StartCommand = txtStartCmdWifi.Text,
                    StopCommand = txtStopCmdWifi.Text,
                    PortCommand = txtPortCmd.Text,
                    SsidCommand = txtSSIDcmd.Text,
                    PasswordCommand = txtPSWcmd.Text,
                    AutoSendEnabled = cbAutoSendWifi.Checked,
                    ShowTimecodeSerial = cbTimecodeSerial.Checked,
                    ShowTimecodeServer = cbTimecodeServer.Checked,
                    LogMetadata = txtLogMetadata.Text
                };

                string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, configJson);

                WriteMessage("Configuration saved", MessageTarget.ProgramMessages, true, false, false);
            }
            catch (Exception ex)
            {
                WriteMessage($"Configuration save error: {ex.Message}",
                    MessageTarget.ProgramMessages, true, false, false);
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    string configJson = File.ReadAllText(CONFIG_FILE);
                    WifiConfig config = JsonConvert.DeserializeObject<WifiConfig>(configJson);

                    txtIPAddress.Text = config.IpAddress;
                    txtTcpPort.Text = config.Port;
                    txtSSID.Text = config.Ssid;
                    txtPassword.Text = config.Password;
                    txtStartCmdWifi.Text = config.StartCommand;
                    txtStopCmdWifi.Text = config.StopCommand;
                    txtPortCmd.Text = config.PortCommand;
                    txtSSIDcmd.Text = config.SsidCommand;
                    txtPSWcmd.Text = config.PasswordCommand;
                    cbAutoSendWifi.Checked = config.AutoSendEnabled;
                    cbTimecodeSerial.Checked = config.ShowTimecodeSerial;
                    cbTimecodeServer.Checked = config.ShowTimecodeServer;
                    txtLogMetadata.Text = config.LogMetadata;

                    WriteMessage("Configuration loaded", MessageTarget.ProgramMessages, true, false, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show Config clicked!", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportData(string format)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                switch (format)
                {
                    case "CSV":
                        saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                        saveFileDialog.FileName = $"export_{timestamp}.csv";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StringBuilder csvContent = new StringBuilder();
                            csvContent.AppendLine("Timestamp,Temperature1,Temperature2,Mass");
                            foreach (DataRow row in liveDataTable.Rows)
                                csvContent.AppendLine($"{row["Time"]},{row["Temp1"]},{row["Temp2"]},{row["Mass"]}");
                            File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());
                        }
                        break;

                    case "TXT":
                        saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                        saveFileDialog.FileName = $"export_{timestamp}.txt";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StringBuilder txtContent = new StringBuilder();
                            txtContent.AppendLine("ESP32 Data Logger Export");
                            txtContent.AppendLine($"Exported: {timestamp}");
                            txtContent.AppendLine("---------------------------------");
                            txtContent.AppendLine("Time\tTemp1\tTemp2\tMass");
                            foreach (DataRow row in liveDataTable.Rows)
                                txtContent.AppendLine($"{row["Time"]}\t{row["Temp1"]}\t{row["Temp2"]}\t{row["Mass"]}");
                            File.WriteAllText(saveFileDialog.FileName, txtContent.ToString());
                        }
                        break;

                    case "RAW":
                        saveFileDialog.Filter = "Raw data files (*.raw)|*.raw";
                        saveFileDialog.FileName = $"export_{timestamp}.raw";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StringBuilder rawContent = new StringBuilder();
                            rawContent.AppendLine("Serial Monitor:");
                            rawContent.AppendLine(rtbMonitorSerial.Text);
                            rawContent.AppendLine("\nServer Monitor:");
                            rawContent.AppendLine(rtbMonitorServer.Text);
                            File.WriteAllText(saveFileDialog.FileName, rawContent.ToString());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Event Handlers
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected || stream == null)
            {
                MessageBox.Show("Please connect to the TCP server first.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStartCmdWifi.Text))
            {
                MessageBox.Show("Start command is empty.", "Error");
                return;
            }

            try
            {
                string command = txtStartCmdWifi.Text.Trim();
                byte[] dataBytes = Encoding.ASCII.GetBytes(command + "\n");
                stream.Write(dataBytes, 0, dataBytes.Length);
                stream.Flush();

                WriteMessage(command, MessageTarget.ServerMonitor,
                    cbTimecodeServer.Checked, true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending start command: {ex.Message}", "Error");
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected || stream == null)
            {
                MessageBox.Show("Please connect to the TCP server first.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStopCmdWifi.Text))
            {
                MessageBox.Show("Stop command is empty.", "Error");
                return;
            }

            try
            {
                string command = txtStopCmdWifi.Text.Trim();
                byte[] dataBytes = Encoding.ASCII.GetBytes(command + "\n");
                stream.Write(dataBytes, 0, dataBytes.Length);
                stream.Flush();

                WriteMessage(command, MessageTarget.ServerMonitor,
                    cbTimecodeServer.Checked, true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending stop command: {ex.Message}", "Error");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData("CSV");
        }

        private void ExportAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData("CSV");
        }

        private void ExportTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData("TXT");
        }

        private void ExportRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData("RAW");
        }

        private void ExportConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show("Configuration saved successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Config Files|*.cfg";
            openFile.Title = "Import Configuration";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string configJson = File.ReadAllText(openFile.FileName);
                    WifiConfig config = JsonConvert.DeserializeObject<WifiConfig>(configJson);

                    txtIPAddress.Text = config.IpAddress;
                    txtTcpPort.Text = config.Port;
                    txtSSID.Text = config.Ssid;
                    txtPassword.Text = config.Password;
                    txtStartCmdWifi.Text = config.StartCommand;
                    txtStopCmdWifi.Text = config.StopCommand;
                    txtPortCmd.Text = config.PortCommand;
                    txtSSIDcmd.Text = config.SsidCommand;
                    txtPSWcmd.Text = config.PasswordCommand;
                    cbAutoSendWifi.Checked = config.AutoSendEnabled;
                    cbTimecodeSerial.Checked = config.ShowTimecodeSerial;
                    cbTimecodeServer.Checked = config.ShowTimecodeServer;
                    txtLogMetadata.Text = config.LogMetadata;

                    WriteMessage("Configuration imported successfully",
                        MessageTarget.ProgramMessages, true, false, false);
                    MessageBox.Show("Configuration imported successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing configuration: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshAvailablePorts();
            WriteMessage("Serial ports refreshed",
                MessageTarget.ProgramMessages, true, false, false);
        }

        private void btnClearSerial_Click(object sender, EventArgs e)
        {
            rtbMonitorSerial.Clear();
        }

        private void btnClearServer_Click(object sender, EventArgs e)
        {
            rtbMonitorServer.Clear();
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            ClearAllData();
            WriteMessage("All data cleared",
                MessageTarget.ProgramMessages, true, false, false);
        }

        private void btnFullscreen_Click(object sender, EventArgs e)
        {
            ToggleFullscreen(!isFullscreen);
        }

        private void recordingTimer_Tick(object sender, EventArgs e)
        {
            UpdateRecordingTimer();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SafeClose();
        }

        private void UpdateUIConnectingState()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIConnectingState));
                return;
            }

            btnConnectWiFi.Enabled = false;
            lblConnectionStatus.Text = "Connecting...";
            lblConnectionStatus.ForeColor = Color.Orange;
        }

        private void txtSendSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && serialPort != null && serialPort.IsOpen)
            {
                btnSendSerial_Click(sender, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void txtSendTCP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && client != null && client.Connected)
            {
                btnSendTCP_Click(sender, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            SafeClose();
        }

        private void tsbFullscren_Click(object sender, EventArgs e)
        {
            ToggleFullscreen(!isFullscreen);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SFCS - Serial and TCP Data Logger\nVersion 1.0\n\nA tool for monitoring and logging data from serial and TCP connections.",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButtonR_Click(object sender, EventArgs e)
        {
            RefreshAvailablePorts();
        }

        private void cbViewPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cbViewPassword.Checked ? '\0' : '*';
        }

        private void cbAutoSendWifi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoSendWifi.Checked)
            {
                WriteMessage("Auto-send WiFi credentials enabled.", MessageTarget.ProgramMessages, true, false, false);
                txtPortCmd.Enabled = true;
                txtSSIDcmd.Enabled = true;
                txtPSWcmd.Enabled = true;
            }
            else
            {
                WriteMessage("Auto-send WiFi credentials disabled.", MessageTarget.ProgramMessages, true, false, false);
                txtPortCmd.Enabled = false;
                txtSSIDcmd.Enabled = false;
                txtPSWcmd.Enabled = false;
            }
        }
        #endregion
    }
}