using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedSoundSensory : SoundSensory
{

    public override void Heard(Vector3 soundPoint, bool hasWall)
    {
        CheckSoundOverlap(soundPoint); 
        GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath, hasWall);
    }

    private void CheckSoundOverlap(Vector3 soundPoint)
    {
        if (HaveHeard)
        throw new NotImplementedException();
    }
}
