using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlantGrowth : MonoBehaviour
{
    float growthTime = 10;
    float[] growthPeriods;
    int plantStagesNum = 3; // minus 1, as it start off with stage 1 and changes to stage 2, whilst stage 3 happens once the time is passed
    float timePassed;
    int currStage = -1;

    Quaternion orgRotation;

    // Start is called before the first frame update
    void Start()
    {
        growthPeriods = calculateGrowthPeriods();

        orgRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //keep track of time in seconds
        timePassed += Time.deltaTime;
        growMethod();
    }

    //makes an array of the time for the plant to change stages (divided equally)
    float[] calculateGrowthPeriods()
    {
        //divide the duration of growth and number of plant stages
        float periodTime = growthTime / plantStagesNum;
        //create an array to keep the times
        float[] periodArray = new float[plantStagesNum];
        
        //calculate and add into an array of times by continously adding the time period
        periodArray[0] = periodTime;
        Debug.Log("1: " + periodArray[0]);
        for (int i = 1; i < plantStagesNum; i++)
        {
            periodArray[i] = periodArray[i-1] + periodTime;
            Debug.Log( (i+1) + " " + periodArray[i]);
        }
        return periodArray;
    }

    void growMethod()
    {
        //Debug.Log("growing");
        for (int i = 0; i < plantStagesNum; i++)
        {
            //compares the time and the growth period. currStage makes sure the code in the if statement is ran only once 
            if (timePassed >= growthPeriods[i] && currStage == i - 1)
            {
                Debug.Log("stage " + (i + 1));
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
                    Debug.Log("fin");
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

    public void Harvest()
    {
        timePassed = 0;
        currStage = -1;
        Debug.Log("Picked up");
        //Debug.Log("This object: " + this.gameObject.name);
    }

    /*public void reposition()
    {
        GameObject crop = this.transform.GetChild(plantStagesNum - 1).gameObject;
        crop.transform.position = this.transform.position;
        crop.transform.rotation = orgRotation;
    } */
}
