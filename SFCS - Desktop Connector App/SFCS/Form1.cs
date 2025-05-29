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
        private const int TCP_RECEIVE_BUFFER_SIZE = 1024;
        private const int TCP_CONNECT_TIMEOUT = 3000; // 3 seconds
        private const int THREAD_JOIN_TIMEOUT = 500; // 500ms
        private const int MAX_DATA_POINTS = 1000;
        private const int MAX_BUFFER_SIZE = 65536; // 64KB
        private const string CONFIG_FILE = "config.cfg";
        private const string LOG_FILE_PREFIX = "log_";

        // Application state
        private bool isFullscreen = false;
        private bool isConnected = false;
        private bool isRecording = false;
        private bool isSerialConnected = false;
        private readonly StringBuilder _serialBuffer = new StringBuilder();
        private readonly StringBuilder _tcpBuffer = new StringBuilder();
        private DateTime recordingStartTime;
        private string currentLogFile = "";
        private readonly object dataLock = new object();

        // Communication objects
        private SerialPort serialPort;
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        // Data structures
        private DataTable liveDataTable = new DataTable();
        private readonly object connectionLock = new object();

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
            toggleFullscreen(isFullscreen);

            // Set default baud rate
            cbBaudRate.SelectedItem = "115200";

            // Initialize UI elements
            lblRecordingStatus.Text = "Not recording";
            lblRecordingTime.Text = "00:00:00";

            // Wire up event handlers
            btnConnectWiFi.Click += btnConnectWiFi_Click;
            btnSendTCP.Click += btnSendTCP_Click;
            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            btnRecord.Click += btnRecord_Click;
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            exportAsCSVToolStripMenuItem.Click += exportAsCSVToolStripMenuItem_Click;
            exportTXTToolStripMenuItem.Click += exportTXTToolStripMenuItem_Click;
            exportRawToolStripMenuItem.Click += exportRawToolStripMenuItem_Click;
            exportConfigToolStripMenuItem.Click += exportConfigToolStripMenuItem_Click;
            importConfigToolStripMenuItem.Click += importConfigToolStripMenuItem_Click;
            showConfigToolStripMenuItem.Click += showConfigToolStripMenuItem_Click;
            clearDataToolStripMenuItem.Click += clearDataToolStripMe_Click;
            clearSerialTerminalToolStripMenuItem.Click += btnClearSerial_Click;
            clearTCPTerminalToolStripMenuItem.Click += btnClearServer_Click;
            btnConnectSerial.Click += btnConnectSerial_Click;
            btnSendSerial.Click += btnSendSerial_Click;
            toolStripButtonR.Click += toolStripButtonR_Click;
            clearAllTerminalsToolStripMenuItem.Click += clearAllTerminalsToolStripMenuItem_Click;
            Quit.Click += Quit_Click;
            cbViewPassword.CheckedChanged += cbViewPassword_CheckedChanged;
            tsbFullscren.Click += tsbFullscren_Click;
            cbAutoSendWifi.CheckedChanged += cbAutoSendWifi_CheckedChanged;

            // Start timer for recording time updates
            recordingTimer.Interval = 1000;
            recordingTimer.Tick += RecordingTimer_Tick;
            recordingTimer.Start();
        }

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
            SerialLog,
            ServerMonitor,
            ServerLog,
            LogFile
        }

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
            // Setup Temp1 Chart
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
            chartTemp1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            // Setup Temp2 Chart
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
            chartTemp2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            // Setup Mass Chart
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
            chartMass.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        }

        private void Auto_Connect(string message)
        {
            WifiConfig wifiTCP = new WifiConfig
            {
                Port = txtTcpPort.Text,
                Ssid = txtSSID.Text,
                Password = txtPassword.Text,
                PortCommand = txtPortCmd.Text,
                SsidCommand = txtSSIDcmd.Text,
                PasswordCommand = txtPSWcmd.Text
            };

            if (cbAutoSendWifi.Checked)
            {
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
            }

            if (message.Trim().StartsWith("IP", StringComparison.OrdinalIgnoreCase))
            {
                wifiTCP.IpAddress = message.Remove(0, 3);
                if (InvokeRequired)
                {
                    Invoke(new Action(() => txtIPAddress.Text = wifiTCP.IpAddress));
                }
                else
                {
                    txtIPAddress.Text = wifiTCP.IpAddress;
                }
            }
        }

        private void RefreshAvailablePorts()
        {
            cbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbPorts.Items.AddRange(ports);
            if (ports.Length > 0) cbPorts.SelectedIndex = 0;
        }

        private void clearDataToolStripMe_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnClearSerial_Click(object sender, EventArgs e)
        {
            rtbMonitorSerial.Clear();
        }

        private void btnClearServer_Click(object sender, EventArgs e)
        {
            rtbMonitorServer.Clear();
        }

        private void RecordingTimer_Tick(object sender, EventArgs e)
        {
            if (isRecording)
            {
                TimeSpan duration = DateTime.Now - recordingStartTime;
                lblRecordingTime.Text = duration.ToString(@"hh\:mm\:ss");
            }
            else
            {
                lblRecordingTime.Text = "00:00:00";
            }
        }

        #endregion

        #region UI Methods

        private void UpdateUIConnectingState()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIConnectingState));
                return;
            }

            btnConnectWiFi.Enabled = false;
            btnConnectWiFi.Text = "Connecting...";
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

        private void WriteMessage(string content, MessageTarget target, bool includeTime, bool includeDirection, bool isIncoming)
        {
            string timestamp = includeTime ? $"[{DateTime.Now:HH:mm:ss:FFFF}] " : "";
            string direction = includeDirection ? isIncoming ? "» " : "« " : "";
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
                case MessageTarget.SerialLog:
                    rtbLogSerial.AppendText(fullMessage + Environment.NewLine);
                    rtbLogSerial.ScrollToCaret();
                    break;
                case MessageTarget.ServerLog:
                    rtbLogServer.AppendText(fullMessage + Environment.NewLine);
                    rtbLogServer.ScrollToCaret();
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
                            WriteMessage($"Error writing to log file: {ex.Message}", MessageTarget.ServerMonitor, true, false, false);
                        }
                    }
                    break;
            }
        }

        private void toggleFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                ShowInTaskbar = true;
                Quit.Visible = true;
                toolStripSeparator4.Visible = true;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                Quit.Visible = false;
                toolStripSeparator4.Visible = false;
            }
            isFullscreen = fullscreen;
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

        #endregion

        #region Data Processing

        private void ProcessIncomingData(string data)
        {
            try
            {
                // Skip empty or whitespace data
                if (string.IsNullOrWhiteSpace(data)) return;

                // Example data format: "T1:25.5,T2:26.7,M:12.3"
                string[] parts = data.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                double temp1 = lastTemp1; // Keep last known values if not updated
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

                // Update UI with new data
                UpdateDataTable(timestamp, temp1, temp2, mass);
                UpdateCharts(temp1, temp2, mass);

                // Write to log if recording
                if (isRecording)
                {
                    string logEntry = $"{timestamp},{temp1},{temp2},{mass}";
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
                // Limit the number of rows to prevent memory issues
                if (liveDataTable.Rows.Count > MAX_DATA_POINTS)
                {
                    liveDataTable.Rows.RemoveAt(0);
                }

                liveDataTable.Rows.Add(timestamp, temp1, temp2, mass);
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

            // Update Temp1 chart
            if (temp1Series.Points.Count > MAX_DATA_POINTS)
            {
                temp1Series.Points.RemoveAt(0);
            }
            temp1Series.Points.AddXY(now, temp1);

            // Update Temp2 chart
            if (temp2Series.Points.Count > MAX_DATA_POINTS)
            {
                temp2Series.Points.RemoveAt(0);
            }
            temp2Series.Points.AddXY(now, temp2);

            // Update Mass chart
            if (massSeries.Points.Count > MAX_DATA_POINTS)
            {
                massSeries.Points.RemoveAt(0);
            }
            massSeries.Points.AddXY(now, mass);
        }

        #endregion

        #region Serial Communication

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadExisting();
            _serialBuffer.Append(data);

            ProcessBuffer(_serialBuffer, (line) =>
            {
                WriteMessage(line, MessageTarget.SerialMonitor, cbTimecodeSerial.Checked, true, true);
                WriteMessage(line, MessageTarget.SerialLog, false, false, true);
                Auto_Connect(line);
                ProcessIncomingData(line);
            });
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

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SerialPort_ErrorReceived(sender, e)));
                return;
            }
            MessageBox.Show($"Serial port error: {e.EventType}", "Error");
        }

        private void btnConnectSerial_Click(object sender, EventArgs e)
        {
            if (serialPort == null) InitializeSerialPort();

            if (!serialPort.IsOpen)
            {
                try
                {
                    if (cbPorts.SelectedItem == null || cbBaudRate.Text == null)
                    {
                        MessageBox.Show("Please select both port and baud rate.");
                        return;
                    }

                    serialPort.PortName = cbPorts.SelectedItem.ToString();
                    serialPort.BaudRate = int.Parse(cbBaudRate.Text);
                    serialPort.Open();
                    UpdateSerialConnectionStatus(true);
                    WriteMessage("Serial Port Connected", MessageTarget.SerialLog, true, false, false);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access to the port is denied. The port may already be in use.", "Connection Error");
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Invalid baud rate selected.", "Connection Error");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Invalid port name selected.", "Connection Error");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"I/O error: {ex.Message}", "Connection Error");
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
                    WriteMessage("Serial Port Disconnected", MessageTarget.SerialLog, true, false, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error closing port: {ex.Message}", "Connection Error");
                }
            }
        }

        #endregion

        #region TCP Communication

        private async Task<bool> IsPortOpen(string host, int port, int timeout = 2000)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(host, port);
                    var timeoutTask = Task.Delay(timeout);

                    var completedTask = await Task.WhenAny(connectTask, timeoutTask);
                    if (completedTask == timeoutTask)
                    {
                        return false;
                    }

                    return client.Connected;
                }
            }
            catch
            {
                return false;
            }
        }

        private async void Connect()
        {
            if (string.IsNullOrWhiteSpace(txtIPAddress.Text) || string.IsNullOrWhiteSpace(txtTcpPort.Text))
            {
                MessageBox.Show("Please enter valid IP and Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lock (connectionLock)
            {
                if (isConnected)
                {
                    Disconnect();
                    return;
                }

                try
                {
                    UpdateUIConnectingState();

                    // First check if port is open (without ping which might be blocked)
                    bool portOpen = false;
                    var checkTask = Task.Run(async () =>
                    {
                        portOpen = await IsPortOpen(txtIPAddress.Text, int.Parse(txtTcpPort.Text));
                    });

                    if (!checkTask.Wait(2000)) // Wait max 2 seconds for port check
                    {
                        throw new Exception("Port check timed out");
                    }

                    if (!portOpen)
                    {
                        throw new Exception($"Port {txtTcpPort.Text} is not responding");
                    }

                    // Create new client with optimized settings
                    client = new TcpClient
                    {
                        LingerState = new LingerOption(true, 1),
                        NoDelay = true,
                        ReceiveTimeout = 5000,
                        SendTimeout = 5000
                    };

                    // Use async connect with cancellation token
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(TCP_CONNECT_TIMEOUT);

                    try
                    {
                        await client.ConnectAsync(txtIPAddress.Text, int.Parse(txtTcpPort.Text)).WithCancellation(cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        throw new TimeoutException($"Connection timed out after {TCP_CONNECT_TIMEOUT}ms");
                    }

                    if (!client.Connected)
                    {
                        throw new Exception("Connection failed");
                    }

                    // Connection successful
                    stream = client.GetStream();
                    isConnected = true;

                    // Start receive thread if not already running
                    if (receiveThread == null || !receiveThread.IsAlive)
                    {
                        receiveThread = new Thread(TcpServer_ReceiveData)
                        {
                            IsBackground = true,
                            Priority = ThreadPriority.AboveNormal
                        };
                        receiveThread.Start();
                    }

                    UpdateConnectionStatus(true);
                    WriteMessage($"Connected to {txtIPAddress.Text}:{txtTcpPort.Text}",
                        MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                }
                catch (Exception ex)
                {
                    SafeDisconnect();

                    // Enhanced error reporting
                    string errorDetails = $"Error connecting to {txtIPAddress.Text}:{txtTcpPort.Text}\n" +
                                        $"Exception: {ex.Message}\n" +
                                        $"Stack Trace: {ex.StackTrace}";

                    WriteMessage(errorDetails, MessageTarget.ServerMonitor, true, false, false);

                    // Additional diagnostics
                    try
                    {
                        var ipAddresses = Dns.GetHostAddresses(txtIPAddress.Text);
                        WriteMessage($"DNS resolution: {(ipAddresses.Length > 0 ? "Success" : "Failed")}",
                            MessageTarget.ServerMonitor, false, false, false);
                    }
                    catch (Exception dnsEx)
                    {
                        WriteMessage($"DNS error: {dnsEx.Message}",
                            MessageTarget.ServerMonitor, false, false, false);
                    }
                }
                finally
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => btnConnectWiFi.Enabled = true));
                    }
                    else
                    {
                        btnConnectWiFi.Enabled = true;
                    }
                }
            }
        }

        private void Disconnect()
        {
            lock (connectionLock)
            {
                try
                {
                    isConnected = false;

                    // Stop receive thread
                    if (receiveThread != null && receiveThread.IsAlive)
                    {
                        var cts = new CancellationTokenSource();
                        cts.CancelAfter(THREAD_JOIN_TIMEOUT);
                        try
                        {
                            receiveThread.Join(THREAD_JOIN_TIMEOUT);
                        }
                        catch (ThreadAbortException)
                        {
                            Thread.ResetAbort();
                        }
                        receiveThread = null;
                    }

                    // Close stream
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                        stream = null;
                    }

                    // Close client
                    if (client != null)
                    {
                        if (client.Connected)
                        {
                            client.Close();
                        }
                        client.Dispose();
                        client = null;
                    }

                    UpdateConnectionStatus(false);
                    WriteMessage("Disconnected", MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                }
                catch (Exception ex)
                {
                    WriteMessage($"Disconnection error: {ex.Message}",
                        MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                }
            }
        }

        private void SafeDisconnect()
        {
            try
            {
                stream?.Dispose();
                client?.Dispose();
            }
            catch (Exception ex)
            {
                WriteMessage($"Dispose error: {ex.Message}",
                    MessageTarget.ServerMonitor, true, false, false);
            }
            finally
            {
                stream = null;
                client = null;
            }
        }

        private void TcpServer_ReceiveData()
        {
            byte[] buffer = new byte[TCP_RECEIVE_BUFFER_SIZE];

            try
            {
                while (isConnected && client != null && client.Connected)
                {
                    try
                    {
                        if (stream != null && stream.DataAvailable)
                        {
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead > 0)
                            {
                                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                                _tcpBuffer.Append(receivedData);

                                // Process buffer with size limit check
                                if (_tcpBuffer.Length > MAX_BUFFER_SIZE)
                                {
                                    _tcpBuffer.Remove(0, _tcpBuffer.Length - MAX_BUFFER_SIZE);
                                    WriteMessage("TCP buffer overflow, truncated",
                                        MessageTarget.ServerMonitor, true, false, false);
                                }

                                ProcessBuffer(_tcpBuffer, (line) =>
                                {
                                    WriteMessage(line, MessageTarget.ServerMonitor,
                                        cbTimecodeServer.Checked, true, true);
                                    WriteMessage(line, MessageTarget.ServerLog,
                                        false, false, false);
                                    ProcessIncomingData(line);
                                });
                            }
                        }
                        else
                        {
                            // Small delay when no data available to prevent CPU overload
                            Thread.Sleep(50);
                        }
                    }
                    catch (IOException ioEx)
                    {
                        // Handle connection loss
                        if (isConnected)
                        {
                            WriteMessage($"Connection error: {ioEx.Message}",
                                MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (isConnected)
                        {
                            WriteMessage($"Receive error: {ex.Message}",
                                MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                        }
                        break;
                    }
                }
            }
            finally
            {
                if (isConnected)
                {
                    isConnected = false;
                    UpdateConnectionStatus(false);
                    WriteMessage("Connection lost",
                        MessageTarget.ServerMonitor, cbTimecodeServer.Checked, false, false);
                }
            }
        }

        private async void btnConnectWiFi_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                await Task.Run(() => Connect());
            }
        }

        private void btnSendTCP_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected || stream == null)
            {
                MessageBox.Show("Please connect to the TCP server first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSendTCP.Text))
            {
                MessageBox.Show("Please enter text to send.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else
                    {
                        throw new Exception("Network stream is not writable");
                    }
                }

                WriteMessage(dataToSend, MessageTarget.ServerMonitor,
                    cbTimecodeServer.Checked, true, false);
                txtSendTCP.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (client != null && !client.Connected)
                {
                    Disconnect();
                }
            }
        }

        #endregion

        #region Task Extensions

        public static class TaskExtensions
        {
            public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
            {
                var tcs = new TaskCompletionSource<bool>();
                using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
                {
                    if (task != await Task.WhenAny(task, tcs.Task))
                    {
                        throw new OperationCanceledException(cancellationToken);
                    }
                }
                return await task;
            }

            public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
            {
                var tcs = new TaskCompletionSource<bool>();
                using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
                {
                    if (task != await Task.WhenAny(task, tcs.Task))
                    {
                        throw new OperationCanceledException(cancellationToken);
                    }
                }
                await task;
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

                // Write header matching the actual data columns
                string header = "Timestamp,Temperature1,Temperature2,Mass";
                File.WriteAllText(currentLogFile, header + Environment.NewLine);

                // Write metadata if available
                if (!string.IsNullOrEmpty(txtLogMetadata.Text))
                {
                    File.AppendAllText(currentLogFile, $"# {txtLogMetadata.Text}" + Environment.NewLine);
                }

                recordingStartTime = DateTime.Now;
                isRecording = true;
                btnRecord.BackColor = Color.Red;
                lblRecordingStatus.Text = $"Recording to: {currentLogFile}";
                lblRecordingStatus.ForeColor = Color.Red;

                WriteMessage("Recording started", MessageTarget.ServerLog, true, false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting recording: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopRecording()
        {
            isRecording = false;
            btnRecord.BackColor = Color.Transparent;
            lblRecordingStatus.Text = "Not recording";
            lblRecordingStatus.ForeColor = Color.Black;

            WriteMessage("Recording stopped", MessageTarget.ServerLog, true, false, false);

            // Add summary to log file
            try
            {
                if (!string.IsNullOrEmpty(currentLogFile))
                {
                    TimeSpan duration = DateTime.Now - recordingStartTime;
                    File.AppendAllText(currentLogFile, $"# Recording duration: {duration.TotalSeconds} seconds" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Temp1: {lastTemp1}°C" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Temp2: {lastTemp2}°C" + Environment.NewLine);
                    File.AppendAllText(currentLogFile, $"# Max Mass: {lastMass}kg" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                WriteMessage($"Error finalizing log file: {ex.Message}", MessageTarget.ServerMonitor, true, false, false);
            }

            currentLogFile = "";
        }

        private void SaveConfig()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIPAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtTcpPort.Text))
                {
                    MessageBox.Show("IP Address and Port are required fields.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                string configJson = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, configJson);

                WriteMessage("Configuration saved", MessageTarget.ServerLog, true, false, false);
            }
            catch (Exception ex)
            {
                WriteMessage($"Configuration save error: {ex.Message}",
                    MessageTarget.ServerMonitor, true, false, false);
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
                    WifiConfig config = Newtonsoft.Json.JsonConvert.DeserializeObject<WifiConfig>(configJson);

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

                    WriteMessage("Configuration loaded", MessageTarget.SerialMonitor, true, false, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                            // Write header
                            csvContent.AppendLine("Timestamp,Temperature1,Temperature2,Mass");
                            // Write data
                            foreach (DataRow row in liveDataTable.Rows)
                            {
                                csvContent.AppendLine($"{row["Time"]},{row["Temp1"]},{row["Temp2"]},{row["Mass"]}");
                            }
                            File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());
                        }
                        break;

                    case "TXT":
                        saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                        saveFileDialog.FileName = $"export_{timestamp}.txt";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StringBuilder txtContent = new StringBuilder();
                            // Write header
                            txtContent.AppendLine("ESP32 Data Logger Export");
                            txtContent.AppendLine($"