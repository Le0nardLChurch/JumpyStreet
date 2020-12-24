using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModel : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject deathModel;
    [SerializeField] GameObject playerModel;
    [SerializeField] Mesh[] playerMeshes;
    [SerializeField] Material[] playerMaterials;
#pragma warning restore 0649


    public void SwitchPlayer(int index)
    {
        SwitchMeshes(index);
        SwitchMaterials(index);
    }
    public void SwitchMeshes(int index)
    {
        playerModel.GetComponent<MeshFilter>().mesh = playerMeshes[index];
    }
    public void SwitchMaterials(int index)
    {
        playerModel.GetComponent<MeshRenderer>().material = playerMaterials[index];
        foreach (var childMesh in deathModel.GetComponentsInChildren<MeshRenderer>())
        {
            childMesh.material = playerMaterials[index];
        }
    }
}
