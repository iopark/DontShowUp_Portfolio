using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedSoundSensory : SoundSensory
{

    public override void Heard(Vector3 soundPoint)
    {
        CheckSoundOverlap(soundPoint); 
        GameManager.PathManager.RequestPath(transform.position, soundPoint, GetPath);
    }

    private void CheckSoundOverlap(Vector3 soundPoint)
    {
        if (HaveHeard)
        throw new NotImplementedException();
    }
}
