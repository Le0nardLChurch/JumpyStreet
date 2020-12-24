using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomization : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject[] leaves;
#pragma warning restore 0649


    void Start()
    {
        for (int i = Random.Range(0, leaves.Length); i < leaves.Length; i++)
        {
            leaves[i].SetActive(false);
        }
    }
}
