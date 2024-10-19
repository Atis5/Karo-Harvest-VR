using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialShowcase : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject dialKnob;


    // Start is called before the first frame update
    void Start()
    {
        dialKnob.GetComponent<DialInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dialKnob.GetComponent<DialInteraction>().dialKnobTransform);
    }
}
