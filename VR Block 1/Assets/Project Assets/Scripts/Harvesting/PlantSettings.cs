using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSettings : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("How many seconds it takes for a plant to fully grow. Divided by 3 for each stage.")]
    public float GrowthTime;

    [Tooltip("How many seconds a plant can survive in wrong humidity before it dies.")]
    public float TimeToDie;

}
