using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HumidityChanger : MonoBehaviour
{


    [Header("Settings")]
    [Tooltip("How fast the player can increase / decrease humidity.")]
    [SerializeField] private float humidityIncrementRateButton;
    [Tooltip("Humidity count cannot go over this value.")]
    [SerializeField] private float maxHumidityCount;
    [Tooltip("Set the minimum and the maximum value for correct humidity.")]
    [SerializeField] private float[] correctHumidity;

    [Header("Information")]
    public float humidityCount;
    public bool isHumidityCorrect = true;

    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private Image humidityBarSprite;
    [SerializeField] private TextMeshProUGUI humidity;


    // Start is called before the first frame update
    void Start()
    {
        humidity = GetComponent<TextMeshProUGUI>();
        humidity.text = humidityCount.ToString();
    }

    void SetCorrectHumidity()
    {
        if (humidityCount <= correctHumidity[0] || humidityCount >= correctHumidity[1])
        {
            isHumidityCorrect = false;
        }
        else
        {
            isHumidityCorrect = true;
        }
    }

    /// <summary>
    /// Changes the color of the humidity bar and the screen's background.
    /// </summary>
    void ChangeColor()
    {
        if (isHumidityCorrect)
        {
            image.color = new Color32(0, 0, 0, 230);
            humidityBarSprite.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            image.color = new Color32(255, 0, 0, 230);
            humidityBarSprite.color = new Color32(255, 0, 0, 255);
        }
    }

    void UpdateHumidityBar()
    {
        humidityBarSprite.fillAmount = humidityCount / maxHumidityCount;
    }

    void Update()
    {
        SetCorrectHumidity();
        ChangeColor();
        UpdateHumidityBar();

        // Keep humidityCount between 1 and 100
        humidityCount = Mathf.Clamp(humidityCount, 1, 100);
    }


    //  \/\/\/ METHODS FOR UNITY EVENTS \/\/\/ 

    /// <summary>
    /// Increases Humidity by 1. Use it for one-time presses.
    /// </summary>
    public void IncreaseHumidity()
    {      
        humidityCount++;
        humidity.text = humidityCount.ToString();
    }

    /// <summary>
    /// Decreases Humidity by 1. Use it for one-time presses.
    /// </summary>
    public void DecreaseHumidity()
    {
        if (humidityCount > 0)
        {
            humidityCount--;
            humidity.text = humidityCount.ToString();
        }
    }

    /// <summary>
    /// Increases the humidity by a certain amount every frame. Use it for long-lasting actions.
    /// </summary>
    public void ContinueIncreasingHumidity()
    {
        humidityCount += humidityIncrementRateButton;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    /// <summary>
    /// Decreases the humidity by a certain amount every frame. Use it for long-lasting actions.
    /// </summary>
    public void ContinueDecreasingHumidity()
    {
        humidityCount -= humidityIncrementRateButton;
        humidity.text = Mathf.FloorToInt(humidityCount).ToString();
    }

    /// <summary>
    /// Equalizes humidity to the round number. Use it when switching off the continuous function.
    /// </summary>
    public void EqualizeHumidity()
    {
        humidityCount = Mathf.FloorToInt(humidityCount);
        humidity.text = humidityCount.ToString();
    }
}