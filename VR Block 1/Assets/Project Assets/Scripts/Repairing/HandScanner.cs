using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandScanner : MonoBehaviour
{
    //[SerializeField] GameObject leftHand, rightHand; //ITS CURRENTLY USING A TESTER BLOCK - REMEMBER TO CHANGE THIS
    [SerializeField] float timeOnScanner = 0;
    [SerializeField] float timeToScan = 3;
    [SerializeField] TextMeshPro passText;
    [SerializeField] TMP_FontAsset font, orgFont;
    string orgText = "> scan hand to reveal <";
    [SerializeField] float orgFontSize = 192.8f;
    bool scanned = false;

    string password;
    [SerializeField] Repairing repairScript;

    //Needed for audio
    private AudioSource scannerSound;

    // Start is called before the first frame update
    void Start()
    {
        passText.fontSize = orgFontSize;
        //to get the password from repair script
        repairScript?.sendPass.AddListener(getPass);
        //to check if the person got logged off
        repairScript?.logOff.AddListener(loggedOff);

        // Grab reference to audio source.
        scannerSound = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter()
    {
        scannerSound.Play();
    }
    
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject == leftHand || other.gameObject == rightHand) 
        //{
            Debug.Log("scanning");
            timeOnScanner += Time.deltaTime;
            //after waiting for a few seconds, the password shows up
            if (timeOnScanner >= timeToScan && !scanned) 
            {
                Debug.Log("Scan Done");
                passText.text = password; 
                passText.font = font;
                passText.fontSize = 687;
                scanned = true;
            }

        //}
    }

    private void OnTriggerExit(Collider other)
    {
        timeOnScanner = 0;
        scannerSound.Stop();
    }

    //gets the password from repairing script
    void getPass(string pass)
    {
        Debug.Log("got pass: " + pass);
        password = pass;
    }

    void loggedOff()
    {
        Debug.Log("logged off");
        passText.text = orgText;
        passText.font = orgFont;
        passText.fontSize = orgFontSize;
        scanned = false;
    }
}
