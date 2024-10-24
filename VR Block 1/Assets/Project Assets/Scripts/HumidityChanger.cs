using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HumidityChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private Image humidityBarSprite;
    [SerializeField] private TextMeshProUGUI humidity;

    [Header("Settings")]
    [SerializeField] private float humidityIncrementRateButton;
    [SerializeField] private float maxHumidityCount;

    public float humidityCount;

    // Start is called before the first frame update
    void Start()
    {
        humidity = GetComponent<TextMeshProUGUI>();
        humidity.text = humidityCount.ToString();
    }


    // Used by UnityEvents.
    public void IncreaseHumidity()
    {      
        humidityCount++;
        humidity.text = humidityCount.ToString();
    }

    // Used by UnityEvents.
    public void DecreaseHumidity()
    {
        if (humidityCount > 0)
        {
            humidityCount--;
            humidity.text = humidityCount.ToString();
        }
    }

    // Used by UnityEvents.
    public void ContinueIncreasingHumidity()
    {
        humidityCount += humidityIncrementRateButton;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    // Used by UnityEvents.
    public void ContinueDecreasingHumidity()
    {
        humidityCount -= humidityIncrementRateButton;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    // Used by UnityEvents.
    public void EqualizeHumidity()
    {
        humidityCount = Mathf.FloorToInt(humidityCount);
        humidity.text = humidityCount.ToString();
    }

    public void ChangeColor()
    {
        if (humidityCount >= 0 && humidityCount < 50)
        {
            image.color = new Color32(255, 0, 0, 230);
            humidityBarSprite.color = new Color32(255, 0, 0, 255);
        }
        else if (humidityCount >= 50 && humidityCount < 70)
        {
            image.color = new Color32(0, 0, 0, 230);
            humidityBarSprite.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            image.color = new Color32(0, 0, 0, 230);
            humidityBarSprite.color = new Color32(255, 0, 0, 255);
        }
    }

    public void UpdateHumidityBar()
    {
        humidityBarSprite.fillAmount = humidityCount / maxHumidityCount;
    }

    void Update()
    {
        ChangeColor();
        UpdateHumidityBar();
    }
}