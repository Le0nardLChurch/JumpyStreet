using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawning : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] public GameObject[] vehicleList;
    protected GameObject vehicle;
    [SerializeField] protected GameObject spawnPoint;
#pragma warning restore 0649
    protected List<GameObject> vehiclePool = new List<GameObject>();
    protected int amountToPool = 10;
    protected float spawnRate = 5;
    protected float speed;


    protected virtual void Start()
    {
        // Random flip to left or right
        if (Random.Range(0, 2) == 0)
        {
            transform.Rotate(Vector3.up, 180);
        }
        SetupVehiclePool();

        spawnRate = vehicle.GetComponent<VehicleMovement>().spawnRate;

        Invoke("SpawnVehicle", Random.Range(0.5f, 1.5f));
    }

    protected void SetupVehiclePool()
    {
        // Get random vehicle 
        vehicle = vehicleList[Random.Range(0, vehicleList.Length - 1)];
        amountToPool = vehicle.GetComponent<VehicleMovement>().spawnAmount;
        speed = vehicle.GetComponent<VehicleMovement>().speed;

        for (int i = 0; i < amountToPool; i++)
        {
            AddVehicleToPool(i);
        }
    }

    protected void AddVehicleToPool(int i)
    {
        GameObject go = Instantiate(vehicle, spawnPoint.transform.position + (Vector3.right * i), spawnPoint.transform.rotation);
        go.GetComponent<VehicleMovement>().speed = speed;
        go.transform.parent = transform;
        go.SetActive(false);
        vehiclePool.Add(go);
    }

    // Get the first inactive veh
    protected GameObject GetVehicleFromPool()
    {
        for (int i = 0; i < vehiclePool.Count; i++)
        {
            if (!vehiclePool[i].activeInHierarchy)
            {
                return vehiclePool[i];
            }
        }

        AddVehicleToPool(0);
        return vehiclePool[vehiclePool.Count - 1];
    }

    protected void SpawnVehicle()
    {
        GameObject go = GetVehicleFromPool();
        go.transform.position = spawnPoint.transform.position;
        go.SetActive(true);
        Invoke("SpawnVehicle", spawnRate + Random.Range(-0.5f, 0.5f));
    }

}
