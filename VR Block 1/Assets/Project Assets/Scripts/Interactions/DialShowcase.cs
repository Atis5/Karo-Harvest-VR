using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialShowcase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialController;
                     private DialInteraction dialInteraction;
    [SerializeField] private TextMeshProUGUI valueNumber;
    [SerializeField] private TextMeshProUGUI rotationText;

    private float knobRotationValue;



    // Start is called before the first frame update
    void Start()
    {
        dialInteraction = dialController.GetComponent<DialInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        knobRotationValue = Mathf.Round(dialInteraction.dialKnobTransform.localEulerAngles.y);
        valueNumber.text = knobRotationValue.ToString();
    }

    public void DisplayLeft()
    {
        rotationText.text = "LEFT";
    }

    public void DisplayUp()
    {
        rotationText.text = "UP";
    }

    public void DisplayRight()
    {
        rotationText.text = "RIGHT";
    }
}
