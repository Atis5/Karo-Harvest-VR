using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandScanner : MonoBehaviour
{
    [SerializeField] GameObject leftHand, rightHand; //ITS CURRENTLY USING A TESTER BLOCK - REMEMBER TO CHANGE THIS
    [SerializeField] float timeOnScanner = 0;
    [SerializeField] float timeToScan = 3;
    [SerializeField] TextMeshPro passText;
    [SerializeField] TMP_FontAsset font;
    bool scanned = false;

    string password;
    [SerializeField] Repairing repairScript;

    // Start is called before the first frame update
    void Start()
    {
        //to get the password from repair script
        repairScript?.sendPass.AddListener(getPass);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand) 
        {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeOnScanner = 0;
    }

    //gets the password from repairing script
    void getPass(string pass)
    {
        password = pass;
    }
}
