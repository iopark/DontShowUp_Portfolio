using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : Enemy
{
    private EnemyStat currentStat;
    public EnemyStat CurrentStat { get { return currentStat; } }    
    private void Awake()
    {
        data = GameManager.Resource.Load<EnemyData>("Data/Zombie/BasicZombie");
        ImportEnemyData();
    }
    protected override void ImportEnemyData()
    {
        currentStat = data.AccessLevelData(CurrentLevel);
        currentStat.SyncPhysicalData(this);
        currentStat.SyncSightData(sight); 
    }
}
