using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndContest : MonoBehaviour
{
    [SerializeField] Sound StageClearSound; 
    public bool isInEnd;
    public bool doorClosed;
    [SerializeField] Door door; 

    public void StageClear()
    {
        if (!door.isOpened && isInEnd)
        {
            GameManager.DataManager.Stage++;
            GameManager.AudioManager.PlayBGM(StageClearSound); 
        }

    }

    private void Update()
    {
        StageClear();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInEnd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isInEnd = false; 
    }
}
