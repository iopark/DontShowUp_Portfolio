using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

/// <summary>
/// TemporaryState Controller Inheriting NormalZombie Class
/// </summary>
public class StateController : MonoBehaviour
{
    #region GetSet Unitbodies, must be declared on the Awake 
    public Enemy Enemy { get; private set; }
    public EnemyAttacker EnemyAttacker { get; private set; }
    public EnemyMover EnemyMover { get; private set; }
    public SightSensory Sight { get; private set; }
    public SoundSensory Auditory { get; private set; }
    #endregion

    [Header("Unit State")]
    public State currentState;
    public State remainState;
    public State previousState;

    //[Header("StateRequired Elements")]
    //public bool isAligning; 
    //public 
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        EnemyAttacker = GetComponent<EnemyAttacker>();
        EnemyMover = GetComponent<EnemyMover>();
        Sight = GetComponent<SightSensory>();
        Auditory = GetComponent<SoundSensory>();
    }


    private void Update()
    {
        currentState.UpdateState(this);
    }
    private void FixedUpdate()
    {
        //currentState.FixedUpdateState(this);
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState.ExitState(this);
            previousState = currentState;
            currentState = nextState;
            name = currentState.name;
            AnimationUpdate(); 
            currentState.EnterStateAct(this);
            //currentState.EnterStateActions(this);
        }
        return;
    }

    public void AnimEvent()
    {

    }
    public void AnimationUpdate()
    {
        AnimRequestSlip? stateAnim = currentState.EnterStateAnim();
        if (stateAnim == null)
            return;
        Enemy.AnimationUpdate((AnimRequestSlip)stateAnim); 
    }

    #region attempting to perform request for future path in delegated ways

    public List<CoroutineSlip> coroutines = new List<CoroutineSlip>();
    IEnumerator toRun;

    public void RunAndSaveForReset(string coroutineKey, IEnumerator _routine)
    {
        if (CheckOverlapCoroutine(coroutineKey, _routine))
            return;
        CoroutineSlip newSlip = new CoroutineSlip(coroutineKey, _routine);
        coroutines.Add(newSlip);
        StartCoroutine(_routine);
    }

    public void SignalCoroutineFinish(string coroutineKey)
    {
        foreach (CoroutineSlip slip in coroutines)
        {
            if (slip.Equals(coroutineKey))
            {
                slip.CoroutineFinished(); 
            }
        }
    }
    public void RestartCoroutine(string coroutineKey, IEnumerator _routine)
    {
        foreach (CoroutineSlip name in coroutines)
        {
            if (name.Equals(coroutineKey))
            {
                StopCoroutine(name.routine);
                name.ChangeRoutine(_routine);
                StartCoroutine(name.routine);
                return;
            }
        }
        IEnumerator routine = _routine;
        StartCoroutine(routine); 
        CoroutineSlip newSlip = new CoroutineSlip(coroutineKey, _routine);
        coroutines.Add(newSlip);
    }

    public void RemoveFromCoroutineList(string slipKey)
    {
        foreach (CoroutineSlip slip in coroutines)
        {
            if (slip.Equals(slipKey))
            {
                if (slip.routine != null)
                    StopCoroutine(slip.routine);
                slip.SetToNull(); 
                coroutines.Remove(slip);
                return;
            }
        }
    }

    private bool CheckOverlapCoroutine(string slipKey, IEnumerator _routine)
    {
        foreach (CoroutineSlip name in coroutines)
        {
            if (name.Equals(slipKey))
            {
                if (name.hasFinished)
                {
                    name.ChangeRoutine(_routine); 
                    StartCoroutine(name.routine);
                }
                return true;
            }
        }
        return false; 
    }

    /// <summary>
    /// if coroutine exists and if its not running, then stop the coroutine. 
    /// </summary>
    /// <param name="slipKey"></param>
    public void ResetCoroutine(string slipKey)
    {
        if (coroutines.Count == 0)
            return;
        foreach (CoroutineSlip name in coroutines)
        {
            if (name.Equals(slipKey))
            {
                StopCoroutine(name.routine);
                name.CoroutineFinished();
                return;
                //toDestroy = name.routine;
            }
        }
        return;
    }
    public void ResetAllCoroutines()
    {
        if (coroutines.Count == 0)
            return; 
        foreach(CoroutineSlip slip in coroutines)
        {
            if (slip.hasFinished)
                continue;
            StopCoroutine(slip.routine);
            slip.CoroutineFinished();
        }
    }
    #endregion
    protected void OnDrawGizmos()
    {
        Gizmos.color = currentState.sceneGizmoColor;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}