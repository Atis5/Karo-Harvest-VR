using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityAudio : MonoBehaviour
{

    [Header ("References")]
    private HumidityChanger humidityChanger;
    private AudioSource audioSource;

    [Header ("Variables")]
    private int m_humidityCount;
    private int newHumidityCount;


    // Start is called before the first frame update
    void Start()
    {
        // Grab references
        audioSource = GetComponent<AudioSource>();
        humidityChanger = GetComponent<HumidityChanger>();
        newHumidityCount = (int)humidityChanger.humidityCount;
    }

    /// <summary>
    /// Plays the sound if humidity number changes.
    /// </summary>
    public void PlayHumiditySound()
    {
        m_humidityCount = (int)humidityChanger.humidityCount;
        if (m_humidityCount != newHumidityCount)
        {
            audioSource.Play();
            newHumidityCount = m_humidityCount;
        }
    }
}
