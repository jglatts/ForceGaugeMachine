/**
 *  Automated Force vs Deflection Machine
 *                      
 *  Copyright: Z-Axis Connector Company
 *  Date:      2/22/24
 *  Author:    John Glatts
 */
namespace ForceGaugeMachine
{
    public partial class Form1 : Form
    {

        private MotorHelper motorHelper;
        private double currentPos;
        private bool isPaused;
        private bool testActive;
        delegate void SetTextCallback(double pos);
        delegate void SetTextCallbackDelay(int value);
        delegate void SetTextCallbackClear();

        public Form1()
        {
            InitializeComponent();
            motorHelper = new MotorHelper(this);
            currentPos = 0.0;
            txtBoxCurrentPos.ReadOnly = true;
            txtBoxDelayInterval.Text = "1.5";
            txtBoxCurrentPos.Text = "0.000";
            isPaused = false;
            testActive = false;
            setHelpButton();
            motorHelper.setTestDelayInterval(1.5);   
        }

        private void Form1_HelpButtonClicked(object sender, EventArgs e) 
        {
            string helpStr = "Automatic Force vs Deflection Machine\n\n";
            helpStr += "Be sure to remove any backlash from the system before starting a test.\n";
            helpStr += "To remove the backlash:\n";
            helpStr += "    * Find the zero position with the connector and gauge assembly\n";
            helpStr += "    * Move .03\" in the DOWN direction\n";
            helpStr += "    * Manually move .03\" in the UP direction to remove backlash\n";
            helpStr += "    * Begin test\n";
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
        }


        private void setHelpButton()
        {
            this.HelpButtonClicked += Form1_HelpButtonClicked;
        }

        private void btnOpenDevice_Click(object sender, EventArgs e)
        {
            if (!motorHelper.setDevice())
                MessageBox.Show(motorHelper.helpStr, "Z-Axis Connector Company");
            else
                MessageBox.Show("UC100 Connected", "Z-Axis Connector Company");
        }

        public void updateCurrentPosition(double pos)
        {
            if (this.txtBoxCurrentPos.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(updateCurrentPosition);
                this.Invoke(d, new object[] { pos });
            }
            else
            {
                if (pos == 0)
                {
                    currentPos = 0.0;
                }
                else
                {
                    currentPos = Math.Round(currentPos + pos, 5);
                }
                txtBoxCurrentPos.Text = currentPos.ToString();
            }
        }

        private void btnCloseDevice_Click(object sender, EventArgs e)
        {
            motorHelper.closeDevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.5, true);
            updateCurrentPosition(-0.5);
        }

        private void btnMoveQuartInchLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.25, true);
            updateCurrentPosition(-0.25);
        }

        private void btnMove10ThouLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.01, true);
            updateCurrentPosition(-.01); 
        }

        private void btnMoveOneMillLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.001, true);
            updateCurrentPosition(-.001);
        }

        private void btnMoveHalfInchRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.5, false);
            updateCurrentPosition(0.5);
        }

        private void btnMoveQuartInchRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.25, false);
            updateCurrentPosition(0.25);
        }

        private void btnMove10ThouRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.01, false);
            updateCurrentPosition(0.01);
        }

        private void btnMoveOneMillRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.001, false);
            updateCurrentPosition(.001);
        }

        private void btnStopMotor_Click(object sender, EventArgs e)
        {
            motorHelper.stopMotor(this);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnEndTest_Click(object sender, EventArgs e)
        {
            motorHelper.stopMotor(this);
            clearProgBar();
        }

        public void clearProgBar()
        {
            try
            {
                if (this.progressBarTestInterval.InvokeRequired)
                {
                    SetTextCallbackClear d = new SetTextCallbackClear(clearProgBar);
                    this.Invoke(d);
                }
                else
                {
                    progressBarTestInterval.Value = 0;
                }
            }
            catch(Exception ex) { }
        }

        private void btnRunDeflectionTest_Click(object sender, EventArgs e)
        {
            double totalDeflection = 0;
            double deflectionInterval = 0;
            double delayInterval = 0;

            if (!Double.TryParse(txtBoxTotalDeflection.Text, out totalDeflection) ||
                !Double.TryParse(txtBoxDeflectionInterval.Text, out deflectionInterval) ||
                !Double.TryParse(txtBoxDelayInterval.Text, out delayInterval))
            {
                MessageBox.Show("error with data!", "Z-Axis Connector Company");
                return;
            }

            if (!checkUserInput(delayInterval, deflectionInterval, totalDeflection))
            {
                return;
            }

            progressBarTestInterval.Maximum = ((int)(delayInterval * 100));
            progressBarTestInterval.Step = 1;
            progressBarTestInterval.Value = 0;
            motorHelper.setTestDelayInterval(delayInterval);
            testActive = true;
            motorHelper.runForceDeflectionTest(totalDeflection, deflectionInterval, this);
        }

        public void setTestState(bool isActive)
        { 
            testActive = isActive; 
        }
         
        public bool getPaused()
        {
            return isPaused;
        }

        public void updateProgressBar(int value)
        {
            if (this.progressBarTestInterval.InvokeRequired)
            {
                SetTextCallbackDelay d = new SetTextCallbackDelay(updateProgressBar);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                int previous = progressBarTestInterval.Value;
                progressBarTestInterval.Value = previous + value;
                return;
                //sMessageBox.Show("val " + progressBarTestInterval.Value + "\ncomp: " + (previous + value));
            }
        }

        private bool checkUserInput(double delayInterval, double deflectionInterval, double totalDeflection) {
            if (deflectionInterval >= totalDeflection)
            {
                MessageBox.Show("error:\ndeflectionInterval >= totalDeflection!", "Z-Axis Connector Company");
                return false;
            }

            if (delayInterval <= 0)
            {
                MessageBox.Show("error:\ndelayInterval <= 0", "Z-Axis Connector Company");
                return false;
            }

            return true;
        }

        private void txtBoxDeflectionInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnResetHome_Click(object sender, EventArgs e)
        {
            currentPos = 0.0;
            txtBoxCurrentPos.Text = "0.000";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            if (isPaused) 
            {
                testActive = true;
                isPaused = false;
                btnPauseTest.Text = "Pause Test";

            }
            else if (testActive) 
            {
                testActive = false;
                isPaused = true;
                btnPauseTest.Text = "Resume Test";
            }
        }
    }
}
