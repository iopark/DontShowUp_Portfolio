using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="State_", menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    //Do we categorize the States? 
    // 
    protected Action[] actions;
    protected Action[] fixedActions; 
    protected Transition[] transitions;
    //You could also do Decisions[], if requiring wider range or options to converge with other states. 
    public Color sceneGizmoColor = Color.grey;
    public virtual void UpdateState(StateController controller)
    {
        DoActions(controller); // Upon Changing into a certain state, a State contains 'set' of actions, which will iterate until doing every bit of the given actions 
        CheckTransition(controller);
    }

    public virtual void FixedUpdateState(StateController controller)
    {
        if (fixedActions.Length == 0)
            return;
        DoFixedActions(controller);
    }

    protected virtual void DoFixedActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            fixedActions[i].Act(controller); 
        }
    }
    protected virtual void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    protected virtual void CheckTransition(StateController controller)
    {
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
