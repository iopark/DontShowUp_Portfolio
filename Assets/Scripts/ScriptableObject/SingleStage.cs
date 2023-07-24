using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage_", menuName = "Registry/StageInfo")]
public class SingleStage : ScriptableObject
{
    public string stageName; 
    public int maxZombie;
    public int numberOfRewards; 
    public int requiredDiamonds;
    public float spawnTimer;

    public bool InitializeSpawnPoints(out Vector3[] spawnPoints)
    {
        GameObject[] SpawningLists = GameObject.FindGameObjectsWithTag("EnemySpawner"); 
        if (SpawningLists.Length == 0)
        {
            //Debug.Log("List is null");
            spawnPoints = null; 
            return false; 
        }
        spawnPoints = new Vector3[SpawningLists.Length];
        for (int i = 0; i < SpawningLists.Length; i++)
        {
            spawnPoints[i] = SpawningLists[i].transform.position; 
        }
        return true; 
    }
}
