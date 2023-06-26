using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensory : MonoBehaviour, IListenable
{
    Enemy enemy;
    EnemyMoverSound EnemyMover { get; set; }
    //EnemyMover EnemyMover { get; set; }
    private bool haveHeard; 
    public bool HaveHeard { get { return haveHeard; } set { haveHeard = value; } }

    private void Start()
    {
        EnemyMover = GetComponent<EnemyMoverSound>();
        //EnemyMover = GetComponent<EnemyMover>();
    }

    public void Heard(Vector3 soundPoint)
    {
        GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath);
    }

    public void GetPath(Vector3[] soundPath, bool success)
    {
        if (success)
        {
            Debug.Log("Have Heard"); 
            haveHeard = true; 
            EnemyMover.ReactToSound(soundPath);
            // Can be called by the State Controller? 
        }
    }

    public Cell ReturnHeardPoint()
    {
        return GameManager.MapManager.CellFromWorldPoint(transform.position);
    }

    //TODO: Should Return appropriate value to calculate the according path for the hearer to trace to. 


}
