using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensory : MonoBehaviour, IListenable
{
    protected Enemy enemy;
    protected EnemyMover EnemyMover { get; set; }
    protected bool haveHeard; 
    public bool HaveHeard { get { return haveHeard; } set { haveHeard = value; } }

    private void Start()
    {
        //EnemyMover = GetComponent<EnemyMoverSound>();
        EnemyMover = GetComponent<EnemyMover>();
    }

    public virtual void Heard(Vector3 soundPoint, bool hasWall)
    {
        if (hasWall)
            GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath); 
        else 

        //TODO: Make sure the getter's position is a walkable grid point
        GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath);
    }

    public void GetPath(Vector3[] soundPath, bool success)
    {
        //Preceeding must come determining valid sound travel, if there's any collision to the wall. 
        if (success)
        {
            Debug.Log("Have Heard"); 

            EnemyMover.TraceSoundPoints = new Vector3[soundPath.Length];
            Array.Copy(soundPath, EnemyMover.TraceSoundPoints, soundPath.Length);
            haveHeard = true;
            //EnemyMover.ReactToSound(soundPath);
            // Can be called by the State Controller? 
        }
    }

    public Cell ReturnHeardPoint()
    {
        return GameManager.MapManager.CellFromWorldPoint(transform.position);
    }

    //TODO: Should Return appropriate value to calculate the according path for the hearer to trace to. 


}
