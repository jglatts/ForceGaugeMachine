namespace ForceGaugeMachine
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenDevice = new System.Windows.Forms.Button();
            this.btnCloseDevice = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnResetHome = new System.Windows.Forms.Button();
            this.btnStopMotor = new System.Windows.Forms.Button();
            this.btnMoveOneMillRight = new System.Windows.Forms.Button();
            this.btnMove10ThouRight = new System.Windows.Forms.Button();
            this.btnMoveQuartInchRight = new System.Windows.Forms.Button();
            this.btnMoveHalfInchRight = new System.Windows.Forms.Button();
            this.btnMoveOneMillLeft = new System.Windows.Forms.Button();
            this.btnMove10ThouLeft = new System.Windows.Forms.Button();
            this.btnMoveQuartInchLeft = new System.Windows.Forms.Button();
            this.btnMoveHalfInchLeft = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEndTest = new System.Windows.Forms.Button();
            this.btnRunDeflectionTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxDeflectionInterval = new System.Windows.Forms.TextBox();
            this.txtBoxTotalDeflection = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(45, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(482, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z-Axis Connector Company";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(117, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Automated Force v Deflection";
            // 
            // btnOpenDevice
            // 
            this.btnOpenDevice.Location = new System.Drawing.Point(117, 205);
            this.btnOpenDevice.Name = "btnOpenDevice";
            this.btnOpenDevice.Size = new System.Drawing.Size(166, 58);
            this.btnOpenDevice.TabIndex = 3;
            this.btnOpenDevice.Text = "Open UC100";
            this.btnOpenDevice.UseVisualStyleBackColor = true;
            this.btnOpenDevice.Click += new System.EventHandler(this.btnOpenDevice_Click);
            // 
            // btnCloseDevice
            // 
            this.btnCloseDevice.Location = new System.Drawing.Point(217, 41);
            this.btnCloseDevice.Name = "btnCloseDevice";
            this.btnCloseDevice.Size = new System.Drawing.Size(166, 58);
            this.btnCloseDevice.TabIndex = 4;
            this.btnCloseDevice.Text = "Close UC100";
            this.btnCloseDevice.UseVisualStyleBackColor = true;
            this.btnCloseDevice.Click += new System.EventHandler(this.btnCloseDevice_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCloseDevice);
            this.groupBox1.Location = new System.Drawing.Point(94, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 130);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UC100 Control";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnResetHome);
            this.groupBox2.Controls.Add(this.btnStopMotor);
            this.groupBox2.Controls.Add(this.btnMoveOneMillRight);
            this.groupBox2.Controls.Add(this.btnMove10ThouRight);
            this.groupBox2.Controls.Add(this.btnMoveQuartInchRight);
            this.groupBox2.Controls.Add(this.btnMoveHalfInchRight);
            this.groupBox2.Controls.Add(this.btnMoveOneMillLeft);
            this.groupBox2.Controls.Add(this.btnMove10ThouLeft);
            this.groupBox2.Controls.Add(this.btnMoveQuartInchLeft);
            this.groupBox2.Controls.Add(this.btnMoveHalfInchLeft);
            this.groupBox2.Location = new System.Drawing.Point(94, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 326);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motor Control";
            // 
            // btnResetHome
            // 
            this.btnResetHome.Location = new System.Drawing.Point(26, 262);
            this.btnResetHome.Name = "btnResetHome";
            this.btnResetHome.Size = new System.Drawing.Size(163, 45);
            this.btnResetHome.TabIndex = 16;
            this.btnResetHome.Text = "Reset Home";
            this.btnResetHome.UseVisualStyleBackColor = true;
            // 
            // btnStopMotor
            // 
            this.btnStopMotor.Location = new System.Drawing.Point(220, 262);
            this.btnStopMotor.Name = "btnStopMotor";
            this.btnStopMotor.Size = new System.Drawing.Size(163, 45);
            this.btnStopMotor.TabIndex = 15;
            this.btnStopMotor.Text = "&Stop Motor";
            this.btnStopMotor.UseVisualStyleBackColor = true;
            this.btnStopMotor.Click += new System.EventHandler(this.btnStopMotor_Click);
            // 
            // btnMoveOneMillRight
            // 
            this.btnMoveOneMillRight.Location = new System.Drawing.Point(217, 194);
            this.btnMoveOneMillRight.Name = "btnMoveOneMillRight";
            this.btnMoveOneMillRight.Size = new System.Drawing.Size(166, 41);
            this.btnMoveOneMillRight.TabIndex = 14;
            this.btnMoveOneMillRight.Text = ">";
            this.btnMoveOneMillRight.UseVisualStyleBackColor = true;
            this.btnMoveOneMillRight.Click += new System.EventHandler(this.btnMoveOneMillRight_Click);
            // 
            // btnMove10ThouRight
            // 
            this.btnMove10ThouRight.Location = new System.Drawing.Point(217, 147);
            this.btnMove10ThouRight.Name = "btnMove10ThouRight";
            this.btnMove10ThouRight.Size = new System.Drawing.Size(166, 41);
            this.btnMove10ThouRight.TabIndex = 13;
            this.btnMove10ThouRight.Text = ">>";
            this.btnMove10ThouRight.UseVisualStyleBackColor = true;
            this.btnMove10ThouRight.Click += new System.EventHandler(this.btnMove10ThouRight_Click);
            // 
            // btnMoveQuartInchRight
            // 
            this.btnMoveQuartInchRight.Location = new System.Drawing.Point(217, 100);
            this.btnMoveQuartInchRight.Name = "btnMoveQuartInchRight";
            this.btnMoveQuartInchRight.Size = new System.Drawing.Size(166, 41);
            this.btnMoveQuartInchRight.TabIndex = 12;
            this.btnMoveQuartInchRight.Text = ">>>";
            this.btnMoveQuartInchRight.UseVisualStyleBackColor = true;
            this.btnMoveQuartInchRight.Click += new System.EventHandler(this.btnMoveQuartInchRight_Click);
            // 
            // btnMoveHalfInchRight
            // 
            this.btnMoveHalfInchRight.Location = new System.Drawing.Point(217, 53);
            this.btnMoveHalfInchRight.Name = "btnMoveHalfInchRight";
            this.btnMoveHalfInchRight.Size = new System.Drawing.Size(166, 41);
            this.btnMoveHalfInchRight.TabIndex = 11;
            this.btnMoveHalfInchRight.Text = ">>>>";
            this.btnMoveHalfInchRight.UseVisualStyleBackColor = true;
            this.btnMoveHalfInchRight.Click += new System.EventHandler(this.btnMoveHalfInchRight_Click);
            // 
            // btnMoveOneMillLeft
            // 
            this.btnMoveOneMillLeft.Location = new System.Drawing.Point(23, 194);
            this.btnMoveOneMillLeft.Name = "btnMoveOneMillLeft";
            this.btnMoveOneMillLeft.Size = new System.Drawing.Size(166, 41);
            this.btnMoveOneMillLeft.TabIndex = 10;
            this.btnMoveOneMillLeft.Text = "<";
            this.btnMoveOneMillLeft.UseVisualStyleBackColor = true;
            this.btnMoveOneMillLeft.Click += new System.EventHandler(this.btnMoveOneMillLeft_Click);
            // 
            // btnMove10ThouLeft
            // 
            this.btnMove10ThouLeft.Location = new System.Drawing.Point(23, 147);
            this.btnMove10ThouLeft.Name = "btnMove10ThouLeft";
            this.btnMove10ThouLeft.Size = new System.Drawing.Size(166, 41);
            this.btnMove10ThouLeft.TabIndex = 9;
            this.btnMove10ThouLeft.Text = "<<";
            this.btnMove10ThouLeft.UseVisualStyleBackColor = true;
            this.btnMove10ThouLeft.Click += new System.EventHandler(this.btnMove10ThouLeft_Click);
            // 
            // btnMoveQuartInchLeft
            // 
            this.btnMoveQuartInchLeft.Location = new System.Drawing.Point(23, 100);
            this.btnMoveQuartInchLeft.Name = "btnMoveQuartInchLeft";
            this.btnMoveQuartInchLeft.Size = new System.Drawing.Size(166, 41);
            this.btnMoveQuartInchLeft.TabIndex = 8;
            this.btnMoveQuartInchLeft.Text = "<<<";
            this.btnMoveQuartInchLeft.UseVisualStyleBackColor = true;
            this.btnMoveQuartInchLeft.Click += new System.EventHandler(this.btnMoveQuartInchLeft_Click);
            // 
            // btnMoveHalfInchLeft
            // 
            this.btnMoveHalfInchLeft.Location = new System.Drawing.Point(23, 53);
            this.btnMoveHalfInchLeft.Name = "btnMoveHalfInchLeft";
            this.btnMoveHalfInchLeft.Size = new System.Drawing.Size(166, 41);
            this.btnMoveHalfInchLeft.TabIndex = 7;
            this.btnMoveHalfInchLeft.Text = "<<<<";
            this.btnMoveHalfInchLeft.UseVisualStyleBackColor = true;
            this.btnMoveHalfInchLeft.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEndTest);
            this.groupBox3.Controls.Add(this.btnRunDeflectionTest);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtBoxDeflectionInterval);
            this.groupBox3.Controls.Add(this.txtBoxTotalDeflection);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(658, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 299);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Force vs Deflection Test";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // btnEndTest
            // 
            this.btnEndTest.Location = new System.Drawing.Point(129, 240);
            this.btnEndTest.Name = "btnEndTest";
            this.btnEndTest.Size = new System.Drawing.Size(219, 34);
            this.btnEndTest.TabIndex = 18;
            this.btnEndTest.Text = "End Test";
            this.btnEndTest.UseVisualStyleBackColor = true;
            this.btnEndTest.Click += new System.EventHandler(this.btnEndTest_Click);
            // 
            // btnRunDeflectionTest
            // 
            this.btnRunDeflectionTest.Location = new System.Drawing.Point(129, 169);
            this.btnRunDeflectionTest.Name = "btnRunDeflectionTest";
            this.btnRunDeflectionTest.Size = new System.Drawing.Size(219, 55);
            this.btnRunDeflectionTest.TabIndex = 17;
            this.btnRunDeflectionTest.Text = "Start Test";
            this.btnRunDeflectionTest.UseVisualStyleBackColor = true;
            this.btnRunDeflectionTest.Click += new System.EventHandler(this.btnRunDeflectionTest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Deflection Interval";
            // 
            // txtBoxDeflectionInterval
            // 
            this.txtBoxDeflectionInterval.Location = new System.Drawing.Point(199, 99);
            this.txtBoxDeflectionInterval.Name = "txtBoxDeflectionInterval";
            this.txtBoxDeflectionInterval.Size = new System.Drawing.Size(191, 31);
            this.txtBoxDeflectionInterval.TabIndex = 2;
            // 
            // txtBoxTotalDeflection
            // 
            this.txtBoxTotalDeflection.Location = new System.Drawing.Point(199, 55);
            this.txtBoxTotalDeflection.Name = "txtBoxTotalDeflection";
            this.txtBoxTotalDeflection.Size = new System.Drawing.Size(191, 31);
            this.txtBoxTotalDeflection.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total Deflection";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 738);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOpenDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Z-Axis Connector Company";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnOpenDevice;
        private Button btnCloseDevice;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnStopMotor;
        private Button btnMoveOneMillRight;
        private Button btnMove10ThouRight;
        private Button btnMoveQuartInchRight;
        private Button btnMoveHalfInchRight;
        private Button btnMoveOneMillLeft;
        private Button btnMove10ThouLeft;
        private Button btnMoveQuartInchLeft;
        private Button btnMoveHalfInchLeft;
        private Button btnResetHome;
        private GroupBox groupBox3;
        private Label label4;
        private TextBox txtBoxDeflectionInterval;
        private TextBox txtBoxTotalDeflection;
        private Label label3;
        private Button btnRunDeflectionTest;
        private Button btnEndTest;
    }
}