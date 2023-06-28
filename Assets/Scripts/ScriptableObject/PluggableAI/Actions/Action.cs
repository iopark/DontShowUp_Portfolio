using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [TextArea]
    public string actionName; 
    public string RequiredProceedingAct;
    [TextArea] 
    public string RequiredPreceedingAction;
    public abstract void Act(StateController controller);
}