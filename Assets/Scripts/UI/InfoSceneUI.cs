using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoSceneUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake(); //BaseUI 에서 정의된 Awake를 실행한다. Where( InfoSceneUI -> SceneUI -> BaseUI) 
        texts["HeartText"].text = "20"; 
    }

}
