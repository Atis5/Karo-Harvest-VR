using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeQuota : MonoBehaviour
{
    TMP_Text txt;
    [SerializeField] winandlosecondition wlScript;

    // Start is called before the first frame update
    void Start()
    {
        txt = this.GetComponent<TMP_Text>();
        wlScript?.quotaChange.AddListener(ChangeText);
    }

    void ChangeText(int count, int maxCount)
    {
        txt.text = "Quota: " + count + "/" + maxCount;
    }
}
