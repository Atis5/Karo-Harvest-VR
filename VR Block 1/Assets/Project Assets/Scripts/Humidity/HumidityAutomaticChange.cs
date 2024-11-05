using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HumidityAutomaticChange : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Humidity;
    [SerializeField] private TextMeshProUGUI humidity;
                     private HumidityChanger HumidityChanger;

    [Header("Settings")]
    [SerializeField] private bool startBrokenDecreasing = true;

    

    // Start is called before the first frame update
    void Start()
    {
        HumidityChanger = Humidity.GetComponent<HumidityChanger>();
        humidity.text = HumidityChanger.humidityCount.ToString();

        if (startBrokenDecreasing)
        {
            StartbrokenDecreasing();
        }
    }

    public void StartbrokenDecreasing()
    {
        StartCoroutine(addHumidityCount());
    } 

    IEnumerator addHumidityCount()
    {
        while (true)
        { 
            if (HumidityChanger.humidityCount > 0)
            { // if humidity < 100

                // increase humuidityCount and wait the specified time
                HumidityChanger.humidityCount--;

                // Update text but show only full numbers
                humidity.text = Mathf.FloorToInt(HumidityChanger.humidityCount).ToString(); 

                yield return new WaitForSeconds(1);
            }
            else
            { 
                yield return null;
            }
        }
    }
}
