using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLilyPads : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject[] lilyPadList;
    [SerializeField] private GameObject coinPrefab;
    private GameObject pad;
    [SerializeField] private GameObject spawnPoint;
    private int[] randXVals = { 1, 1, 2, 2, 3, 3, 3 };
#pragma warning restore 0649


    // Start is called before the first frame update
    void Start()
    {
        SpawnCenter();
    }

    private void SpawnCenter()
    {
        for (int nextX = -14; nextX < 15;)
        {
            int x = randXVals[Random.Range(0, randXVals.Length)];
            Vector3 SpawnPos = new Vector3(transform.position.x + nextX, transform.position.y, transform.position.z);
            SpawnItemAtLocation(SpawnPos);
            nextX += x;
        }

    }

    private void SpawnItemAtLocation(Vector3 spawnPos)
    {

        GameObject go = lilyPadList[Random.Range(0, lilyPadList.Length)];
        go = Instantiate(go, go.transform.position + spawnPos, go.transform.rotation);
        go.transform.parent = transform;

    }
}
