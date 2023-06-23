using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "State_", menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    //Do we categorize the States? 
    // 
    [Header("State Default Animation")]
    [SerializeField] private bool noAnimUpdate; 
    [SerializeField] private string animName;
    [SerializeField] private AnimType animType;
    [SerializeField] private bool animBool;
    [SerializeField] private float animFloat;

    [Header("State Anim Bool and Float")]
    [SerializeField] private bool setBoolAsNull;
    [SerializeField] private bool setFloatAsNull;

    [Header("UponEnter")]
    [SerializeField] protected Act[] preRequisiteActs;
    [SerializeField] protected Action[] preRequisiteActions;

    [SerializeField] protected Action[] actions;
    [SerializeField] protected Transition[] transitions;

    [Header("Fixed")]
    [SerializeField] protected Action[] fixedActions;
    [SerializeField] protected Transition[] fixedTransitions;

    [SerializeField] protected Act[] exitActs;
    //You could also do Decisions[], if requiring wider range or options to converge with other states. 
    public Color sceneGizmoColor = Color.grey;
    public virtual void UpdateState(StateController controller)
    {
        DoActions(controller); // Upon Changing into a certain state, a State contains 'set' of actions, which will iterate until doing every bit of the given actions 
        CheckTransition(controller);
        if (fixedActions.Length == 0 && fixedTransitions.Length == 0)
            return;
        DoFixedActions(controller);
        CheckFixedTransition(controller);
    }

    public virtual void FixedUpdateState(StateController controller)
    {
        if (fixedActions.Length == 0 && fixedTransitions.Length == 0)
            return;
        DoFixedActions(controller);
        CheckFixedTransition(controller);
    }

    protected virtual void DoFixedActions(StateController controller)
    {
        if (fixedActions.Length == 0)
            return;
        for (int i = 0; i < actions.Length; i++)
        {
            fixedActions[i].Act(controller);
        }
    }
    protected virtual void DoActions(StateController controller)
    {
        if (actions.Length == 0)
            return;
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
    public AnimRequestSlip? EnterStateAnim()
    {
        if (noAnimUpdate)
            return null; 
        if (setBoolAsNull && setFloatAsNull)
        {
            return new AnimRequestSlip(animType, animName);
        }
        else if (setBoolAsNull)
            return new AnimRequestSlip(animType, animName, animFloat);
        else
            return new AnimRequestSlip(animType, animName, animBool); 
    }
    public void EnterState(StateController controller)
    {
        if (preRequisiteActions.Length == 0)
            return;
        for (int i = 0; i < preRequisiteActions.Length; i++)
        {
            preRequisiteActions[i].Act(controller);
        }
        if (preRequisiteActs.Length == 0)
            return;
        for (int i = 0; i < preRequisiteActs.Length; i++)
        {
            preRequisiteActs[i].Perform(controller);
        }
    }

    public void ExitState(StateController controller)
    {
        if (exitActs.Length == 0)
            return;
        for (int i = 0; i < exitActs.Length; i++)
        {
            exitActs[i].Perform(controller);
        }
    }

    protected virtual void CheckFixedTransition(StateController controller)
    {
        if (fixedTransitions.Length == 0)
            return;
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decision = transitions[i].decision.Decide(controller);

            if (decision)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
                controller.TransitionToState(transitions[i].falseState);
        }
    }
    protected virtual void CheckTransition(StateController controller)
    {
        if (transitions.Length == 0)
            return;
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decision = transitions[i].decision.Decide(controller);

            if (decision)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
                controller.TransitionToState(transitions[i].falseState);
        }
    }
}
