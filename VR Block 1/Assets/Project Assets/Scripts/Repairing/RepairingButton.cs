using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairingButton : MonoBehaviour
{
    public UnityEvent onPress;
    private void OnTriggerEnter(Collider other)
    {
        onPress.Invoke();
    }
}
