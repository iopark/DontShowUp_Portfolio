using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public void StartButton()
    {
        GameManager.SceneManager.LoadScene("GameScene"); 
        //ALERT: 앞으로는 우리 씬매니저 쓰면서 로딩하는것이 정배다. 

    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return null; 
    }
}
