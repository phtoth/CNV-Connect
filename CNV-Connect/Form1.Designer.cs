namespace CNV_Connect
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            comboAircraftSoft = new ComboBox();
            label5 = new Label();
            comboAircraftModel = new ComboBox();
            comboAircraftManufacturer = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            lblBoardVersion = new Label();
            label4 = new Label();
            btnConnTest = new Button();
            comboConnSerial = new ComboBox();
            label3 = new Label();
            groupBox3 = new GroupBox();
            lblSIMStatus = new Label();
            lblSerialStatus = new Label();
            btnConnectSIM = new Button();
            btnConnectSerial = new Button();
            label7 = new Label();
            label6 = new Label();
            tmrStillAlive = new System.Windows.Forms.Timer(components);
            groupBox4 = new GroupBox();
            cbRadio = new CheckBox();
            cbOverhead = new CheckBox();
            button1 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboAircraftSoft);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(comboAircraftModel);
            groupBox1.Controls.Add(comboAircraftManufacturer);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(551, 188);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Modelo de Aeronave";
            // 
            // comboAircraftSoft
            // 
            comboAircraftSoft.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAircraftSoft.FormattingEnabled = true;
            comboAircraftSoft.Location = new Point(136, 129);
            comboAircraftSoft.Name = "comboAircraftSoft";
            comboAircraftSoft.Size = new Size(182, 33);
            comboAircraftSoft.TabIndex = 6;
            comboAircraftSoft.SelectedIndexChanged += comboAircraftSoft_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 129);
            label5.Name = "label5";
            label5.Size = new Size(102, 25);
            label5.TabIndex = 5;
            label5.Text = "Fornecedor";
            // 
            // comboAircraftModel
            // 
            comboAircraftModel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAircraftModel.FormattingEnabled = true;
            comboAircraftModel.Location = new Point(135, 84);
            comboAircraftModel.Name = "comboAircraftModel";
            comboAircraftModel.Size = new Size(182, 33);
            comboAircraftModel.TabIndex = 4;
            comboAircraftModel.SelectedIndexChanged += comboAircraftModel_SelectedIndexChanged;
            // 
            // comboAircraftManufacturer
            // 
            comboAircraftManufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAircraftManufacturer.FormattingEnabled = true;
            comboAircraftManufacturer.Location = new Point(135, 41);
            comboAircraftManufacturer.Name = "comboAircraftManufacturer";
            comboAircraftManufacturer.Size = new Size(182, 33);
            comboAircraftManufacturer.TabIndex = 2;
            comboAircraftManufacturer.SelectedIndexChanged += comboAircraftManufacturer_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 87);
            label2.Name = "label2";
            label2.Size = new Size(74, 25);
            label2.TabIndex = 3;
            label2.Text = "Modelo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 44);
            label1.Name = "label1";
            label1.Size = new Size(92, 25);
            label1.TabIndex = 2;
            label1.Text = "Fabricante";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblBoardVersion);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(btnConnTest);
            groupBox2.Controls.Add(comboConnSerial);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 442);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(551, 140);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Informações de Conexão";
            // 
            // lblBoardVersion
            // 
            lblBoardVersion.AutoSize = true;
            lblBoardVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBoardVersion.Location = new Point(185, 95);
            lblBoardVersion.Name = "lblBoardVersion";
            lblBoardVersion.Size = new Size(0, 25);
            lblBoardVersion.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 96);
            label4.Name = "label4";
            label4.Size = new Size(144, 25);
            label4.TabIndex = 6;
            label4.Text = "Modelo da Placa";
            // 
            // btnConnTest
            // 
            btnConnTest.Location = new Point(351, 38);
            btnConnTest.Name = "btnConnTest";
            btnConnTest.Size = new Size(169, 33);
            btnConnTest.TabIndex = 3;
            btnConnTest.Text = "Testar Conexão";
            btnConnTest.UseVisualStyleBackColor = true;
            btnConnTest.Click += btnConnTest_Click;
            // 
            // comboConnSerial
            // 
            comboConnSerial.DropDownStyle = ComboBoxStyle.DropDownList;
            comboConnSerial.FormattingEnabled = true;
            comboConnSerial.Location = new Point(135, 38);
            comboConnSerial.Name = "comboConnSerial";
            comboConnSerial.Size = new Size(182, 33);
            comboConnSerial.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 46);
            label3.Name = "label3";
            label3.Size = new Size(100, 25);
            label3.TabIndex = 5;
            label3.Text = "Porta Serial";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblSIMStatus);
            groupBox3.Controls.Add(lblSerialStatus);
            groupBox3.Controls.Add(btnConnectSIM);
            groupBox3.Controls.Add(btnConnectSerial);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Location = new Point(12, 602);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(551, 141);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Iniciar Conexão";
            // 
            // lblSIMStatus
            // 
            lblSIMStatus.AutoSize = true;
            lblSIMStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSIMStatus.ForeColor = Color.Red;
            lblSIMStatus.Location = new Point(407, 94);
            lblSIMStatus.Name = "lblSIMStatus";
            lblSIMStatus.Size = new Size(132, 25);
            lblSIMStatus.TabIndex = 12;
            lblSIMStatus.Text = "Desconectado";
            // 
            // lblSerialStatus
            // 
            lblSerialStatus.AutoSize = true;
            lblSerialStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSerialStatus.ForeColor = Color.Red;
            lblSerialStatus.Location = new Point(407, 46);
            lblSerialStatus.Name = "lblSerialStatus";
            lblSerialStatus.Size = new Size(132, 25);
            lblSerialStatus.TabIndex = 11;
            lblSerialStatus.Text = "Desconectado";
            // 
            // btnConnectSIM
            // 
            btnConnectSIM.Location = new Point(207, 90);
            btnConnectSIM.Name = "btnConnectSIM";
            btnConnectSIM.Size = new Size(169, 33);
            btnConnectSIM.TabIndex = 10;
            btnConnectSIM.Text = "Conectar";
            btnConnectSIM.UseVisualStyleBackColor = true;
            btnConnectSIM.Click += btnConnectSIM_Click;
            // 
            // btnConnectSerial
            // 
            btnConnectSerial.Location = new Point(207, 42);
            btnConnectSerial.Name = "btnConnectSerial";
            btnConnectSerial.Size = new Size(169, 33);
            btnConnectSerial.TabIndex = 8;
            btnConnectSerial.Text = "Conectar";
            btnConnectSerial.UseVisualStyleBackColor = true;
            btnConnectSerial.Click += btnConnectSerial_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 94);
            label7.Name = "label7";
            label7.Size = new Size(57, 25);
            label7.TabIndex = 9;
            label7.Text = "MSFS";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 50);
            label6.Name = "label6";
            label6.Size = new Size(162, 25);
            label6.TabIndex = 8;
            label6.Text = "Placa Controladora";
            // 
            // tmrStillAlive
            // 
            tmrStillAlive.Interval = 60000;
            tmrStillAlive.Tick += tmrStayinAlive_Tick;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cbRadio);
            groupBox4.Controls.Add(cbOverhead);
            groupBox4.Location = new Point(12, 215);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(551, 200);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Módulos Instalados";
            // 
            // cbRadio
            // 
            cbRadio.AutoSize = true;
            cbRadio.Enabled = false;
            cbRadio.Location = new Point(22, 75);
            cbRadio.Name = "cbRadio";
            cbRadio.Size = new Size(84, 29);
            cbRadio.TabIndex = 1;
            cbRadio.Text = "Radio";
            cbRadio.UseVisualStyleBackColor = true;
            // 
            // cbOverhead
            // 
            cbOverhead.AutoSize = true;
            cbOverhead.Enabled = false;
            cbOverhead.Location = new Point(22, 40);
            cbOverhead.Name = "cbOverhead";
            cbOverhead.Size = new Size(115, 29);
            cbOverhead.TabIndex = 0;
            cbOverhead.Text = "Overhead";
            cbOverhead.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(107, 804);
            button1.Name = "button1";
            button1.Size = new Size(209, 49);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 865);
            Controls.Add(button1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CNV Connect";
            Load += frmMain_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private ComboBox comboAircraftModel;
        private ComboBox comboAircraftManufacturer;
        private GroupBox groupBox2;
        private ComboBox comboConnSerial;
        private Label label3;
        private Button btnConnTest;
        private Label label4;
        private Label lblBoardVersion;
        private Label label5;
        private ComboBox comboAircraftSoft;
        private GroupBox groupBox3;
        private Label label7;
        private Label label6;
        private Button btnConnectSerial;
        private Button btnConnectSIM;
        private Label lblSIMStatus;
        private Label lblSerialStatus;
        private System.Windows.Forms.Timer tmrStillAlive;
        private GroupBox groupBox4;
        private CheckBox cbOverhead;
        private CheckBox cbRadio;
        private Button button1;
    }
}
