using UnityEngine;

public class GrassBlockersSpawning : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject[] treesAndBushes;
    [SerializeField] GameObject coinPrefab;
#pragma warning restore 0649
    private int[] randXVals = { 1, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 6 };

    void Start()
    {
        SpawnEdges();
        SpawnCenter();
    }

    private void SpawnCenter()
    {
        for (int nextX = -14; nextX < 15;)
        {
            int x = randXVals[Random.Range(0, randXVals.Length)];
            Vector3 SpawnPos = new Vector3(transform.position.x + nextX, transform.position.y, transform.position.z);
            SpawnItemAtLocation(SpawnPos);
            if(x > 3 && 0 == Random.Range(0,5))// 20% chance of coin in space next to tree About 66% of spots match so odds are ~13%
            {

                GameObject coin = Instantiate(coinPrefab, SpawnPos + new Vector3(-2,0.75f,0), Quaternion.Euler(0, 180, 0));
            }
            nextX += x;
        }

    }

    // Spawn blocking items the last 10 blocks on each side
    private void SpawnEdges()
    {
        for (int i = 1; i < 11; i++)
        {
            Vector3 SpawnPos = new Vector3(transform.position.x + 25 - i, transform.position.y, transform.position.z);
            SpawnItemAtLocation(SpawnPos);
            SpawnPos = new Vector3(transform.position.x - 25 + i, transform.position.y, transform.position.z);
            SpawnItemAtLocation(SpawnPos);
        }
    }

    private void SpawnItemAtLocation(Vector3 spawnPos)
    {
        GameObject go = treesAndBushes[Random.Range(0,treesAndBushes.Length)];
        go = Instantiate(go, go.transform.position + spawnPos, go.transform.rotation);
        go.transform.parent = transform;

    }
}
