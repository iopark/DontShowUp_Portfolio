using UnityEngine;

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
    [Header("preRequisteActions")]

    [SerializeField] protected Action[] actions;
    [SerializeField] protected Transition[] transitions;

    [SerializeField] protected Act[] exitActs;
    //You could also do Decisions[], if requiring wider range or options to converge with other states. 
    public Color sceneGizmoColor = Color.grey;
    public void UpdateState(StateController controller)
    {
        DoActions(controller); 
        CheckTransition(controller);
    }

    public void DoActions(StateController controller)
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
    public void EnterStateAct(StateController controller)
    {
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
