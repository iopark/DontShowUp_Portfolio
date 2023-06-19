using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    public void Listen(Vector3 soundPoint);

    public Cell ReturnHeardPoint(); 
    public void GetPath(Vector3[] soundPath, bool successs); 
}
