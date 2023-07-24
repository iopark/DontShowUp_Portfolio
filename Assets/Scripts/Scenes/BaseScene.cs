using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public float progress { get; protected set; }

    protected abstract IEnumerator LoadingRoutine(); 
    public virtual void SetPlayerPos() { }

    public void LoadAsync()
    {
        StartCoroutine(LoadingRoutine());
    }
}
