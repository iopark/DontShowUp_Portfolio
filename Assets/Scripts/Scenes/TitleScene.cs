using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        progress = 0.3f;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f;
        yield return new WaitForSecondsRealtime(1f);
        progress = 1f;
        yield return null; 
    }
}
