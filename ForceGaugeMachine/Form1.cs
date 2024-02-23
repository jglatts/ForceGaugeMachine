/**
 *  Automated Force vs Deflection Machine
 *  
 *  left down
 *  right UP
 *  
 *  ToDo:
 *      - implement current pos txtbox to keep track of where we are
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
        delegate void SetTextCallback(double pos);

        public Form1()
        {
            InitializeComponent();
            motorHelper = new MotorHelper();
            currentPos = 0.0;
            txtBoxCurrentPos.ReadOnly = true;
            //motorHelper.setDevice();
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
            motorHelper.stopMotor();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnEndTest_Click(object sender, EventArgs e)
        {
            motorHelper.stopMotor();
        }

        private void btnRunDeflectionTest_Click(object sender, EventArgs e)
        {
            double totalDeflection;
            double deflectionInterval;

            try 
            {
                totalDeflection = Double.Parse(txtBoxTotalDeflection.Text);
                deflectionInterval = Double.Parse(txtBoxDeflectionInterval.Text);
                motorHelper.runForceDeflectionTest(totalDeflection, deflectionInterval, this);
            }
            catch
            {
                MessageBox.Show("error with data!", "Z-Axis Connector Company");
            }

        }

        private void txtBoxDeflectionInterval_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
