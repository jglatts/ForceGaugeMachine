/**
 *  MotorHelper UC100 wrapper class                 
 *                      
 *  Copyright: Z-Axis Connector Company
 *  Date:      11/15/23
 *  Author:    John Glatts
 */
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static UC100;

class MotorHelper
{
    private int boardID;
    private int moveID;
    private int stepXPin;
    private int dirXPin;
    private int enaXPin;
    private int stepYPin;
    private int dirYPin;
    private int enaYPin;
    private int xStepsPerUnit;
    private int xHomePin;
    private int leftHomePin;
    private double xPos;
    private double yPos;
    private double zPos;
    private double aPos;
    private double bPos;
    private double cPos;
    public double speed;
    public double feedRate;
    public double cameraOffset;
    private int bladePin;
    private int lockPin;
    public String helpStr;
    public bool useCamera;
    private bool isConnected;
    private bool isBladeDown;
    private double cutLength;
    private int cutQuantity;
    private CancellationTokenSource cancelTokenSource;
    private CancellationToken token;

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
        moveID = 0;
        boardID = 1;
        speed = 5.5;
        feedRate = 0.85;
        stepXPin = 2;               // prod for autocutter
        dirXPin = 3;
        enaXPin = 5;
        bladePin = 8;               // 8 for autocutter2
        lockPin = 13;
        xHomePin = 0;            // right limit switch
        leftHomePin = 15;           // left limit switch
        //xStepsPerUnit = 8000;
        xStepsPerUnit = 10724;
        xPos = 0.0;
        yPos = 0.0;
        zPos = 0.0;
        aPos = 0.0;
        bPos = 0.0;
        cPos = 0.0;
        cameraOffset = 0.0;
        useCamera = false;
        isConnected = false;
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
     *  Drive the blade up
     */
    public bool bladeUp()
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        if (SetOutputBit(bladePin) != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
    }

    /**
     *  Drive the blade down
     */
    public bool bladeDown()
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        /*
        if (getPinState(lockPin))
        {
            MessageBox.Show("Check Locking Pins", "Z-Axis Connector Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
        */

        int ret = ClearOutputBit(bladePin);
        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
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
        bladeUp();

        return ret;
    }

    /**
     *  Set a motor axis with UC100
     *  Will enable the motors on completion
     */
    private bool setMotorAxis()
    {
        int ret = 0;

        AxisSetting xAxisSetting = getAxisSetup(0, stepXPin, dirXPin, enaXPin, xHomePin, xStepsPerUnit, true, 1, 1000.0);
        ret = SetAxisSetting(ref xAxisSetting);

        if (ret != (int)ReturnVal.UC100_OK)
        {
            return false;
        }

        return true;
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
     *  Driver method start cut-many routine
     */
    public void makeCuts(int quantity, double length)
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr + "\nyoure not connected!?!", "Z-Axis Connector Company");
            return;
        }

        cutLength = length;
        cutQuantity = quantity;
        startCutThread();
    }

    /**
     *  Helper method to configure cut-many thread and cancel token
     */
    private void startCutThread()
    {
        cancelTokenSource = new CancellationTokenSource();
        token = cancelTokenSource.Token;
        /* Test this impl.
        token.Register(() =>
        {
            Stop();
            return;
        });
        */
        Task task = new Task(doCutMove, token);
        task.Start();
    }

    /**
     *  Method for cut-many thread
     *  Will make cutQuantity cuts
     */
    private void doCutMove()
    {
        for (int i = 0; i < cutQuantity; i++)
        {
            // is main-thread saying to stop?
            if (token.IsCancellationRequested)
            {
                Stop();
                return;
            }

            // compensate for offset, move to cut
            if (useCamera)
            {
                doMotorMove(cameraOffset, false);
                while (isMotorRunning()) ; // NOP
            }

            // first cut
            if (!bladeDown())
            {
                errorLoop();
            }

            // blade up settings
            Thread.Sleep(500);
            bladeUp();
            Thread.Sleep(500);

            // go back if using camera
            if (useCamera)
            {
                doMotorMove(cameraOffset, true);
            }

            // move to next position
            doMotorMove(cutLength, true);
            while (isMotorRunning()) ; // NOP
        }
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
        //doMotorMove(4.5, true);     // move to home position
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

    public void doMotorMove(double steps, bool dir)
    {
        /* 
        if (!getPinState(leftHomePin)) {
            // need a better stop here, maybe multi-threaded
            // could have a thread that listens for pin going low 
            bladeUp();
            AddLinearMoveRel(0, 1.0, 1, speed, false);
            return;
        }
        */

        int ret = AddLinearMoveRel(0, steps, 1, speed, dir);
        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return;
        }

        /*
        // need a better check, multi-threaded?
        if (!getPinState(leftHomePin))
        {
            AddLinearMoveRel(0, 1.0, 1, speed, false);
            return;
        }
        */
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