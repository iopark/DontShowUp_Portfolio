using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndContest : MonoBehaviour
{
    [SerializeField] Sound StageClearSound; 
    public bool isInEnd;
    public bool doorClosed;
    public bool stageCleared; 
    [SerializeField] Door door;

    private void OnDisable()
    {
        StopAllCoroutines();
        stageCleared = false;
    }
    public void StageClear()
    {
        if (!door.isOpened && isInEnd)
        {
            stageCleared = true;
            GameManager.DataManager.Stage++;
            GameManager.AudioManager.PlayEffect(StageClearSound); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInEnd = true;
        }
        StartCoroutine(StageCheck()); 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isInEnd = false; 
    }

    IEnumerator StageCheck()
    {
        while (!stageCleared)
        {
            StageClear(); 
            yield return null;
        }
    }
}
