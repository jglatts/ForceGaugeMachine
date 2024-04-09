/**
 *  MotorHelper UC100 wrapper class                 
 *                      
 *  Copyright: Z-Axis Connector Company
 *  Date:      11/15/23
 *  Author:    John Glatts
 */
using System.Reflection.Metadata.Ecma335;
using static UC100;

class MotorHelper
{
    private int boardID;
    private int stepXPin;
    private int dirXPin;
    private int enaXPin;
    private int xStepsPerUnit;
    private int xHomePin;
    public double speed;
    public double feedRate;
    public String helpStr;
    private bool isConnected;
    private double totalDeflection;
    private double deflectionInterval;
    private double testDelayInterval;
    private CancellationTokenSource cancelTokenSource;
    private CancellationToken token;
    private ForceGaugeMachine.Form1 mainForm;

    /**
     *  Constructor 
     */
    public MotorHelper()
    {
        setMotorVars();
        setHelpStr();
    }

    /**
     *  Set the needed motor variables/pins
     */
    private void setMotorVars()
    {
        boardID = 1;
        speed = 5.5;
        feedRate = 0.85;
        stepXPin = 2;               // prod for autocutter
        dirXPin = 3;
        enaXPin = 5;
        xHomePin = 0;            // right limit switch
        //xStepsPerUnit = 8000;
        xStepsPerUnit = 10724;
        isConnected = false;
    }

    public void setTestDelayInterval(double delay)
    {
        testDelayInterval = delay;
    }

    /**
     *  Set the error help string
     */
    private void setHelpStr()
    {
        helpStr = "ERROR WITH DEVICE!\nIS DEVICE CONNECTED?\n\nFOLLOW STEPS BELOW\n";
        helpStr += "1. Make sure the UC100 is connected to the PC\n2. Hit 'Open Device'";
    }

    /**
     *  Enable the motor
     */
    public bool enableMotor()
    {
        return setMotorAxis();
    }

    /**
     *  Return the state of an input pin
     *  
     *  Returns TRUE on HI and FALSE on LOW
     */
    private bool getPinState(int pin)
    {
        // to keep same-pinmapping, tie the input to solenoid-lock-pin, will go low-high
        // UC100 uses pull-ups on the input pins
        // https://cncdrive.com/MC/UC100%20datasheet/UC100%20users%20guide.pdf
        int[] pinMap = { 10, 11, 12, 13, 15 };
        int bitPin = 0;
        long input = 0L;

        if (pin > 15 || pin < 10 || pin == 14)
        {
            return false;
        }

        for (int i = 0; i < pinMap.Length; i++)
        {
            if (pin == pinMap[i])
            {
                bitPin = i;
                break;
            }
        }

        GetInput(ref input);
        return (input & (1 << bitPin)) != 0;
    }

    /**
     *  Helper method to check all input pins
     */
    public void checkAllInputPins()
    {
        String s = "Pin #1 state= " + getPinState(1).ToString() + "\n";
        for (int i = 10; i <= 15; i++)
        {
            s += "Pin #" + i + " state= " + getPinState(i).ToString() + "\n";
        }
        MessageBox.Show(s, "Z-Axis Connector Company");
    }

    /**
     *  Open and being comms. with UC100 device
     */
    public bool setDevice()
    {
        bool ret = true;

        ret = listDevices();
        if (!ret)
            return ret;

        ret = getDeviceInfo();
        if (!ret)
            return ret;

        ret = deviceOpen();
        if (!ret)
            return ret;

        isConnected = true;
        setMotorAxis();

        return ret;
    }

    /**
     *  Set a motor axis with UC100
     *  Will enable the motors on completion
     */
    private bool setMotorAxis()
    {
        int ret = 0;

        AxisSetting xAxisSetting = getAxisSetup(0, stepXPin, dirXPin, enaXPin, xHomePin, xStepsPerUnit, true, 1.0, 1000.0);
        ret = SetAxisSetting(ref xAxisSetting);

        if (ret != (int)ReturnVal.UC100_OK)
        {
            return false;
        }

        return true;
    }

    private bool checkForBacklashCompensated()
    {
        DialogResult dialogResult = MessageBox.Show("Has backlash been accounted for?", "Z-Axis Connector Company", MessageBoxButtons.YesNo);
        return dialogResult == DialogResult.Yes;
    }

    public void runForceDeflectionTest(double totalDeflection, double deflectionInterval, ForceGaugeMachine.Form1 mainForm) 
    {
        /*
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return;
        }
        */
        cancelTokenSource = new CancellationTokenSource();
        token = cancelTokenSource.Token;
        this.totalDeflection = totalDeflection;
        this.deflectionInterval = deflectionInterval;
        this.mainForm = mainForm;
        Task task = new Task(forceDeflectionWorker, token);
        task.Start();
    }

    private void forceDeflectionWorker() { 
        double moves = totalDeflection / deflectionInterval;
        int delay = (int)(testDelayInterval * 1000);

        // prompt user for backlash compensation
        if (!checkForBacklashCompensated())
        {
            MessageBox.Show("Please compensate for backlash.\n\nHit the \"?\" icon for more info.\n", "Z-Axis Connector Company");
            Stop();
            return;
        }

        // do the stepper moves
        mainForm.updateCurrentPosition(0);
        Thread.Sleep(500); 
        for (int i = 0; i < (int)moves; i++) {
            if (token.IsCancellationRequested)
            {
                Stop();
                mainForm.clearProgBar();
                return;
            }

            if (!doMotorMove(deflectionInterval, false))
            {
                mainForm.clearProgBar();
                return;
            }

            // do the interval delay and update GUI
            mainForm.updateCurrentPosition(deflectionInterval);
            for (int j = 0; j < delay; j++) {
                Thread.Sleep(1);
                mainForm.updateProgressBar(1);
            }
            mainForm.clearProgBar();
        }
    }

    /**
     *  Helper method to configure motor axis parameters
     */
    private AxisSetting getAxisSetup(int axis, int step, int dir, int enable, int home, int stepsPerUnit, bool isEnabled, double accel, double vel)
    {
        AxisSetting axisSetting = new AxisSetting
        {
            Axis = axis,
            Enable = isEnabled,
            StepPin = step,
            DirPin = dir,
            StepNeg = false,
            DirNeg = false,
            MaxAccel = accel,            // max machine acceleration
            MaxVel = vel,                // max machine velocity
            StepPer = stepsPerUnit,
            HomePin = home,              // limit home pin
            HomeNeg = false,
            LimitPPin = 0,
            LimitNPin = 0,
            LimitNNeg = false,           // try setting this guy for the right limit switch, then wont have to poll\check
            SoftLimitP = 0,
            SoftLimitN = 0,
            SlaveAxis = 0,
            BacklashOn = false,
            BacklashDist = 0,
            CompAccel = 0,
            EnablePin = enable,
            EnablePinNeg = false,
            EnableDelay = 0,
            CurrentHiLowPin = 0,
            CurrentHiLowPinNeg = false,
            HomeBackOff = 0,
            RotaryAxis = false,
            RotaryRollover = false,
        };

        return axisSetting;
    }

    /**
     *  Check and return motor running status
     */
    private bool isMotorRunning()
    {
        Stat s = new Stat { };
        int ret = GetStatus(ref s);
        return s.Idle == false;
    }

    /**
     *  Check and return motor homing status
     */
    private bool isMotorHoming()
    {
        Stat s = new Stat { };
        int ret = GetStatus(ref s);
        return s.Home == true;
    }

    /**
     *  Forever loop used for erroneous threads
     */
    private void errorLoop()
    {
        while (true) ;   // NOP
    }

    /**
     *  Not implemented / not used
     */
    public bool setOutputPins()
    {
        return true;
    }

    /**
     *  Close the UC100 device
     */
    public bool closeDevice()
    {
        if (Close() != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }
        isConnected = false;

        return true;
    }

    /**
     *  Home the machine
     */
    public bool resetHome()
    {
        // bad impl. cant hit STOP here, blocks
        int ret = HomeOn(0, 1.0, 0.25, true);

        if (ret == (int)ReturnVal.UC100_MOVEMENT_IN_PROGRESS)
        {
            MessageBox.Show("Movement in Progress!\nHit 'Stop' then try again", "Z-Axis Connector Company");
            return false;
        }

        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(ret.ToString() + "\n" + helpStr, "Z-Axis Connector Company");
            return false;
        }

        while (isMotorHoming()) ;    // wait for motor to home
        doMotorMove(2.375, true);     // move to home position

        return true;
    }

    private bool getDeviceInfo()
    {
        int type = 0;
        int serialNumber = 0;
        int ret = DeviceInfo(boardID, ref type, ref serialNumber);
        return ret == (int)ReturnVal.UC100_OK;
    }

    private bool listDevices()
    {
        int devices = 0;
        return ListDevices(ref devices) == (int)ReturnVal.UC100_OK;
    }

    private bool deviceOpen()
    {
        return Open(1) == (int)ReturnVal.UC100_OK;
    }

    public bool doMotorMove(double steps, bool dir)
    {
        int ret = AddLinearMoveRel(0, steps, 1, speed, dir);
        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }
        return true;
    }

    public void stopMotor()
    {
        try
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
            int ret = Stop();
            if (ret != (int)ReturnVal.UC100_OK)
            {
                MessageBox.Show(helpStr, "Z-Axis Connector Company");
            }
        }
        catch (Exception ex) { }
    }

    public bool disableMotor()
    {
        int ret = 0;

        AxisSetting xAxisSetting = getAxisSetup(0, stepXPin, dirXPin, enaXPin, xHomePin, xStepsPerUnit, false, 0.5, 1000);

        ret = SetAxisSetting(ref xAxisSetting);
        if (ret == (int)ReturnVal.UC100_MOVEMENT_IN_PROGRESS)
        {
            MessageBox.Show("Movement in Progress!\nHit 'Stop' then try again", "Z-Axis Connector Company");
            return false;
        }

        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
    }

}