using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HarvestHeadset : MonoBehaviour
{
    [SerializeField] GameObject transitionScreen, player, farmSpawn, orgPos;
    [SerializeField] int delayTime = 2;
    [SerializeField] XRSocketInteractor snapSocket;

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
        this.transform.position = orgPos.transform.position;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        transitionScreen.SetActive(false);
    }
}
