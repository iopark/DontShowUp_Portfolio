using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControllerHelper : MonoBehaviour
{
    
}

//public class SequenceActionRequest: IEnumerator
//{
//    IEnumerator toControl;
//    bool toResume;
//    Action<StateController> requestee; 

//    public SequenceActionRequest(IEnumerator toControl, bool toResume)
//    {
//        this.toControl = toControl;
//        this.toResume = toResume;
//    }

//    public object Current => null; 

//    public bool MoveNext()
//    {
//        if (requestee
//    }

//    public void Reset()
//    {
//        throw new System.NotImplementedException();
//    }
//}

#region Coroutine Manager through custom Coroutine 
//public Queue<MoveRequestSlip> actionRequests = new Queue<MoveRequestSlip>();
//MoveRequestSlip currentRequest;
//MoveRequestSlip previousRequest;
//bool isCompletingAction;
//public bool IsCompletingAction { get { return isCompletingAction; } }
//public void RequestMove(string key, IEnumerator enumerator)
//{
//    MoveRequestSlip newRequest = new MoveRequestSlip(key, enumerator);
//    if (actionRequests.Count < 0 && CheckForEqual(newRequest)) // if new request is considered an equal one, ignore this request. 
//        return;
//    pauseRequests.Enqueue(newRequest);
//    TryCompleteNext(); 
//}
//private void TryCompleteNext()
//{
//    if (!isCompletingAction && actionRequests.Count > 0)
//    {
//        currentRequest = actionRequests.Dequeue();
//        isCompletingAction = true;
//        StartCoroutine(currentRequest.enumerator); 
//    }
//}

//private bool CheckForEqual(MoveRequestSlip other)
//{
//    foreach (MoveRequestSlip slip in  actionRequests)
//    {
//        if (other.Equals(slip))
//            return true;
//    }
//    return false;
//}
///// <summary>
///// Simply call this function for every coroutines to imply a designated action has been finished. 
///// </summary>
///// <param name="success"></param>
//public void FinishedAction(bool success)
//{
//    if (!success)
//    {
//        //TODO: if some designated action is to fail, maybe allow the state to move to different state? if so, what should be placed in for the parameter?? 
//        return;
//    }
//    //How do we Deliever this path to the Requestee? 
//    else if (success)
//    {
//        //TODO: maybe do a next action? 
//        isCompletingAction = false;
//        TryCompleteNext(); // run the next expected coroutine 
//    }
//}

//public void FinishedAction(bool success, System.Action<StateController> nextAction)
//{
//    if (!success)
//    {
//        //TODO: if some designated action is to fail, maybe allow the state to move to different state? if so, what should be placed in for the parameter?? 
//        return;
//    }
//    //How do we Deliever this path to the Requestee? 
//    else if (success)
//    {
//        //TODO: maybe do a next action? 
//        nextAction(this);
//        isCompletingAction = false;
//        TryCompleteNext(); // run the next expected coroutine 
//    }
//}
//public void EmptyQueue()
//{
//    if (coroutines.Count <= 0 && !isCompletingAction)
//        return;
//    else if (isCompletingAction)
//    {
//        StopCoroutine(currentRequest.enumerator);
//        suspendedCoroutine.Clear();
//    }
//    else
//    {
//        suspendedCoroutine.Clear();
//    }
//}
#endregion