using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameScene : BaseScene
{
    public GameObject player;
    public Transform playerPosition;
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0; 
        Debug.Log("Random Map Generation"); 
        yield return new WaitForSecondsRealtime(0.5f);
        progress = 0.3f;
        Debug.Log("Allocate Player");
        player.transform.position = playerPosition.position;
        player.transform.rotation = playerPosition.rotation;

        //freeLookCamera.LookAt = player.transform;
        //freeLookCamera.Follow = player.transform;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f; 
        Debug.Log("Allocate Enemies");
        yield return new WaitForSecondsRealtime(1f);
        progress = 1f; 
    }

    public void InitializePlayerPosition(Transform spawnPoint)
    {

    }
}
