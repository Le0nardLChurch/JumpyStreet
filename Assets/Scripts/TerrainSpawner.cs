using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
#pragma warning disable 0649

    [Header("Terrain Prefabs")]
    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject[] waterPrefabs;
    [SerializeField] GameObject trackPrefab;
    [Header("Odds of spawning (Total needs to be 100)")]
    [SerializeField] int grassChance = 35;
    [SerializeField] int roadChance = 25;
    [SerializeField] int waterChance = 20;
    [SerializeField] int trackChance = 20;
#pragma warning restore 0649

    private Vector3 spawnPoint;
    private Queue<GameObject> currentTerrainList = new Queue<GameObject>();

    // Tracking the items to have a max of the same in a row
    private string[] trainTracking = { "", "", "" };
    private int trackingNum = 0;

    private Transform playerPos;
    private float playerMaxZ = 0;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = transform.position;
        if (grassChance + roadChance + waterChance + trackChance != 100)
        {
            Debug.LogError("Spawning odds don't total 100");
        }
        for (int i = 0; i < 20; i++)
        {
            SpawnNextTerrain();
        }
    }

    void Update()
    {

        playerMaxZ = Mathf.Max(playerPos.position.z, playerMaxZ);

        if (playerMaxZ > spawnPoint.z - 15)
        {
            SpawnNextTerrain();
        }

        if (currentTerrainList.Count > 25)
        {
            RemoveFirstTerrain();
        }
    }

    private void SpawnNextTerrain()
    {


        GameObject nextTerrain;
        // Used to stop more than 3 in a row of the same kind
        if (trainTracking[0] == trainTracking[1] && trainTracking[1] == trainTracking[2])
        {
            nextTerrain = (trainTracking[0] == grassPrefab.name) ? roadPrefab : grassPrefab;
        }
        else
        {
            int r = Random.Range(1, 101);

            if (r <= grassChance)
            {
                nextTerrain = grassPrefab;
            }
            else if (r <= roadChance + grassChance)
            {
                nextTerrain = roadPrefab;
            }
            else if (r <= waterChance + roadChance + grassChance)
            {
                nextTerrain = waterPrefabs[Random.Range(0, waterPrefabs.Length)];
            }
            else
            {
                nextTerrain = trackPrefab;
            }
        }
        trainTracking[trackingNum] = nextTerrain.name;
        trackingNum = (trackingNum == 2) ? 0 : trackingNum + 1;

        GameObject t = Instantiate(nextTerrain, spawnPoint, nextTerrain.transform.rotation, transform);
        spawnPoint.z += 1;
        currentTerrainList.Enqueue(t);
    }
    private void RemoveFirstTerrain()
    {
        GameObject t = currentTerrainList.Dequeue();
        Destroy(t);
    }

}
