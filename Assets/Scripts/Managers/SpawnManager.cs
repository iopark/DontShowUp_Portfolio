using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Embodies characteristic of the Director AI 
    public float timer;
    public Transform playerSpawningSpot; 
    public Transform[] spawnSpots; 
    MapManager mapManager;
    CombatManager combatManager; 
    Vector3 playerLoc; 

    private void Start()
    {
        combatManager = GameManager.CombatManager; 
        mapManager = GameManager.MapManager; 
    }

    //IEnumerator SpawnActivity()
    //{
    //    //TODO: Check Player's current location; 
    //    //TODO: Should retrieve location where player cannot see. 
    //    //TODO: Apply Shyball Project for this matter. 
    //}
}
