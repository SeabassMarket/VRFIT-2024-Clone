using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This will allow us to get InputDevice
using UnityEngine.XR;

public class InputReader : MonoBehaviour
{
    //Will store whether input devices are connected or not
    public bool initialized = false;

    //Create variables for right controller input
    public Vector3 rightControllerPosition;
    public float rightTriggerValue;
    public float rightGripValue;
    public bool rightPrimary;
    public bool rightSecondary;

    //Variables for right controller switch algorithm
    private bool previousRightPrimary;
    public bool rightPrimarySwitch = false;
    private bool previousRightSecondary;
    public bool rightSecondarySwitch = false;

    //Create variables for left controller input
    public Vector3 leftControllerPosition;
    public float leftTriggerValue;
    public float leftGripValue;
    public bool leftPrimary;
    public bool leftSecondary;
    public bool menuButton;

    //Variables for left controller switch algorithm
    private bool previousLeftPrimary;
    public bool leftPrimarySwitch = false;
    private bool previousLeftSecondary;
    public bool leftSecondarySwitch = false;
    private bool previousMenuButton;
    public bool menuButtonSwitch = false;

    //Create variables for head input
    public Vector3 headPosition;
    public Quaternion quaternionHeadRotation;
    public Vector3 eulerHeadRotation;

    //Creating a List of Input Devices to store our Input Devices in
    List<InputDevice> inputDevices = new List<InputDevice>();

    //Stores time since that input devices have been unitialized
    private float timeUninitialized = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //We will try to Initialize the InputReader here, but all components may not be loaded
        InitializeInputReader();
    }

    //This will try to initialize the InputReader by getting all the devices and printing them to the debugger.
    void InitializeInputReader()
    {

        InputDevices.GetDevices(inputDevices);

        /*foreach (var inputDevice in inputDevices)
        {
            Debug.Log(inputDevice.name + " " + inputDevice.characteristics);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //We should have a total of 3 Input Devices. If it's less, then we try to initialize them again.
        if (inputDevices.Count < 2)
        {
            InitializeInputReader();
            timeUninitialized = timeUninitialized + Time.deltaTime;
            if (timeUninitialized > 1.0f)
            {
                initialized = false;
            }
            
        } else
        {
            //Record characteristics for right controller
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, inputDevices);
            foreach (var inputDevice in inputDevices)
            {
                inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float temp);
                rightTriggerValue = temp;
                inputDevice.TryGetFeatureValue(CommonUsages.grip, out float temp0);
                rightGripValue = temp0;
                inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 temp1);
                rightControllerPosition = temp1;
                inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool temp2);
                rightPrimary = temp2;
                inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool temp3);
                rightSecondary = temp3;
            }
            if (previousRightPrimary != rightPrimary && rightPrimary)
            {
                if (rightPrimarySwitch)
                {
                    rightPrimarySwitch = false;
                }
                else
                {
                    rightPrimarySwitch = true;
                }
            }
            if (previousRightSecondary != rightSecondary && rightSecondary)
            {
                if (rightSecondarySwitch)
                {
                    rightSecondarySwitch = false;
                } else
                {
                    rightSecondarySwitch = true;
                }
            }

            //Record characteristics for left controller
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices);
            foreach (var inputDevice in inputDevices)
            {
                inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float temp);
                leftTriggerValue = temp;
                inputDevice.TryGetFeatureValue(CommonUsages.grip, out float temp0);
                leftGripValue = temp0;
                inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 temp1);
                leftControllerPosition = temp1;
                inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool temp2);
                leftPrimary = temp2;
                inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool temp3);
                leftSecondary = temp3;
                inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool temp4);
                menuButton = temp4;
            }
            if (previousLeftPrimary != leftPrimary && leftPrimary)
            {
                if (leftPrimarySwitch)
                {
                    leftPrimarySwitch = false;
                } else
                {
                    leftPrimarySwitch = true;
                }
            }
            if (previousLeftSecondary != leftSecondary && leftSecondary)
            {
                if (leftSecondarySwitch)
                {
                    leftSecondarySwitch = false;
                } else
                {
                    leftSecondarySwitch = true;
                }
            }
            if (previousMenuButton != menuButton && menuButton)
            {
                if (menuButtonSwitch)
                {
                    menuButtonSwitch = false;
                } else
                {
                    menuButtonSwitch = true;
                }
            }

            //Record characteristics for head
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, inputDevices);
            foreach (var inputDevice in inputDevices)
            {
                inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 temp);
                headPosition = temp;
                inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion temp0);
                quaternionHeadRotation = temp0;
                eulerHeadRotation = quaternionHeadRotation.eulerAngles;
            }

            //Store values again to be used as reference to previous frame
            previousRightPrimary = rightPrimary;
            previousRightSecondary = rightSecondary;
            previousLeftPrimary = leftPrimary;
            previousLeftSecondary = leftSecondary;
            previousMenuButton = menuButton;
            //show that it is initialized and reset unitialized timer
            initialized = true;
            timeUninitialized = 0;
        }
    }
}
