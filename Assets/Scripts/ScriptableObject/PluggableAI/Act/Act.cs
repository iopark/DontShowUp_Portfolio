using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act : ScriptableObject
{
    #region There should be list of elements required to perform such an act, 
    [SerializeField] protected string animTrigger;
    [SerializeField] protected int animType; 
    #endregion

    public abstract void Perform(StateController controller); 

}
