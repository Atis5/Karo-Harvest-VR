using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraOffset : MonoBehaviour
{
    [SerializeField] private GameObject cameraOffset;

    // Start is called before the first frame update
    /*void Start()
    {
        cameraOffset.transform.position = new Vector3(0, -0.5f, 0);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        cameraOffset.transform.position = new Vector3(0, -0.5f, 0);
    }
}
