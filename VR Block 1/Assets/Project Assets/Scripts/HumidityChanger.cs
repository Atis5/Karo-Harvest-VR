using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI humidity;
    private float humidityCount;


    // Start is called before the first frame update
    void Start()
    {
        humidity = GetComponent<TextMeshProUGUI>();
        humidityCount = 0;
        humidity.text = humidityCount.ToString();
    }

    public void IncreaseHumidity()
    {
        humidityCount++;
        humidity.text = humidityCount.ToString();
    }

    public void DecreaseHumidity()
    {
        if (humidityCount > 0)
        {
            humidityCount--;
            humidity.text = humidityCount.ToString();
        }
    }


}
