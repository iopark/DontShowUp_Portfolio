using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    public void Listen(Transform trans);

    public Cell ReturnHeardPosition();

    public void GetPath(Vector3[] soundPath, bool successs); 
}
