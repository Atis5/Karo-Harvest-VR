using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehaviourScript : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo; //that's what we're scaleing

    Vector3 desiredScale = Vector3.zero; //default is Info being off

    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale,desiredScale,Time.deltaTime * SPEED);
    }

    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}
