using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndContest : MonoBehaviour
{
    public bool isInEnd;
    public bool doorClosed;
    [SerializeField] Door door; 

    public void StageClear()
    {
        if (doorClosed && isInEnd)
            GameManager.DataManager.GameEnd?.Invoke(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInEnd = true;
            StageClear(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isInEnd = false; 
    }
}
