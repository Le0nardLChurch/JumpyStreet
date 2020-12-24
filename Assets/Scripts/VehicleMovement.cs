using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed;
    public int spawnAmount;
    public float spawnRate;


    private void FixedUpdate()
    {
        transform.position += (-transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            gameObject.SetActive(false);
        }
    }

}
