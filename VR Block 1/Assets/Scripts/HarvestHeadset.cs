using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HarvestHeadset : MonoBehaviour
{
    [SerializeField] GameObject transitionScreen, player, farmSpawn, stand;
    [SerializeField] int delayTime = 2;
    [SerializeField] XRSocketInteractor snapSocket;
    [SerializeField] XRSocketInteractor storingSocket;
    [SerializeField] XRSocketInteractor standSocket;

    private void Start()
    {
        transitionScreen.SetActive(false);
    }
    public void HeadsetOn()
    {
        //make screen black for a few seconds
        transitionScreen.SetActive(true);
        StartCoroutine(Delay());
        //move player to farm
        player.transform.position = farmSpawn.transform.position;
        //remove headset from socket
        snapSocket.interactionManager.SelectExit(snapSocket, snapSocket.GetOldestInteractableSelected());
        //move headset to original position
        this.transform.position = stand.transform.position;
        //move headset to original rotation
        this.transform.rotation = stand.transform.rotation;
    }

    public void HeadsetOff()
    {
        transitionScreen.SetActive(true);
        StartCoroutine(Delay());
        player.transform.position = Vector3.zero;
        Debug.Log("yur");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        transitionScreen.SetActive(false);
    }

    //in select entered of storing socket
    public void StoreHarvest()
    {
        //get the crop object the player is holding
        IXRSelectInteractable crop = storingSocket.GetOldestInteractableSelected();
        Debug.Log(crop);
        //remove the crop object from the socket so it can be modified
        storingSocket.interactionManager.SelectExit(storingSocket, crop);
        Destroy(crop.transform.gameObject);

        /*
        //get the parent of the crop object
        GameObject plantBase = crop.transform.parent.gameObject;
        //disable the visibility of the crop object
        plantBase.transform.GetChild(2).gameObject.SetActive(false);
        //get script
        PlantGrowth repo = plantBase.GetComponent<PlantGrowth>();
        //run the reposition code
        repo.reposition();
        */

        //Debug.Log("Collected");
    }

    //in select exited for the object
    public void returnToSocket()
    {
        //check if there is nothing in the socket
        if(standSocket.interactablesSelected.Count == 0)
        {
            //move headset to original position
            this.transform.position = stand.transform.position;
            //move headset to original rotation
            this.transform.rotation = stand.transform.rotation;
            //Debug.Log("returning");
        }
    }
}
