using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class State : ScriptableObject
{
    Action[] actions;
    Transition[] transitions;
    //You could also do Decisions[], if requiring wider range or options to converge with other states. 
    public Color sceneGizmoColor = Color.grey;
    public void UpdateState(StateController controller)
    {
        DoActions(controller); // Upon Changing into a certain state, a State contains 'set' of actions, which will iterate until doing every bit of the given actions 
        CheckTransition(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransition(StateController controller)
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
