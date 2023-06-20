using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : Enemy
{
    private void Awake()
    {
        data = GameManager.Resource.Load<EnemyData>("Data/Zombie/BasicZombie");
        ImportEnemyData();
    }
    protected override void ImportEnemyData()
    {
        EnemyStat currentData = data.AccessLevelData(CurrentLevel);
        currentData.SyncPhysicalData(this);
        currentData.SyncSightData(sight); 
    }
}
