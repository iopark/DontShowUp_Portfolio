using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void RunAndSaveForReset(string slipKey, IEnumerator _routine)
    {
        if (CheckOverlapCoroutine(slipKey, _routine))
            return;
        CoroutineSlip newSlip = new CoroutineSlip(slipKey, _routine);
        coroutines.Add(newSlip);
        StartCoroutine(_routine);
    }

    public void SignalCoroutineFinish(string slipKey)
    {
        if (coroutines.Count == 0)
            return;

        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Equals(slipKey))
            {
                coroutines[i].CoroutineFinished();
                return;
            }
        }
    }
    public void RestartCoroutine(string slipKey, IEnumerator _routine)
    {
        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Equals(slipKey))
            {
                StopCoroutine(coroutines[i].routine);
                coroutines[i].ChangeRoutine(_routine);
                StartCoroutine(coroutines[i].routine);
                return;
            }
        }
        IEnumerator routine = _routine;
        StartCoroutine(routine); 
        CoroutineSlip newSlip = new CoroutineSlip(slipKey, _routine);
        coroutines.Add(newSlip);
    }

    public void RemoveFromCoroutineList(string slipKey)
    {
        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Equals(slipKey))
            {
                if (coroutines[i].routine != null)
                    StopCoroutine(coroutines[i].routine);
                coroutines[i].SetToNull();
                coroutines.Remove(coroutines[i]);
                return;
            }
        }
    }

    private bool CheckOverlapCoroutine(string slipKey, IEnumerator _routine)
    {
        if (coroutines.Count == 0)
            return false; 
        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Equals(slipKey))
            {
                if (coroutines[i].hasFinished)
                {
                    coroutines[i].ChangeRoutine(_routine);
                    StartCoroutine(_routine);
                    return true;
                }
                return false; 
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

        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Equals(slipKey))
            {
                StopCoroutine(coroutines[i].routine);
                coroutines[i].CoroutineFinished();
                return;
                //toDestroy = name.routine;
            }
        }
    }
    public void ResetAllCoroutines()
    {
        if (coroutines.Count == 0)
            return;
        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].hasFinished)
                continue; 
            StopCoroutine(coroutines[i].routine);
            coroutines[i].CoroutineFinished();
        }
    }
    #endregion
    protected void OnDrawGizmos()
    {
        Gizmos.color = currentState.sceneGizmoColor;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}