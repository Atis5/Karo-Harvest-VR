using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HumidityAutomaticChange : MonoBehaviour
{
    [SerializeField] private GameObject Humidity;
    [SerializeField] private TextMeshProUGUI humidity;
    private float humidityCount;
    private HumidityChanger HumidityChanger;
    private bool Continue = false;

    // Start is called before the first frame update
    void Start()
    {
        HumidityChanger = this.Humidity.GetComponent<HumidityChanger>();
        humidity.text = humidityCount.ToString();
    }

    public void StartbrokenIncresing()
    {
        StartCoroutine(addHumidityCount());
    } 

    IEnumerator addHumidityCount()
    {
        while (true)
        { 
            if (humidityCount < 100)
            { // if humidity < 100
                 
                humidityCount += 1; // increase humuidityCount and wait the specified time
                humidity.text = humidityCount.ToString();
                yield return new WaitForSeconds(1);
            }
            else
            { 
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
