using System;

namespace SFCS
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllTerminalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSerialTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTCPTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbFullscren = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Quit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonR = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblConnectionStatus = new System.Windows.Forms.ToolStripLabel();
            this.programMessages = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRecordingTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblRecordingStatus = new System.Windows.Forms.Label();
            this.txtLogMetadata = new System.Windows.Forms.TextBox();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.tabDataPanel = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvLiveData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chartTemp1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartMass = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartTemp2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbServerMonitor = new System.Windows.Forms.GroupBox();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnConnectWiFi = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtTcpPort = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSSID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStopCmdWifi = new System.Windows.Forms.TextBox();
            this.txtStartCmdWifi = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cbViewPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSendTCP = new System.Windows.Forms.TextBox();
            this.btnSendTCP = new System.Windows.Forms.Button();
            this.cbTimecodeServer = new System.Windows.Forms.CheckBox();
            this.btnClearServer = new System.Windows.Forms.Button();
            this.rtbMonitorServer = new System.Windows.Forms.RichTextBox();
            this.tabSerial = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnectSerial = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbAutoSendWifi = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPSWcmd = new System.Windows.Forms.TextBox();
            this.txtSSIDcmd = new System.Windows.Forms.TextBox();
            this.txtPortCmd = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSendSerial = new System.Windows.Forms.TextBox();
            this.btnSendSerial = new System.Windows.Forms.Button();
            this.cbTimecodeSerial = new System.Windows.Forms.CheckBox();
            this.btnClearSerial = new System.Windows.Forms.Button();
            this.rtbMonitorSerial = new System.Windows.Forms.RichTextBox();
            this.recordingTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.mainTabs.SuspendLayout();
            this.tabDataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveData)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMass)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp2)).BeginInit();
            this.gbServerMonitor.SuspendLayout();
            this.tabServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabSerial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip1.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton2,
            this.toolStripSeparator2,
            this.toolStripDropDownButton3,
            this.tsbFullscren,
            this.toolStripSeparator3,
            this.Quit,
            this.toolStripSeparator4,
            this.toolStripButtonR,
            this.toolStripSeparator5,
            this.lblConnectionStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1062, 27);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.exportDataToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::SFCS.Properties.Resources.File;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsCSVToolStripMenuItem,
            this.exportRawToolStripMenuItem,
            this.exportTXTToolStripMenuItem});
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            // 
            // exportAsCSVToolStripMenuItem
            // 
            this.exportAsCSVToolStripMenuItem.Name = "exportAsCSVToolStripMenuItem";
            this.exportAsCSVToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.exportAsCSVToolStripMenuItem.Text = "Export as CSV";
            this.exportAsCSVToolStripMenuItem.Click += new System.EventHandler(this.ExportAsCSVToolStripMenuItem_Click);
            // 
            // exportRawToolStripMenuItem
            // 
            this.exportRawToolStripMenuItem.Name = "exportRawToolStripMenuItem";
            this.exportRawToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.exportRawToolStripMenuItem.Text = "Export Raw ";
            this.exportRawToolStripMenuItem.Click += new System.EventHandler(this.ExportRawToolStripMenuItem_Click);
            // 
            // exportTXTToolStripMenuItem
            // 
            this.exportTXTToolStripMenuItem.Name = "exportTXTToolStripMenuItem";
            this.exportTXTToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.exportTXTToolStripMenuItem.Text = "Export TXT";
            this.exportTXTToolStripMenuItem.Click += new System.EventHandler(this.ExportTXTToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.clearDataToolStripMenuItem1});
            this.toolStripDropDownButton2.Image = global::SFCS.Properties.Resources.Tools;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(34, 24);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearDataToolStripMenuItem,
            this.clearAllTerminalsToolStripMenuItem,
            this.clearSerialTerminalToolStripMenuItem,
            this.clearTCPTerminalToolStripMenuItem});
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // clearDataToolStripMenuItem
            // 
            this.clearDataToolStripMenuItem.Name = "clearDataToolStripMenuItem";
            this.clearDataToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.clearDataToolStripMenuItem.Text = "Clear Data";
            this.clearDataToolStripMenuItem.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // clearAllTerminalsToolStripMenuItem
            // 
            this.clearAllTerminalsToolStripMenuItem.Name = "clearAllTerminalsToolStripMenuItem";
            this.clearAllTerminalsToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.clearAllTerminalsToolStripMenuItem.Text = "Clear All Terminals";
            this.clearAllTerminalsToolStripMenuItem.Click += new System.EventHandler(this.clearAllTerminalsToolStripMenuItem_Click);
            // 
            // clearSerialTerminalToolStripMenuItem
            // 
            this.clearSerialTerminalToolStripMenuItem.Name = "clearSerialTerminalToolStripMenuItem";
            this.clearSerialTerminalToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.clearSerialTerminalToolStripMenuItem.Text = "Clear Serial Terminal";
            this.clearSerialTerminalToolStripMenuItem.Click += new System.EventHandler(this.btnClearSerial_Click);
            // 
            // clearTCPTerminalToolStripMenuItem
            // 
            this.clearTCPTerminalToolStripMenuItem.Name = "clearTCPTerminalToolStripMenuItem";
            this.clearTCPTerminalToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.clearTCPTerminalToolStripMenuItem.Text = "Clear TCP Terminal";
            this.clearTCPTerminalToolStripMenuItem.Click += new System.EventHandler(this.btnClearServer_Click);
            // 
            // clearDataToolStripMenuItem1
            // 
            this.clearDataToolStripMenuItem1.Name = "clearDataToolStripMenuItem1";
            this.clearDataToolStripMenuItem1.Size = new System.Drawing.Size(160, 26);
            this.clearDataToolStripMenuItem1.Text = "Clear Data";
            this.clearDataToolStripMenuItem1.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportConfigToolStripMenuItem,
            this.showConfigToolStripMenuItem,
            this.importConfigToolStripMenuItem});
            this.toolStripDropDownButton3.Image = global::SFCS.Properties.Resources.Settings;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(34, 24);
            this.toolStripDropDownButton3.Text = "toolStripDropDownButton3";
            // 
            // exportConfigToolStripMenuItem
            // 
            this.exportConfigToolStripMenuItem.Name = "exportConfigToolStripMenuItem";
            this.exportConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.exportConfigToolStripMenuItem.Text = "Export Config";
            this.exportConfigToolStripMenuItem.Click += new System.EventHandler(this.ExportConfigToolStripMenuItem_Click);
            // 
            // showConfigToolStripMenuItem
            // 
            this.showConfigToolStripMenuItem.Name = "showConfigToolStripMenuItem";
            this.showConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.showConfigToolStripMenuItem.Text = "Show Config";
            this.showConfigToolStripMenuItem.Click += new System.EventHandler(this.showConfigToolStripMenuItem_Click);
            // 
            // importConfigToolStripMenuItem
            // 
            this.importConfigToolStripMenuItem.Name = "importConfigToolStripMenuItem";
            this.importConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.importConfigToolStripMenuItem.Text = "Import Config";
            this.importConfigToolStripMenuItem.Click += new System.EventHandler(this.ImportConfigToolStripMenuItem_Click);
            // 
            // tsbFullscren
            // 
            this.tsbFullscren.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbFullscren.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFullscren.Image = global::SFCS.Properties.Resources.Fullscreen;
            this.tsbFullscren.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullscren.Name = "tsbFullscren";
            this.tsbFullscren.Size = new System.Drawing.Size(29, 24);
            this.tsbFullscren.Text = "toolStripButton1";
            this.tsbFullscren.Click += new System.EventHandler(this.tsbFullscren_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // Quit
            // 
            this.Quit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Quit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Quit.Image = global::SFCS.Properties.Resources.Quit;
            this.Quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(29, 24);
            this.Quit.Text = "toolStripButton2";
            this.Quit.Visible = false;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripButtonR
            // 
            this.toolStripButtonR.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonR.Image = global::SFCS.Properties.Resources.Refresh;
            this.toolStripButtonR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonR.Name = "toolStripButtonR";
            this.toolStripButtonR.Size = new System.Drawing.Size(29, 24);
            this.toolStripButtonR.Text = "toolStripButton3";
            this.toolStripButtonR.Click += new System.EventHandler(this.toolStripButtonR_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Helvetica-Narrow", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(104, 24);
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // programMessages
            // 
            this.programMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.programMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programMessages.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programMessages.ForeColor = System.Drawing.Color.LightGreen;
            this.programMessages.Location = new System.Drawing.Point(3, 21);
            this.programMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.programMessages.Name = "programMessages";
            this.programMessages.ReadOnly = true;
            this.programMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.programMessages.Size = new System.Drawing.Size(1022, 135);
            this.programMessages.TabIndex = 2;
            this.programMessages.Text = "";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 6;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.Controls.Add(this.lblRecordingTime, 5, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnStart, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnRecord, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnStop, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblRecordingStatus, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtLogMetadata, 3, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1042, 46);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // lblRecordingTime
            // 
            this.lblRecordingTime.AutoSize = true;
            this.lblRecordingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecordingTime.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordingTime.Location = new System.Drawing.Point(945, 0);
            this.lblRecordingTime.Name = "lblRecordingTime";
            this.lblRecordingTime.Size = new System.Drawing.Size(94, 47);
            this.lblRecordingTime.TabIndex = 5;
            this.lblRecordingTime.Text = "00:00:00";
            this.lblRecordingTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Image = global::SFCS.Properties.Resources.Start;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(44, 41);
            this.btnStart.TabIndex = 0;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecord.Image = global::SFCS.Properties.Resources.notRecording;
            this.btnRecord.Location = new System.Drawing.Point(103, 3);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(44, 41);
            this.btnRecord.TabIndex = 2;
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Image = global::SFCS.Properties.Resources.Stop;
            this.btnStop.Location = new System.Drawing.Point(53, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(44, 41);
            this.btnStop.TabIndex = 1;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblRecordingStatus
            // 
            this.lblRecordingStatus.AutoSize = true;
            this.lblRecordingStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecordingStatus.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordingStatus.Location = new System.Drawing.Point(845, 0);
            this.lblRecordingStatus.Name = "lblRecordingStatus";
            this.lblRecordingStatus.Size = new System.Drawing.Size(94, 47);
            this.lblRecordingStatus.TabIndex = 3;
            this.lblRecordingStatus.Text = "Not recording";
            this.lblRecordingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogMetadata
            // 
            this.txtLogMetadata.Location = new System.Drawing.Point(153, 3);
            this.txtLogMetadata.Name = "txtLogMetadata";
            this.txtLogMetadata.Size = new System.Drawing.Size(486, 27);
            this.txtLogMetadata.TabIndex = 6;
            this.txtLogMetadata.Visible = false;
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.tabDataPanel);
            this.mainTabs.Controls.Add(this.tabServer);
            this.mainTabs.Controls.Add(this.tabSerial);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabs.Location = new System.Drawing.Point(0, 27);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(1062, 646);
            this.mainTabs.TabIndex = 6;
            // 
            // tabDataPanel
            // 
            this.tabDataPanel.Controls.Add(this.splitContainer1);
            this.tabDataPanel.Location = new System.Drawing.Point(4, 29);
            this.tabDataPanel.Name = "tabDataPanel";
            this.tabDataPanel.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataPanel.Size = new System.Drawing.Size(1054, 613);
            this.tabDataPanel.TabIndex = 0;
            this.tabDataPanel.Text = "Data Panel";
            this.tabDataPanel.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel2.Controls.Add(this.gbServerMonitor);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.splitContainer1.Size = new System.Drawing.Size(1048, 607);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1048, 422);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 55);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainer2.Size = new System.Drawing.Size(1042, 364);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.dgvLiveData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 352);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Live CSV";
            // 
            // dgvLiveData
            // 
            this.dgvLiveData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLiveData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLiveData.Location = new System.Drawing.Point(3, 21);
            this.dgvLiveData.Name = "dgvLiveData";
            this.dgvLiveData.RowHeadersWidth = 51;
            this.dgvLiveData.RowTemplate.Height = 24;
            this.dgvLiveData.Size = new System.Drawing.Size(202, 328);
            this.dgvLiveData.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(816, 362);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.White;
            this.groupBox6.Controls.Add(this.chartTemp1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox6.Size = new System.Drawing.Size(264, 350);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Temperature Graph";
            // 
            // chartTemp1
            // 
            chartArea7.Name = "ChartArea1";
            this.chartTemp1.ChartAreas.Add(chartArea7);
            this.chartTemp1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chartTemp1.Legends.Add(legend7);
            this.chartTemp1.Location = new System.Drawing.Point(0, 20);
            this.chartTemp1.Name = "chartTemp1";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chartTemp1.Series.Add(series7);
            this.chartTemp1.Size = new System.Drawing.Size(264, 330);
            this.chartTemp1.TabIndex = 0;
            this.chartTemp1.Text = "chart1";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.chartMass);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(546, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox3.Size = new System.Drawing.Size(264, 350);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mass Graph";
            // 
            // chartMass
            // 
            chartArea8.Name = "ChartArea1";
            this.chartMass.ChartAreas.Add(chartArea8);
            this.chartMass.Dock = System.Windows.Forms.DockStyle.Fill;
            legend8.Name = "Legend1";
            this.chartMass.Legends.Add(legend8);
            this.chartMass.Location = new System.Drawing.Point(0, 20);
            this.chartMass.Name = "chartMass";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.chartMass.Series.Add(series8);
            this.chartMass.Size = new System.Drawing.Size(264, 330);
            this.chartMass.TabIndex = 1;
            this.chartMass.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.chartTemp2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(276, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(264, 350);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Temperature Graph";
            // 
            // chartTemp2
            // 
            chartArea9.Name = "ChartArea1";
            this.chartTemp2.ChartAreas.Add(chartArea9);
            this.chartTemp2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend9.Name = "Legend1";
            this.chartTemp2.Legends.Add(legend9);
            this.chartTemp2.Location = new System.Drawing.Point(0, 20);
            this.chartTemp2.Margin = new System.Windows.Forms.Padding(0);
            this.chartTemp2.Name = "chartTemp2";
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            this.chartTemp2.Series.Add(series9);
            this.chartTemp2.Size = new System.Drawing.Size(264, 330);
            this.chartTemp2.TabIndex = 1;
            this.chartTemp2.Text = "chart1";
            // 
            // gbServerMonitor
            // 
            this.gbServerMonitor.BackColor = System.Drawing.Color.White;
            this.gbServerMonitor.Controls.Add(this.programMessages);
            this.gbServerMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbServerMonitor.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbServerMonitor.Location = new System.Drawing.Point(10, 9);
            this.gbServerMonitor.Name = "gbServerMonitor";
            this.gbServerMonitor.Size = new System.Drawing.Size(1028, 159);
            this.gbServerMonitor.TabIndex = 2;
            this.gbServerMonitor.TabStop = false;
            this.gbServerMonitor.Tag = "";
            this.gbServerMonitor.Text = "Messages";
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.splitContainer3);
            this.tabServer.Location = new System.Drawing.Point(4, 29);
            this.tabServer.Name = "tabServer";
            this.tabServer.Size = new System.Drawing.Size(1054, 613);
            this.tabServer.TabIndex = 2;
            this.tabServer.Text = "Server";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer3.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer3.Panel1.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel6);
            this.splitContainer3.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer3.Size = new System.Drawing.Size(1054, 613);
            this.splitContainer3.SplitterDistance = 270;
            this.splitContainer3.SplitterWidth = 8;
            this.splitContainer3.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(10, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 595);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "COM Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.AutoScrollMinSize = new System.Drawing.Size(244, 569);
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConnectWiFi, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtIPAddress, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtTcpPort, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label27, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtSSID, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtStopCmdWifi, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.txtStartCmdWifi, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.cbViewPassword, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtPassword, 1, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(244, 533);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 13;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(244, 571);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gray;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 37);
            this.label9.TabIndex = 7;
            this.label9.Text = "Connectin Status";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnectWiFi
            // 
            this.btnConnectWiFi.AutoSize = true;
            this.btnConnectWiFi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnectWiFi.Location = new System.Drawing.Point(149, 2);
            this.btnConnectWiFi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnectWiFi.Name = "btnConnectWiFi";
            this.btnConnectWiFi.Size = new System.Drawing.Size(92, 33);
            this.btnConnectWiFi.TabIndex = 9;
            this.btnConnectWiFi.Text = "Connect";
            this.btnConnectWiFi.UseVisualStyleBackColor = true;
            this.btnConnectWiFi.Click += new System.EventHandler(this.btnConnectWiFi_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(3, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 32);
            this.label13.TabIndex = 10;
            this.label13.Text = "Port";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(3, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 32);
            this.label14.TabIndex = 8;
            this.label14.Text = "IP Adress";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Gray;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(3, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(140, 32);
            this.label18.TabIndex = 24;
            this.label18.Text = "Connection Setup";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIPAddress.Location = new System.Drawing.Point(149, 90);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(92, 25);
            this.txtIPAddress.TabIndex = 27;
            // 
            // txtTcpPort
            // 
            this.txtTcpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTcpPort.Location = new System.Drawing.Point(149, 122);
            this.txtTcpPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTcpPort.Name = "txtTcpPort";
            this.txtTcpPort.Size = new System.Drawing.Size(92, 25);
            this.txtTcpPort.TabIndex = 28;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(3, 152);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(140, 32);
            this.label27.TabIndex = 29;
            this.label27.Text = "SSID";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSSID
            // 
            this.txtSSID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSSID.Location = new System.Drawing.Point(149, 154);
            this.txtSSID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSSID.Name = "txtSSID";
            this.txtSSID.Size = new System.Drawing.Size(92, 25);
            this.txtSSID.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(3, 184);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 32);
            this.label15.TabIndex = 31;
            this.label15.Text = "Password";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStopCmdWifi
            // 
            this.txtStopCmdWifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStopCmdWifi.Location = new System.Drawing.Point(149, 346);
            this.txtStopCmdWifi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStopCmdWifi.Name = "txtStopCmdWifi";
            this.txtStopCmdWifi.Size = new System.Drawing.Size(92, 25);
            this.txtStopCmdWifi.TabIndex = 15;
            // 
            // txtStartCmdWifi
            // 
            this.txtStartCmdWifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartCmdWifi.Location = new System.Drawing.Point(149, 314);
            this.txtStartCmdWifi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStartCmdWifi.Name = "txtStartCmdWifi";
            this.txtStartCmdWifi.Size = new System.Drawing.Size(92, 25);
            this.txtStartCmdWifi.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(3, 344);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 32);
            this.label11.TabIndex = 14;
            this.label11.Text = "Stop Command";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(3, 312);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 32);
            this.label12.TabIndex = 13;
            this.label12.Text = "Start Command";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Gray;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(3, 280);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 32);
            this.label19.TabIndex = 25;
            this.label19.Text = "Commands";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbViewPassword
            // 
            this.cbViewPassword.AutoSize = true;
            this.cbViewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbViewPassword.Font = new System.Drawing.Font("Helvetica", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbViewPassword.ForeColor = System.Drawing.Color.Black;
            this.cbViewPassword.Location = new System.Drawing.Point(149, 218);
            this.cbViewPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbViewPassword.Name = "cbViewPassword";
            this.cbViewPassword.Size = new System.Drawing.Size(92, 28);
            this.cbViewPassword.TabIndex = 33;
            this.cbViewPassword.Text = "View Password";
            this.cbViewPassword.UseVisualStyleBackColor = true;
            this.cbViewPassword.CheckedChanged += new System.EventHandler(this.cbViewPassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(149, 187);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(92, 25);
            this.txtPassword.TabIndex = 34;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.rtbMonitorServer, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(766, 603);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Controls.Add(this.txtSendTCP, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnSendTCP, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbTimecodeServer, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnClearServer, 1, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(13, 509);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(740, 82);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // txtSendTCP
            // 
            this.txtSendTCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendTCP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSendTCP.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendTCP.ForeColor = System.Drawing.Color.LightGreen;
            this.txtSendTCP.Location = new System.Drawing.Point(3, 3);
            this.txtSendTCP.Multiline = true;
            this.txtSendTCP.Name = "txtSendTCP";
            this.txtSendTCP.Size = new System.Drawing.Size(549, 35);
            this.txtSendTCP.TabIndex = 0;
            this.txtSendTCP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendTCP_KeyDown);
            // 
            // btnSendTCP
            // 
            this.btnSendTCP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendTCP.Location = new System.Drawing.Point(558, 3);
            this.btnSendTCP.Name = "btnSendTCP";
            this.btnSendTCP.Size = new System.Drawing.Size(179, 35);
            this.btnSendTCP.TabIndex = 1;
            this.btnSendTCP.Text = "SEND";
            this.btnSendTCP.UseVisualStyleBackColor = true;
            this.btnSendTCP.Click += new System.EventHandler(this.btnSendTCP_Click);
            // 
            // cbTimecodeServer
            // 
            this.cbTimecodeServer.AutoSize = true;
            this.cbTimecodeServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTimecodeServer.Location = new System.Drawing.Point(3, 44);
            this.cbTimecodeServer.Name = "cbTimecodeServer";
            this.cbTimecodeServer.Size = new System.Drawing.Size(549, 35);
            this.cbTimecodeServer.TabIndex = 2;
            this.cbTimecodeServer.Text = "Timecode";
            this.cbTimecodeServer.UseVisualStyleBackColor = true;
            // 
            // btnClearServer
            // 
            this.btnClearServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearServer.Location = new System.Drawing.Point(558, 44);
            this.btnClearServer.Name = "btnClearServer";
            this.btnClearServer.Size = new System.Drawing.Size(179, 35);
            this.btnClearServer.TabIndex = 3;
            this.btnClearServer.Text = "Clear";
            this.btnClearServer.UseVisualStyleBackColor = true;
            this.btnClearServer.Click += new System.EventHandler(this.btnClearServer_Click);
            // 
            // rtbMonitorServer
            // 
            this.rtbMonitorServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbMonitorServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMonitorServer.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMonitorServer.ForeColor = System.Drawing.Color.LightGreen;
            this.rtbMonitorServer.Location = new System.Drawing.Point(13, 12);
            this.rtbMonitorServer.Name = "rtbMonitorServer";
            this.rtbMonitorServer.ReadOnly = true;
            this.rtbMonitorServer.Size = new System.Drawing.Size(740, 491);
            this.rtbMonitorServer.TabIndex = 1;
            this.rtbMonitorServer.Text = "";
            // 
            // tabSerial
            // 
            this.tabSerial.Controls.Add(this.splitContainer4);
            this.tabSerial.Location = new System.Drawing.Point(4, 29);
            this.tabSerial.Name = "tabSerial";
            this.tabSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabSerial.Size = new System.Drawing.Size(1054, 613);
            this.tabSerial.TabIndex = 1;
            this.tabSerial.Text = "Serial";
            this.tabSerial.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer4.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer4.Panel1.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer4.Panel2.Controls.Add(this.tableLayoutPanel9);
            this.splitContainer4.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer4.Size = new System.Drawing.Size(1048, 607);
            this.splitContainer4.SplitterDistance = 266;
            this.splitContainer4.SplitterWidth = 8;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Silver;
            this.groupBox5.Controls.Add(this.tableLayoutPanel1);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(10, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(246, 589);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "COM Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(244, 569);
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnectSerial, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbPorts, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbBaudRate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbAutoSendWifi, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label22, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtPSWcmd, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtSSIDcmd, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtPortCmd, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(244, 533);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 563);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 38);
            this.label1.TabIndex = 7;
            this.label1.Text = "Connectin Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnectSerial
            // 
            this.btnConnectSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnectSerial.Location = new System.Drawing.Point(149, 2);
            this.btnConnectSerial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnectSerial.Name = "btnConnectSerial";
            this.btnConnectSerial.Size = new System.Drawing.Size(92, 34);
            this.btnConnectSerial.TabIndex = 9;
            this.btnConnectSerial.Text = "Connect";
            this.btnConnectSerial.UseVisualStyleBackColor = true;
            this.btnConnectSerial.Click += new System.EventHandler(this.btnConnectSerial_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 33);
            this.label5.TabIndex = 10;
            this.label5.Text = "COM Port";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 33);
            this.label2.TabIndex = 8;
            this.label2.Text = "Baud Rate";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbPorts
            // 
            this.cbPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(150, 127);
            this.cbPorts.Margin = new System.Windows.Forms.Padding(4);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(90, 25);
            this.cbPorts.TabIndex = 2;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.BackColor = System.Drawing.Color.White;
            this.cbBaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbBaudRate.Font = new System.Drawing.Font("Helvetica", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "50",
            "75",
            "110",
            "134 ",
            "150 ",
            "200 ",
            "300 ",
            "600 ",
            "1200 ",
            "1800 ",
            "2400 ",
            "4800 ",
            "9600 ",
            "19200 ",
            "28800 ",
            "38400 ",
            "57600 ",
            "76800 ",
            "115200 ",
            "230400 ",
            "460800 ",
            "576000 ",
            "921600"});
            this.cbBaudRate.Location = new System.Drawing.Point(149, 92);
            this.cbBaudRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(92, 25);
            this.cbBaudRate.TabIndex = 6;
            this.cbBaudRate.Text = "115200 ";
            this.cbBaudRate.UseWaitCursor = true;
            // 
            // cbAutoSendWifi
            // 
            this.cbAutoSendWifi.AutoSize = true;
            this.cbAutoSendWifi.Checked = true;
            this.cbAutoSendWifi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoSendWifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAutoSendWifi.Location = new System.Drawing.Point(149, 210);
            this.cbAutoSendWifi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAutoSendWifi.Name = "cbAutoSendWifi";
            this.cbAutoSendWifi.Size = new System.Drawing.Size(92, 29);
            this.cbAutoSendWifi.TabIndex = 23;
            this.cbAutoSendWifi.UseVisualStyleBackColor = true;
            this.cbAutoSendWifi.CheckedChanged += new System.EventHandler(this.cbAutoSendWifi_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(3, 208);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(140, 33);
            this.label23.TabIndex = 20;
            this.label23.Text = "Auto Connect WiFi";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Gray;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(3, 57);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(140, 33);
            this.label24.TabIndex = 24;
            this.label24.Text = "Connection Setup";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Gray;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(3, 175);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(140, 33);
            this.label26.TabIndex = 26;
            this.label26.Text = "Auto-send Credentials";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(3, 307);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(140, 33);
            this.label22.TabIndex = 19;
            this.label22.Text = "Send Password Command";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(3, 274);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(140, 33);
            this.label21.TabIndex = 18;
            this.label21.Text = "Send SSID Command";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Helvetica", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 33);
            this.label6.TabIndex = 30;
            this.label6.Text = "Send Port Command";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPSWcmd
            // 
            this.txtPSWcmd.BackColor = System.Drawing.Color.White;
            this.txtPSWcmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPSWcmd.Location = new System.Drawing.Point(149, 309);
            this.txtPSWcmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPSWcmd.Name = "txtPSWcmd";
            this.txtPSWcmd.Size = new System.Drawing.Size(92, 25);
            this.txtPSWcmd.TabIndex = 22;
            this.txtPSWcmd.Text = "PASSWORD";
            // 
            // txtSSIDcmd
            // 
            this.txtSSIDcmd.BackColor = System.Drawing.Color.White;
            this.txtSSIDcmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSSIDcmd.Location = new System.Drawing.Point(149, 276);
            this.txtSSIDcmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSSIDcmd.Name = "txtSSIDcmd";
            this.txtSSIDcmd.Size = new System.Drawing.Size(92, 25);
            this.txtSSIDcmd.TabIndex = 21;
            this.txtSSIDcmd.Text = "SSID";
            // 
            // txtPortCmd
            // 
            this.txtPortCmd.BackColor = System.Drawing.Color.White;
            this.txtPortCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPortCmd.Location = new System.Drawing.Point(149, 243);
            this.txtPortCmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPortCmd.Name = "txtPortCmd";
            this.txtPortCmd.Size = new System.Drawing.Size(92, 25);
            this.txtPortCmd.TabIndex = 31;
            this.txtPortCmd.Text = "PORT";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.rtbMonitorSerial, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(764, 597);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.Controls.Add(this.txtSendSerial, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnSendSerial, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.cbTimecodeSerial, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnClearSerial, 1, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(13, 504);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(738, 81);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // txtSendSerial
            // 
            this.txtSendSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendSerial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSendSerial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSendSerial.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendSerial.ForeColor = System.Drawing.Color.LightGreen;
            this.txtSendSerial.Location = new System.Drawing.Point(3, 3);
            this.txtSendSerial.Multiline = true;
            this.txtSendSerial.Name = "txtSendSerial";
            this.txtSendSerial.Size = new System.Drawing.Size(547, 34);
            this.txtSendSerial.TabIndex = 0;
            this.txtSendSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendSerial_KeyDown);
            // 
            // btnSendSerial
            // 
            this.btnSendSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendSerial.Location = new System.Drawing.Point(556, 3);
            this.btnSendSerial.Name = "btnSendSerial";
            this.btnSendSerial.Size = new System.Drawing.Size(179, 34);
            this.btnSendSerial.TabIndex = 1;
            this.btnSendSerial.Text = "SEND";
            this.btnSendSerial.UseVisualStyleBackColor = true;
            this.btnSendSerial.Click += new System.EventHandler(this.btnSendSerial_Click);
            // 
            // cbTimecodeSerial
            // 
            this.cbTimecodeSerial.AutoSize = true;
            this.cbTimecodeSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTimecodeSerial.Location = new System.Drawing.Point(3, 43);
            this.cbTimecodeSerial.Name = "cbTimecodeSerial";
            this.cbTimecodeSerial.Size = new System.Drawing.Size(547, 35);
            this.cbTimecodeSerial.TabIndex = 2;
            this.cbTimecodeSerial.Text = "Timecode";
            this.cbTimecodeSerial.UseVisualStyleBackColor = true;
            // 
            // btnClearSerial
            // 
            this.btnClearSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearSerial.Location = new System.Drawing.Point(556, 43);
            this.btnClearSerial.Name = "btnClearSerial";
            this.btnClearSerial.Size = new System.Drawing.Size(179, 35);
            this.btnClearSerial.TabIndex = 3;
            this.btnClearSerial.Text = "Clear";
            this.btnClearSerial.UseVisualStyleBackColor = true;
            this.btnClearSerial.Click += new System.EventHandler(this.btnClearSerial_Click);
            // 
            // rtbMonitorSerial
            // 
            this.rtbMonitorSerial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbMonitorSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMonitorSerial.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMonitorSerial.ForeColor = System.Drawing.Color.LightGreen;
            this.rtbMonitorSerial.Location = new System.Drawing.Point(13, 12);
            this.rtbMonitorSerial.Name = "rtbMonitorSerial";
            this.rtbMonitorSerial.ReadOnly = true;
            this.rtbMonitorSerial.Size = new System.Drawing.Size(738, 486);
            this.rtbMonitorSerial.TabIndex = 1;
            this.rtbMonitorSerial.Text = "";
            // 
            // recordingTimer
            // 
            this.recordingTimer.Interval = 1000;
            this.recordingTimer.Tick += new System.EventHandler(this.recordingTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.mainTabs);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Helvetica", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 530);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Static Fire Ground Station";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.mainTabs.ResumeLayout(false);
            this.tabDataPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveData)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMass)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp2)).EndInit();
            this.gbServerMonitor.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabSerial.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTXTToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllTerminalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSerialTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTCPTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem exportConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbFullscren;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Quit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonR;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lblConnectionStatus;
        private System.Windows.Forms.RichTextBox programMessages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblRecordingTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblRecordingStatus;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage tabDataPanel;
        private System.Windows.Forms.TabPage tabSerial;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbServerMonitor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnConnectWiFi;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtTcpPort;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSSID;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtStopCmdWifi;
        private System.Windows.Forms.TextBox txtStartCmdWifi;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox cbViewPassword;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnectSerial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.CheckBox cbAutoSendWifi;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPSWcmd;
        private System.Windows.Forms.TextBox txtSSIDcmd;
        private System.Windows.Forms.TextBox txtPortCmd;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txtSendTCP;
        private System.Windows.Forms.Button btnSendTCP;
        private System.Windows.Forms.CheckBox cbTimecodeServer;
        private System.Windows.Forms.RichTextBox rtbMonitorServer;
        private System.Windows.Forms.Button btnClearServer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TextBox txtSendSerial;
        private System.Windows.Forms.Button btnSendSerial;
        private System.Windows.Forms.CheckBox cbTimecodeSerial;
        private System.Windows.Forms.Button btnClearSerial;
        private System.Windows.Forms.RichTextBox rtbMonitorSerial;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMass;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp2;
        private System.Windows.Forms.DataGridView dgvLiveData;
        private System.Windows.Forms.Timer recordingTimer;
        private System.Windows.Forms.TextBox txtLogMetadata;
    }
}