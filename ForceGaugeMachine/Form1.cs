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
        
        public Form1()
        {
            InitializeComponent();
            motorHelper= new MotorHelper();
            //motorHelper.setDevice();
        }

        private void btnOpenDevice_Click(object sender, EventArgs e)
        {
            if (!motorHelper.setDevice())
                MessageBox.Show(motorHelper.helpStr, "Z-Axis Connector Company");
            else
                MessageBox.Show("UC100 Connected", "Z-Axis Connector Company");
        }

        private void btnCloseDevice_Click(object sender, EventArgs e)
        {
            motorHelper.closeDevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.5, true);
        }

        private void btnMoveQuartInchLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.25, true);
        }

        private void btnMove10ThouLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.01, true);
        }

        private void btnMoveOneMillLeft_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.001, true);
        }

        private void btnMoveHalfInchRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.5, false);
        }

        private void btnMoveQuartInchRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.25, false);
        }

        private void btnMove10ThouRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.01, false);
        }

        private void btnMoveOneMillRight_Click(object sender, EventArgs e)
        {
            motorHelper.doMotorMove(0.001, false);
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
                motorHelper.runForceDeflectionTest(totalDeflection, deflectionInterval);
            }
            catch
            {
                MessageBox.Show("error with data!", "Z-Axis Connector Company");
            }

        }
    }
}
