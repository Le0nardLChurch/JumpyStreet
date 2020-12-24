using UnityEngine;

public class LogSpawning : TrafficSpawning
{
    // Start is called before the first frame update
    protected override void Start()
    {
        SetupVehiclePool();
        // Random chance for logs to move left or right
        if (Random.Range(0, 2) == 0)
        {
            foreach (GameObject log in vehiclePool)
            {
                log.GetComponent<VehicleMovement>().speed *= -1;
            }
            Vector3 spawnPos = spawnPoint.transform.position;
            spawnPos.x *= -1;
            spawnPoint.transform.position = spawnPos;
        }

        spawnRate = vehicle.GetComponent<VehicleMovement>().spawnRate;
        Invoke("SpawnVehicle", Random.Range(0.5f, 1.5f));
    }
}
