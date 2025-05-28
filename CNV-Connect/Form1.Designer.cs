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
            groupBox1 = new GroupBox();
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
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboAircraftModel);
            groupBox1.Controls.Add(comboAircraftManufacturer);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(551, 173);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Modelo de Aeronave";
            // 
            // comboAircraftModel
            // 
            comboAircraftModel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAircraftModel.FormattingEnabled = true;
            comboAircraftModel.Items.AddRange(new object[] { "A318", "A319", "A320", "A321" });
            comboAircraftModel.Location = new Point(135, 84);
            comboAircraftModel.Name = "comboAircraftModel";
            comboAircraftModel.Size = new Size(182, 33);
            comboAircraftModel.TabIndex = 4;
            // 
            // comboAircraftManufacturer
            // 
            comboAircraftManufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAircraftManufacturer.FormattingEnabled = true;
            comboAircraftManufacturer.Items.AddRange(new object[] { "Airbus", "Boeing", "Bombardier", "Cessna", "Embraer" });
            comboAircraftManufacturer.Location = new Point(135, 41);
            comboAircraftManufacturer.Name = "comboAircraftManufacturer";
            comboAircraftManufacturer.Size = new Size(182, 33);
            comboAircraftManufacturer.TabIndex = 2;
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
            groupBox2.Location = new Point(12, 204);
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
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1158, 593);
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
    }
}
