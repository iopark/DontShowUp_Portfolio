using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act : ScriptableObject
{
    #region There should be list of elements required to perform such an act, 
    #endregion

    public abstract void Perform(StateController controller); 

}
