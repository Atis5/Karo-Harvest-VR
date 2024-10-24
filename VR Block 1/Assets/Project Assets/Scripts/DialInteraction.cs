using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialKnob;

    [Header("Events")]
    public UnityEvent dialLeft;
    public UnityEvent dialUp;
    public UnityEvent dialRight;

    [Header("Other")]
    public Transform dialKnobTransform;

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
        CallDialUp();   
        CallDialRight();
    }

    private void CallDialLeft()
    {
        if (dialKnobTransform.localEulerAngles.y < 60)
        {
            //Debug.Log("Dial rotated left!");
            dialLeft.Invoke();
        }
    }

    private void CallDialUp()
    {
        if (dialKnobTransform.localEulerAngles.y >= 60 && dialKnobTransform.localEulerAngles.y <= 120)
        {
            //Debug.Log("Dial rotated up!");
            dialUp.Invoke();
        }
    }

    private void CallDialRight()
    {
        if (dialKnobTransform.localEulerAngles.y > 120)
        {
            //Debug.Log("Dial rotated right!");
            dialRight.Invoke();
        }
    }
}
