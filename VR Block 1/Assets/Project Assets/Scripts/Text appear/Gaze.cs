using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gaze : MonoBehaviour
{
  List<InfoBehaviourScript> infos = new List<InfoBehaviourScript>();

    void Start()
    {
        infos = FindObjectsOfType<InfoBehaviourScript>().ToList();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<InfoBehaviourScript>());
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehaviourScript desiredInfo)
    {
        foreach (InfoBehaviourScript info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAll()
    {
        foreach (InfoBehaviourScript info in infos)
        {
            info.CloseInfo();
        }
    }
}
