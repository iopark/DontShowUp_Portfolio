using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public void StartButton()
    {
        GameManager.SceneManager.LoadScene("GameScene"); 
        //ALERT: �����δ� �츮 ���Ŵ��� ���鼭 �ε��ϴ°��� �����. 

    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return null; 
    }
}
