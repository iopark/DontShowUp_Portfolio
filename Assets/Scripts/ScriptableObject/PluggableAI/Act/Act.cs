using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Act : ScriptableObject
{
    #region There should be list of elements required to perform such an act, 
    [SerializeField] protected string animTrigger;
    public string AnimTrigger { get { return animTrigger; } }
    [SerializeField] protected int animType;
    public int AnimType { get { return animType; } }
    #endregion

    public abstract void Perform(StateController controller);

}
