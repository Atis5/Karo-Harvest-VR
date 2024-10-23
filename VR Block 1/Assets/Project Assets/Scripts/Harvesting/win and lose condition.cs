using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class winandlosecondition : MonoBehaviour
{
    public int cropCount = 0; // Tracks the number of crops collected
    [SerializeField]
    private int winCondition = 15; // Number of crops required to win
    [SerializeField]
    private float timeLimit = 10f; // Time limit in seconds
    private bool gameIsOver = false; // To track if the game is over

    float timePassed;

    private void Start()
    {
        // Start the timer countdown when the game begins
        //StartCoroutine(StartTimer());
    }
    /*
    public void StoreHarvest()
    {
        if (gameIsOver) return; // Prevent storing crops after game is over
        // Get the crop object the player is holding
        IXRSelectInteractable crop = storingSocket.GetOldestInteractableSelected();
        Debug.Log(crop);
        // Remove the crop object from the socket so it can be modified
        storingSocket.interactionManager.SelectExit(storingSocket, crop);
        Destroy(crop.transform.gameObject);
        // Increment the crop count
        cropCount++;
        // Check for winning condition
        if (cropCount >= winCondition)
        {
            Debug.Log("You win! You have collected " + cropCount + " crops.");
            WinGame();
        }
    }

    // Timer Coroutine to track the countdown
    private IEnumerator StartTimer()
    {
        float timer = timeLimit;
        while (timer > 0 && !gameIsOver)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timer--;
            Debug.Log("Time left: " + timer + " seconds");
            // If time runs out and the player has not collected enough crops, they lose
            if (timer <= 0 && cropCount < winCondition)
            {
                LoseGame();
            }
        }
    }*/

    private void Update()
    {
        timePassed = Time.time;
        if (cropCount >= winCondition)
        {
            WinGame();
        } else if (timePassed >= timeLimit) 
        {
            LoseGame();
        }
    }

    //referenced in harvest headset storing method
    public void addCrop()
    {
        cropCount++;
        Debug.Log(cropCount.ToString());
    }

    // Method to handle winning
    private void WinGame()
    {
        //gameIsOver = true; // Set game as over to prevent further actions

        Debug.Log("Congratulations! You won the game!");

        //load win scene (reference in build settings)
        SceneManager.LoadScene("WinUI");
    }

    // Method to handle losing
    private void LoseGame()
    {
        //gameIsOver = true; // Set game as over to prevent further actions

        Debug.Log("Time's up! You lost the game.");
        //load lose scene (reference in build settings)
        SceneManager.LoadScene("LoseUI");
    }
}

