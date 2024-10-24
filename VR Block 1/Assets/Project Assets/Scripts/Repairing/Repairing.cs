using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Repairing : MonoBehaviour
{
    [SerializeField] TextMeshPro newPass, enterPass;
    [SerializeField] TMP_FontAsset font, orgFont;
    [SerializeField] int passLength = 5;
    string[] symbols = { "b ", "s ", "r ", "u " }; //https://www.fontspace.com/badabum-font-f7151#action=charmap&id=lzxZ
    string password;
    bool generatedPass = false;
    bool loggedIn = false;
    float timeLogged;
    float maxTimeLogged = 3;
    string orgTxt = "Enter Access Code";
    string failTxt = "Wrong Access Code";
    int passCount;
    [SerializeField] float orgFontSize = 192.8f;
    [SerializeField] GameObject loginPage, choosingPage;

    public UnityEvent<string> sendPass;
    public UnityEvent logOff;

    //for testing
    [SerializeField] bool skipPass = false;

    // Start is called before the first frame update
    void Start()
    {
        enterPass.fontSize = orgFontSize;
        choosingPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!generatedPass)
        {
            password = generatePassword().text;
            Debug.Log(password);
            //send the code to hand scanner so it shows up there.
            sendPass.Invoke(password);
            generatedPass = true;
        }
        if (loggedIn)
        {
            Debug.Log("Logged in!");
            timeLogged += Time.deltaTime;
            if (timeLogged >= maxTimeLogged)
            {
                loginPage.SetActive(true);
                choosingPage.SetActive(false);
                loggedIn = false;
                generatedPass = false;
                enterPass.text = orgTxt;
                //log off in hand scanner
                logOff.Invoke();
                timeLogged = 0;
            }
        }
        //FOR TESTING PURPOSES
        if (skipPass)
        {
            loginPage.SetActive(false);
            choosingPage.SetActive(true);
            loggedIn = true;
            skipPass = false;
        }
    }

    //create a random combination of the symbols
    TextMeshPro generatePassword()
    {
        for (int i = 0; i < passLength; i++)
        {
            int rand = Random.Range(0, symbols.Length);
            //Debug.Log(rand + " " + symbols[rand]);
            if (i == 0)
            {
                newPass.text = symbols[rand];
            }
            else if (i != 0)
            {
                newPass.text = newPass.text + symbols[rand];
            }
        }
        return newPass;
    }

    //button press method
    void addText(string txt)
    {
        //add symbol to screen
        if (enterPass.text == orgTxt || enterPass.text == failTxt)
        {
            enterPass.text = txt;
        }
        else if (passCount <= passLength)
        {
            enterPass.text = enterPass.text + txt;
        }
        //change the font (since the symbols are a font) and other formatting
        enterPass.font = font;
        enterPass.fontSize = 600;
        passCount++;
        //check if the code is correct
        if (passCount == passLength)
        {
            if (enterPass.text == password)
            {
                loginPage.SetActive(false);
                choosingPage.SetActive(true);
                loggedIn = true;
            } else
            {
                enterPass.text = failTxt;
                enterPass.font = orgFont;
                enterPass.fontSize = orgFontSize;
            }
            passCount = 0;
        }
    }

    public void addB()
    {
        addText("b ");
    }

    public void addS()
    {
        addText("s ");
    }

    public void addR()
    {
        addText("r ");
    }

    public void addU()
    {
        addText("u ");
    }
}
