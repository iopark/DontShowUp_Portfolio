using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoSceneUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake(); //BaseUI ���� ���ǵ� Awake�� �����Ѵ�. Where( InfoSceneUI -> SceneUI -> BaseUI) 
        texts["HeartText"].text = "20"; 
    }

}
