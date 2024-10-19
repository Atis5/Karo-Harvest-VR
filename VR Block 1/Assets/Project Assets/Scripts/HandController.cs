using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


// To determine which controller the script is related to
public enum HandType
{
    Left,
    Right
}
public class HandController : MonoBehaviour
{

    public HandType handType;

    private Animator animator;
    private InputDevice inputDevice;

    private float grabValue;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    // Required to read input from the controller
    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        if (handType == HandType.Left)
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);

        return inputDevices[0];
    }

    // Reads the input and applies corresponding animation to the hand object
    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out grabValue);

        animator.SetFloat("Grab", grabValue);
    }
}
