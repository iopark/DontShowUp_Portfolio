using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage_", menuName = "Registry/StageInfo")]
public class SingleStage : ScriptableObject
{
    public int maxZombie;
    public int numberOfRewards; 
    public int requiredDiamonds;
    public float InitialTimer;

    public Transform[] spawnPoints; 
}
