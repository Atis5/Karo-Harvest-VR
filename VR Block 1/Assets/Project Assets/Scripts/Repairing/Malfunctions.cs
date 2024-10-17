using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunctions : MonoBehaviour
{
    [SerializeField] List<float> timePhases, malfunctTimes; //in seconds
    int[] frequencies;
    float timePassed;
    int currPhase = -1;

    // Start is called before the first frame update
    void Start()
    {
        timePhases.Add(3f);
        timePhases.Add(5f);
        timePhases.Add(8f);
        frequencies = new int[timePhases.Count];
        calcFreq();
    }

    // Update is called once per frame
    void Update()
    {
        //keeps track of time
        timePassed = Time.time;

        for (int i = 0; i < timePhases.Count; i++)
        {
            if (timePassed >= timePhases[i] && currPhase == i-1)
            {
                currPhase = i;
                calcTimes(frequencies[i]); // move to before if
                //Debug.Log("equal");
            }
        }
    }

    //get frequencies for each phase which has an increasingly higher range - randomised for replayability
    void calcFreq()
    {
        for (int i = 0; i < frequencies.Length; i++)
        {
            frequencies[i] = Random.Range(1 + i, 3 + i); // 1,3 2,4, 3,5
            //Debug.Log(i + ": " + frequencies[i]);
        }
    }

    void calcTimes(int freq)
    {
        Debug.Log(currPhase + ": " + freq);
        for (int i = 0; i < freq; i++) 
        {
            /*
            float num = Random.Range(timePhases[i], timePhases[i+1]);
            malfunctTimes.Add(num);
            Debug.Log("Range: " + timePhases[i] +", "+ timePhases[i+1] + "= " + i + ": " + num); */
        }
    }
}
