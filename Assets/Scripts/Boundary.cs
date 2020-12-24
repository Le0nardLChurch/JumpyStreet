using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 lastPlayerPos;

    void Update()
    {
        if (player.hasChanged)
        {
            transform.position = (Vector3.forward * player.position.z);
            //lastPlayerPos = player.position;
            player.hasChanged = false;
        }
    }

    void Awake()
    {
        player = player == null ? FindObjectOfType<Player>().transform : player;
    }

}
