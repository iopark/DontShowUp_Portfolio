using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    public void Heard(Vector3 soundPoint, bool hasWall);
    public void GetPath(Vector3[] soundPath, bool successs); 
}
