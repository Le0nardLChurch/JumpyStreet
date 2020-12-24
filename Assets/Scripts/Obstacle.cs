using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] bool isObstacle;
    [SerializeField] bool isWater;
#pragma warning restore 0649

    public bool IsObstacle { get { return isObstacle; } }
    public bool IsWater { get { return isWater; } }

}





