using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPressed : MonoBehaviour
{
    public GameObject knob;
    public UnityEvent onPress;
    public UnityEvent onRelease;

    [SerializeField] private float buttonDepth;

    
    private GameObject presser;
    private bool isPressed = false;
    private Vector3 buttonPosition;

    private void Start()
    {
        buttonPosition = knob.GetComponent<Transform>().localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Debug.Log("Button pressed!");
            knob.transform.localPosition = new Vector3(0, 0.002f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            Debug.Log("Button released!");
            knob.transform.localPosition = buttonPosition;
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
