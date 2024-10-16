using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] HarvestHeadset hh;

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            hh.HeadsetOff();
            Debug.Log("player on");
        }
    }
}
