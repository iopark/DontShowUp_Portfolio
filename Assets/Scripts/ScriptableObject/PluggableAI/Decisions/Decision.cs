using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    //This is inner workings which 'decides' trigger to switch between states.
    public abstract bool Decide(StateController controller); 
}
