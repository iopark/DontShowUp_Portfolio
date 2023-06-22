using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : Enemy
{

    protected override void Awake()
    {
        data = GameManager.Resource.Load<EnemyData>("Data/Zombie/BasicZombie");
        base.Awake();
        ImportEnemyData();
    }
    protected override void ImportEnemyData()
    {
        CurrentStat = data.AccessLevelData(CurrentLevel);
    }
}
