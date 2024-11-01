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

    [SerializeField] private bool isPlantDead = false;

    float[] growthPeriods;
    int plantStagesNum = 3; // minus 1, as it start off with stage 1 and changes to stage 2, whilst stage 3 happens once the time is passed
    float timePassed;
    float timeToDiePassed = 0;
    int currStage = -1;

    [SerializeField] TMP_Text txt;

    GameObject deadCropClone;
    GameObject cropClone;
    GameObject currPlant;

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
        DeathTimer();
        KillPlant();

        //update countdown text over the plant (shows how much time left in growth)
        if(timePassed < _growthTime && !isPlantDead)
        {
            txt.text = (_growthTime - timePassed).ToString("F0"); //F0 = 0 decimal points
        } else if (isPlantDead)
        {
            txt.text = "DEAD :(";
        } else
        {
            txt.text = "READY :)";
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
        if (!isPlantDead)
        {
            //Debug.Log("growing");
            for (int i = 0; i < plantStagesNum; i++)
            {
                    //compares the time and the growth period. currStage makes sure the code in the if statement is ran only once 
                    if (timePassed >= growthPeriods[i] && currStage == i - 1)
                    {
                        //Debug.Log("stage " + (i + 1));
                        //activates the visibility of the next plant stage
                        currPlant = this.transform.GetChild(i).gameObject;
                        currPlant.SetActive(true);
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
                            cropClone = Instantiate(this.transform.GetChild(plantStagesNum - 1).gameObject, this.transform);
                            //make the original grown crop invisible so that the player picks up the clone - ensures the object wont be named X(Clone)(Clone)(Clone) etc.
                            this.transform.GetChild(plantStagesNum - 1).gameObject.SetActive(false);

                            //make it invisible
                            //cropClone.SetActive(false);
                            //make it the 3rd child so that the growth process continues
                            //cropClone.transform.SetSiblingIndex(plantStagesNum-1);
                        }

                    }
            }
        }
    }

    void KillPlant()
    {
        // \/ \/ \/ CHRIS' PART \/ \/ \/

        // Replace the current plant object with dead plant object
        if (timeToDiePassed >= _timeToDie && !isPlantDead)
        {
            isPlantDead = true;

            currPlant.SetActive(false);
            Debug.Log(currPlant + " is dead");

            // Otherwise disable the copy of the plant.
            if (cropClone != null)
            {
                Debug.Log("KILLING CHILD'S COPY");
                Destroy(cropClone);
            }

            // Show the dead plant
            deadCropClone = Instantiate(this.transform.GetChild(plantStagesNum).gameObject, this.transform);
            deadCropClone.SetActive(true);
        }

        // /\ /\ /\ CHRIS' PART /\ /\/ \
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
    /// Calculates how much time has passed in incorrect humidity.
    /// </summary>
    private void DeathTimer()
    {
        // Grab reference from another script
        _isHumidityCorrect = humidityChanger.isHumidityCorrect;

        // Calculate how much time has passed in the wrong humidity.
        if (_isHumidityCorrect == false)
        {
            timeToDiePassed += Time.deltaTime;
        }

        // Reset the timer to 0 if humidity is correct.
        else if (_isHumidityCorrect == true && timeToDiePassed != 0)
        {
            timeToDiePassed = 0;
        }
    }


    public void DestroyPlant()
    {
        Destroy(deadCropClone);
        timeToDiePassed = 0;
        isPlantDead = false;
        Harvest();
    }

    /*public void reposition()
    {
        GameObject crop = this.transform.GetChild(plantStagesNum - 1).gameObject;
        crop.transform.position = this.transform.position;
        crop.transform.rotation = orgRotation;
    } */
}
