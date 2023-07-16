using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ZombieType
{
    SleeperZ, 
    SearchZ, 
    PatrolZ, 
    Size
}
public class SpawnManager : MonoBehaviour
{
    //Embodies characteristic of the Director AI 
    public ZombieTypes zombies; 
    public int curZombie; 
    public int maxZombie; 

    public Vector3[] spawnSpots;
    WaitForSeconds spawnInterval;
    Coroutine Spawnroutine;

    private void Awake()
    {
        GameManager.Instance.GameSetup += InitializeStageData; 
    }


    public void InitializeStageData()
    {
        StopAllCoroutines();
        if (zombies == null)
            zombies = Resources.Load<ZombieTypes>("Data/Zombie/ZombieList"); 
        float timer = GameManager.DataManager.CurrentGameData.spawnTimer;
        spawnInterval = new WaitForSeconds(timer);
        maxZombie = GameManager.DataManager.MaxZombie;
        curZombie = maxZombie; 
        bool spawnerExists = GameManager.DataManager.CurrentGameData.InitializeSpawnPoints(out spawnSpots);
        if (!spawnerExists)
            return;
        Spawnroutine = StartCoroutine(Spawner());
    }

    public void SpawnEnemies()
    {
        while (curZombie < maxZombie)
        {
            int randomZombie = UnityEngine.Random.Range(0, (int)ZombieType.Size); 
            int randomSpot = UnityEngine.Random.Range(0, spawnSpots.Length);
            Vector3 randomPos = Random.insideUnitCircle;
            Vector3 spawnPoint = new Vector3(spawnSpots[randomSpot].x + randomPos.x, 0, spawnSpots[randomSpot].z + randomPos.y);
            GameManager.Pool.Get(zombies.zombieList[randomZombie], spawnPoint, Quaternion.identity, null); 
        }
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return spawnInterval;
            SpawnEnemies(); 
        }
    }
}
