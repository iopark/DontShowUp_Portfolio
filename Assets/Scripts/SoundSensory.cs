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

    public bool Heard(Vector3 soundPoint)
    {
        GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath);
        return true;
    }

    public void GetPath(Vector3[] soundPath, bool success)
    {
        if (success)
        {
            enemy.ReactToSound(soundPath);
        }
    }

    public Cell ReturnHeardPoint()
    {
        return GameManager.MapManager.CellFromWorldPoint(transform.position);
    }

    //TODO: Should Return appropriate value to calculate the according path for the hearer to trace to. 
}
