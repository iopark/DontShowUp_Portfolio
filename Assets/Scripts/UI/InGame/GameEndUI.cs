using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : PopUpUI
{
    [SerializeField] Sound victorySound; 
    protected override void Awake()
    {
        base.Awake();
        GameManager.DataManager.StageEnd += StageEnd; 
        GameManager.DataManager.GameEnd += GameEnd;
    }
    public void StageEnd(bool cleared)
    {
        if (cleared)
        {
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/StageCleared");
        }
        else if (!cleared)
        {
            Cursor.lockState = CursorLockMode.Confined;
            //Popup Game Failed 
            GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/GameLost"); 
        }
    }

    public void GameEnd()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/GameWon");
    }
}
