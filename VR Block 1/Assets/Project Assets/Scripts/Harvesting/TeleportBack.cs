using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeleportBack : MonoBehaviour
{
    [SerializeField] GameObject player;
    float timeOnPlatform = 0;
    float timeToTeleport = 5;
    bool justTeleported = true;
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] ParticleSystem particles;

    public UnityEvent headsetOff;

    private void OnTriggerStay(Collider other)
    {
        //check if player is colliding
        if (other.gameObject == player && !justTeleported)
        {
            //starts the particle system
            particles.Play();
            //keep track of how much time the player has been on the platform
            timeOnPlatform += Time.deltaTime;
            //checks if it reaches the time it takes to teleport
            if (timeOnPlatform >= timeToTeleport) 
            {
                //run the taking off headset method in harvest headset script
                headsetOff.Invoke();
                //resets the time on platform and starts a delay to reset a variable
                timeOnPlatform = 0;
                StartCoroutine(Delay());
            }
            Debug.Log("teleporting");
        }
    }

    //delay so that onTriggerExit doesn't change the justTeleported variable back to false the moment the player teleports
    IEnumerator Delay() 
    {
        yield return new WaitForSeconds(delayTime);
        justTeleported = true;
        //stops the particle system
        particles.Stop();
        //Debug.Log("delay done");
    }

    //ensures that the player doesn't immediately get teleported back into the office when teleporting into the field
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == player)
        {
            timeOnPlatform = 0;
            particles.Pause();
            particles.Clear();
            if (justTeleported)
            {
                justTeleported = false;
            }
        }
    }

    private void Update()
    {
        //Debug.Log(timeOnPlatform);
        //Debug.Log(justTeleported);
        //Debug.Log(particles.isPlaying);
    }
}
