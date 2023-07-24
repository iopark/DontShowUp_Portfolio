using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract string actionName { get; }
    [TextArea]
    public string RequiredProceedingAct;
    [TextArea] 
    public string RequiredPreceedingAction;
    public abstract void Act(StateController controller);
}