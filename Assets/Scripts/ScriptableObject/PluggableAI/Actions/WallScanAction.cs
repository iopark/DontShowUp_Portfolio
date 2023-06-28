using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScanAction : Action
{
    public override void Act(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    private void ScanWall(StateController controller)
    {
        //TODO: 3 Directional search, left right, and forward. 
        //If 1 direction is viable
                // if forward: simply turn back 
                // else go to that 
        // if 2 direction is viable, then turn pick from two. 
        // if 3 direction is viable, then pick from three. 
    }
}
