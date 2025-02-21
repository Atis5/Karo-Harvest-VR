using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public class NarrationScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ShowNarration;

    [Header("Narration")]
    [TextArea] [SerializeField] private string narrationSentance1;
    [TextArea] [SerializeField] private string narrationSentance2;
    [TextArea] [SerializeField] private string narrationSentance3;
    [TextArea] [SerializeField] private string narrationSentance4;
    [TextArea] [SerializeField] private string narrationSentance5;
    [TextArea] [SerializeField] private string narrationSentance6;
    [TextArea] [SerializeField] private string narrationSentance7;
    [TextArea] [SerializeField] private string narrationSentance8;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(narration());
    }

    IEnumerator narration()
    {
        ShowNarration.text = narrationSentance1;

        yield return new WaitForSeconds(3);

        ShowNarration.text = narrationSentance2;

        yield return new WaitForSeconds(8);

        ShowNarration.text = narrationSentance3;

        yield return new WaitForSeconds(5);

        ShowNarration.text = narrationSentance4;

        yield return new WaitForSeconds(4);

        ShowNarration.text = narrationSentance5;

        yield return new WaitForSeconds(6);

        ShowNarration.text = narrationSentance6;

        yield return new WaitForSeconds(5);

        ShowNarration.text = narrationSentance7;

        yield return new WaitForSeconds(4);

        ShowNarration.text = narrationSentance8;

        yield return new WaitForSeconds(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
