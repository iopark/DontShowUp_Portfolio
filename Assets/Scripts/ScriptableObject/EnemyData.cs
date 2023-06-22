using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Registry/Enemy")]
public class EnemyData : ScriptableObject
{
    //Requires 1. Enemy Prefab with the component, Enemy 
    //Requires 2. Enemy Stat according to the different stages of the enemy. 
    [SerializeField] private EnemyLevelInfo[] enemyStages;

    public EnemyLevelInfo[] EnemyStages
    {
        get { return enemyStages; }
    }

    [System.Serializable]
    public class EnemyLevelInfo
    {
        public Enemy enemy;
        public EnemyStat enemyStat;
    }

    public EnemyStat AccessLevelData(int level)
    {
        return EnemyStages[level].enemyStat;
    }

    public EnemyStat AccessLevelData()
    {
        //Default 
        return EnemyStages[0].enemyStat;
    }
}
