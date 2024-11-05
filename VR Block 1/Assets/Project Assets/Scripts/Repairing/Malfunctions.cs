using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Malfunctions : MonoBehaviour
{
    [SerializeField] List<float> timePhases, malfunctTimes; //in seconds
    int[] frequencies;
    float timePassed;
    int currMalfunct = 0;
    float timeInBetweenMalfunct = 0.1f;
    [SerializeField] int numPhases = 3;
    [SerializeField] float maxTime;

    public bool humidifierMalfunt, harvesterMalfunt = false;
    [SerializeField] List<string> allMachines;
    [SerializeField] TextMeshPro humidifierTXT, harvesterTXT;
    [SerializeField] GameObject harvesterMalfunctUI, humidifierMalfunctUI;
    [SerializeField] GameObject headset;

    //[SerializeField] bool repairHumid, repairHarv;

    [SerializeField] winandlosecondition wlScript;

    //Audio references
    [Tooltip ("Reference from 'HumidMalfunct' object")]
    [SerializeField] private AudioSource humidifierAudio;
    [Tooltip ("Reference from 'HarvMalfunct' object")]
    [SerializeField] private AudioSource harvesterAudio;

    // Start is called before the first frame update
    void Start()
    {
        wlScript?.sendTime.AddListener(getMaxTime);
        wlScript?.startCalc.AddListener(startCalculations);
        //add all machines to a list
        allMachines.Add("Harvester");
        allMachines.Add("Humidifier");
        /*
        //calculations
        calcTimePhases();
        frequencies = new int[timePhases.Count - 1];
        calcFreq();
        calcTimes(frequencies);*/
    }

    //called by the winning and losing conditions script so it happens after the script gets the maxTime variable
    void startCalculations()
    {
        calcTimePhases();
        frequencies = new int[timePhases.Count - 1];
        calcFreq();
        calcTimes(frequencies);
    }
    // Update is called once per frame
    void Update()
    {
        //keeps track of time
        timePassed = Time.timeSinceLevelLoad;
        //index starts from 1, as index 0 = 0
        for (int i = 1; i < malfunctTimes.Count; i++)
        {
            if (timePassed >= malfunctTimes[i] && currMalfunct == i-1)
            {
                Debug.Log("Break!");
                if (!harvesterMalfunt || !humidifierMalfunt)
                {
                    breakMachine();
                }
                currMalfunct = i;
            }
        }
        /*//TEST
        if (repairHarv)
        {
            repairMachine("Harvester");
            harvesterMalfunt = false;
            harvesterTXT.text = "HARVESTER: FUNCTIONING PROPERLY";
            repairHarv = false;
        } else if (repairHumid)
        {
            repairMachine("Humidifier");
            humidifierMalfunt = false;
            humidifierTXT.text = "HUMIDIFIER: FUNCTIONING PROPERLY";
            repairHumid = false;
        }*/
    }

    //calculate the duration of the phases
    void calcTimePhases()
    {
        timePhases.Add(0f);
        float phaseTime = maxTime / numPhases;
        for (int i = 1;i <= numPhases;i++)
        {
            timePhases.Add(timePhases[i - 1] + phaseTime);
            //Debug.Log(timePhases[i]);
        }
    }

    //get frequencies for each phase which has an increasingly higher range - randomised for replayability
    void calcFreq()
    {
        for (int i = 0; i < frequencies.Length; i++)
        {
            frequencies[i] = Random.Range(2 + i, 4 + i); // 1,3 2,4, 3,5
            //Debug.Log(i + ": " + frequencies[i]);
        }
    }

    //get the times based on the frequencies and phases
    void calcTimes(int[] freqArray)
    {
        float num;
        malfunctTimes.Add(0);
        //loops through the frequency array
        for (int i = 0; i < freqArray.Length; i++) 
        {
            //takes the frequency from the array and gets that amount of random times
            for (int freq = 0; freq < freqArray[i]; freq++)
            {
                if(freq == 0) 
                {
                    //gets a number in the range of the beginning of the phase to the last
                    num = Random.Range(timePhases[i], timePhases[i + 1]);
                } else
                {
                    //gets a number in the range of the previous time to the end of the phase with an delay in between so that multiple malfunctions dont happen at the same time
                    num = Random.Range(malfunctTimes.Last() + timeInBetweenMalfunct, timePhases[i+1]);
                }
                //keep track of the times
                malfunctTimes.Add(num);
                Debug.Log("Range: " + timePhases[i] + " to " + timePhases[i + 1] + " = " + i + ": " + num);
            }
        }
    }

    //chooses a random machine to break
    void breakMachine()
    {
        int rand = Random.Range(0, allMachines.Count);
        if (allMachines[rand] == "Humidifier")
        {
            //Debug.Log("Humidifier broken");
            breakHumidifier();
            humidifierAudio.Play();
        } else if (allMachines[rand] == "Harvester")
        {
            //Debug.Log("Harvester broken");
            breakHarvester();
            harvesterAudio.Play();
        } else
        {
            Debug.Log("Machine doesn't exist");
        }
        allMachines.RemoveAt(rand);
    }

    //attached to buttons with a corresponding string
    public void repairMachine(string machine)
    {
        allMachines.Add(machine);
        if (machine == "Harvester")
        {
            repairHarvester();
            Debug.Log("fixed harvester");
        } else if (machine == "Humidifier")
        {
            repairHumidifier();
            Debug.Log("fixed humidifier");
        }
    }

    void breakHarvester()
    {
        //gets the script that makes the object grabbable and disables it
        headset.GetComponent<XRGrabInteractable>().enabled = false;
        //activates the malfunction UI
        harvesterMalfunctUI.SetActive(true);
        harvesterMalfunt = true;
        harvesterTXT.text = "HARVESTER: MALFUNCTIONING";
    }

    void breakHumidifier()
    {
        //activates the malfunction UI
        humidifierMalfunctUI.SetActive(true);
        humidifierMalfunt = true;
        humidifierTXT.text = "HUMIDIFIER: MALFUNCTIONING";
    }

    public void repairHarvester()
    {
        //enables the script that makes the object grabbable
        headset.GetComponent<XRGrabInteractable>().enabled = true;
        //deactivates the malfunction UI
        harvesterMalfunctUI.SetActive(false);
        harvesterMalfunt = false;
        harvesterTXT.text = "HARVESTER: FUNCTIONING PROPERLY";
    }

    public void repairHumidifier()
    {
        //deactivates the malfunction UI
        humidifierMalfunctUI.SetActive(false);
        humidifierMalfunt = false;
        humidifierTXT.text = "HUMIDIFIER: FUNCTIONING PROPERLY";

        //continue code...
    }

    void getMaxTime(float time)
    {
        Debug.Log("got pass: " + time);
        maxTime = time;
    }
}
