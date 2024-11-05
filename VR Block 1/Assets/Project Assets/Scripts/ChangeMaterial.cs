using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    private Material currentMaterial;
    [SerializeField] private Material newMaterial;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponentInChildren<Renderer>().material;
    }

    /// <summary>
    /// Call this method to change the material of an object. Reference to new material needs to be set.
    /// </summary>
    public void ChangeToNewMaterial()
    {
        this.GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void ChangeToOldMaterial()
    {
        this.GetComponentInChildren<Renderer>().material = currentMaterial;
    }
}
