using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public static class Extension
{
    public static bool Contain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeInHierarchy;
    }

    public static bool IsValid(this Component component)
    {
        return component != null && component.gameObject.activeInHierarchy;
    }

    public struct ParallelTester : IJobParallelFor
    {
        public void Execute(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}
