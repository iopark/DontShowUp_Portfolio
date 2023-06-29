using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Act : ScriptableObject
{
    #region There should be list of elements required to perform such an act, 
    [TextArea]
    public string aboutThisAct;
    [SerializeField] protected string animTrigger;
    public string AnimTrigger { get { return animTrigger; } }
    [SerializeField] protected AnimType animType;
    public AnimType AnimType { get { return animType; } }

    [SerializeField] protected float animFloatValue;
    [SerializeField] protected bool animBoolValue;

    [SerializeField] protected Action[] actionsToStop;
    [SerializeField] protected bool stopAllCoroutine; 
    #endregion

    public abstract void Perform(StateController controller); 
}
