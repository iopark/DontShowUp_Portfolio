using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensory : MonoBehaviour, IListenable
{
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Listen(Transform trans)
    {
        throw new System.NotImplementedException();
    }

    public Cell ReturnHeardPosition()
    {
        return GameManager.MapManager.CellFromWorldPoint(transform.position);
    }

    public void GetPath(Vector3[] soundPath, bool successs)
    {
        if (successs)
        {
            enemy.ReactToSound(soundPath);
        }
    }

    //TODO: Should Return appropriate value to calculate the according path for the hearer to trace to. 
}
