using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    public bool Heard(Vector3 soundPoint);
    //public void GetPath(Vector3[] soundPath, bool successs); 
}
