using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialKnob;
    public Transform dialKnobTransform;

    [Header("Events")]
    public UnityEvent dialLeft;
    public UnityEvent dialLeftOnce;
    public UnityEvent dialUp;
    public UnityEvent dialUpOnce;
    public UnityEvent dialRight;
    public UnityEvent dialRightOnce;

    [Header("Other")]
    private bool dialLeftCalled = false;
    private bool dialUpCalled = true;
    private bool dialRightCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        dialKnobTransform = dialKnob.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dialKnobTransform.localEulerAngles.y);
        CallDialLeft();
        CallDialLeftOnce();
        CallDialUp();
        CallDialUpOnce();
        CallDialRight();
        CallDialRightOnce();
    }

    private void CallDialLeft()
    {
        if (dialKnobTransform.localEulerAngles.y < 30)
        {
            dialLeft.Invoke();
        }
    }

    private void CallDialLeftOnce()
    {
        if (dialKnobTransform.localEulerAngles.y < 30 && !dialLeftCalled)
        {
            dialLeftOnce.Invoke();
            dialLeftCalled = true;
            dialUpCalled = false;
            dialRightCalled = false;
        }
    }

    private void CallDialUp()
    {
        if (dialKnobTransform.localEulerAngles.y >= 75 && dialKnobTransform.localEulerAngles.y <= 105)
        {
            dialUp.Invoke();
        }
    }

    private void CallDialUpOnce()
    {
        if (dialKnobTransform.localEulerAngles.y >= 75 && dialKnobTransform.localEulerAngles.y <= 105 && !dialUpCalled)
        {
            dialUpOnce.Invoke();
            dialLeftCalled = false;
            dialUpCalled = true;
            dialRightCalled = false;
        }
    }

    private void CallDialRight()
    {
        if (dialKnobTransform.localEulerAngles.y > 150)
        {
            dialRight.Invoke();
        }
    }

    private void CallDialRightOnce()
    {
        if (dialKnobTransform.localEulerAngles.y > 150 && !dialRightCalled)
        {
            dialRightOnce.Invoke();
            dialLeftCalled = false;
            dialUpCalled = false;
            dialRightCalled = true;
        }
    }
}
