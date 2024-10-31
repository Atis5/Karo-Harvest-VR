using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlantGrowth : MonoBehaviour
{
    // References
    private PlantSettings plantSettings;
    private HumidityChanger humidityChanger;
    private GameObject humidityController;

    // Referenced variables. Do not change them here.
    private float _growthTime;
    private float _timeToDie;
    private bool _isHumidityCorrect;

    float[] growthPeriods;
    int plantStagesNum = 3; // minus 1, as it start off with stage 1 and changes to stage 2, whilst stage 3 happens once the time is passed
    float timePassed;
    float timeToDiePassed = 0;
    int currStage = -1;

    [SerializeField] TMP_Text txt;

    //Quaternion orgRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Grab references.
        plantSettings = GetComponentInParent<PlantSettings>();
        humidityController = GameObject.Find("Humidity Controller");
        humidityChanger = humidityController.GetComponent<HumidityChanger>();

        // Grab variables from references.
        _growthTime = plantSettings.growthTime;
        _timeToDie = plantSettings.timeToDie;
        

        growthPeriods = calculateGrowthPeriods();

        //orgRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //keep track of time in seconds
        timePassed += Time.deltaTime;

        growPlant();
        KillPlant();

        //update countdown text over the plant (shows how much time left in growth)
        if(timePassed < _growthTime)
        {
            txt.text = (_growthTime - timePassed).ToString("F0"); //F0 = 0 decimal points
        } else
        {
            txt.text = "READY";
        }
    }

    //makes an array of the time for the plant to change stages (divided equally)
    float[] calculateGrowthPeriods()
    {
        //divide the duration of growth and number of plant stages
        float periodTime = _growthTime / plantStagesNum;
        //create an array to keep the times
        float[] periodArray = new float[plantStagesNum];
        
        //calculate and add into an array of times by continously adding the time period
        periodArray[0] = periodTime;
        //Debug.Log("1: " + periodArray[0]);
        for (int i = 1; i < plantStagesNum; i++)
        {
            periodArray[i] = periodArray[i-1] + periodTime;
            //Debug.Log( (i+1) + " " + periodArray[i]);
        }
        return periodArray;
    }

    void growPlant()
    {
        //Debug.Log("growing");
        for (int i = 0; i < plantStagesNum; i++)
        {
            //compares the time and the growth period. currStage makes sure the code in the if statement is ran only once 
            if (timePassed >= growthPeriods[i] && currStage == i - 1)
            {
                //Debug.Log("stage " + (i + 1));
                //activates the visibility of the next plant stage
                this.transform.GetChild(i).gameObject.SetActive(true);
                currStage = i;
                if (i != 0)
                {
                    //disables the visibility of the previous plant stage
                    this.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
                if (i == plantStagesNum - 1)
                {
                    //Debug.Log("growth done");
                    //create a clone of the grown crop
                    GameObject cropClone = Instantiate(this.transform.GetChild(plantStagesNum - 1).gameObject, this.transform);
                    //make the original grown crop invisible so that the player picks up the clone - ensures the object wont be named X(Clone)(Clone)(Clone) etc.
                    this.transform.GetChild(plantStagesNum-1).gameObject.SetActive(false);

                    //make it invisible
                    //cropClone.SetActive(false);
                    //make it the 3rd child so that the growth process continues
                    //cropClone.transform.SetSiblingIndex(plantStagesNum-1);
                }
            }
        }
    }

    //in first select entered of grown stage of the plant
    public void Harvest()
    {
        //reset variables
        timePassed = 0;
        currStage = -1;
        //Debug.Log("Picked up");
        //Debug.Log("This object: " + this.gameObject.name);
    }


    /// <summary>
    /// Calculates how much time has passed in incorrect humidity and kills the plant if it's longer than "Time To Die".
    /// </summary>
    private void KillPlant()
    {
        // Grab reference from another script
        _isHumidityCorrect = humidityChanger.isHumidityCorrect;

        // Calculate how much time has passed in the wrong humidity.
        if (_isHumidityCorrect == false)
        {
            timeToDiePassed += Time.deltaTime;
            // Debug.Log("Wrong humidity for " + timeToDiePassed + " seconds!");
        }

        // Reset the timer to 0 if humidity is correct.
        else if (_isHumidityCorrect == true && timeToDiePassed != 0)
        {
            timeToDiePassed = 0;
        }

        // Replace the current plant object with dead plant object
        if (timeToDiePassed >= _timeToDie)
        {
            Debug.Log("PLANT IS KILL! ;-;");
            this.transform.GetChild(5).gameObject.SetActive(false);
            this.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    /*public void reposition()
    {
        GameObject crop = this.transform.GetChild(plantStagesNum - 1).gameObject;
        crop.transform.position = this.transform.position;
        crop.transform.rotation = orgRotation;
    } */
}
