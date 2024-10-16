using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] HarvestHeadset hh;
    float timeOnPlatform = 0;
    float timeToTeleport = 5;
    bool justTeleported = true;

    private void OnTriggerStay(Collider other)
    {
        //check if player is colliding
        if (other.gameObject == player && !justTeleported)
        {
            timeOnPlatform += Time.deltaTime;
            if (timeOnPlatform >= timeToTeleport) 
            {
                hh.HeadsetOff();
                timeOnPlatform = 0;
            }
            //hh.HeadsetOff();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            timeOnPlatform = 0;
            if(justTeleported)
            {
                justTeleported = false;
            }
        }
    }

    private void Update()
    {
        Debug.Log(timeOnPlatform);
    }
}
