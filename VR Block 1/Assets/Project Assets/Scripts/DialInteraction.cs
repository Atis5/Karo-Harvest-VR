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
    private float knobRotation;

    // Start is called before the first frame update
    void Start()
    {
        knobRotation = dialKnob.GetComponent<Transform>().rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        CallDialLeft();
        CallDialUp();   
        CallDialRight();
    }

    private void CallDialLeft()
    {
        if (knobRotation <= -90)
        {
            Debug.Log("Dial rotated left!");
            dialLeft.Invoke();
        }
    }

    private void CallDialUp()
    {
        if (knobRotation == 0)
        {
            Debug.Log("Dial rotated up!");
            dialUp.Invoke();
        }
    }

    private void CallDialRight()
    {
        if (knobRotation >= 90)
        {
            Debug.Log("Dial rotated right!");
            dialRight.Invoke();
        }
    }
}
