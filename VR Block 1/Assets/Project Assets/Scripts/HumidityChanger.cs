using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HumidityChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI humidity;
    [SerializeField] private float humidityCount;
    public Image image;

    [Header("Settings")]
    [SerializeField] private float humidityIncrementRate;




    // Start is called before the first frame update
    void Start()
    {
        humidity = GetComponent<TextMeshProUGUI>();
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

    public void KeepIncreasingHumidity()
    {
        humidityCount += humidityIncrementRate;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    public void KeepDecreasingHumidity()
    {
        humidityCount -= humidityIncrementRate;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    public void EqualizeHumidity()
    {
        humidityCount = Mathf.FloorToInt(humidityCount);
        humidity.text = humidityCount.ToString();
    }

    void ChangeColor()
    {
        if (humidityCount >= 0 && humidityCount < 50)
        {
            image.color = new Color32(255, 0, 0, 230);
        }
        else if (humidityCount >= 50 && humidityCount < 70)
        {
            image.color = new Color32(0, 0, 0, 230);
        }
        else
        {
            image.color = new Color32(255, 0, 0, 230);
        }
    }

    void Update()
    {
        ChangeColor();
    }
}