using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndUI : PopUpUI
{
    string cleared = "Stage Cleared";
    string failed = "Stage Failed";
    string gameWon = "You Won!"; 

    TMP_Text StageResult;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameManager.DataManager.GameEnd += GameEnd;
        GameManager.DataManager.StageEnd += StageEnd;
        StageResult = texts["Content_GameResult"]; 

        transforms["GameFinishedPopUpUI_Content"].gameObject.SetActive(false);
        transforms["GameFinishedPopUpUI_Buttons"].gameObject.SetActive(false);
        buttons["Buttons_ExitToMain"].onClick.AddListener(() => GameManager.SceneManager.LoadScene("TitleScene")); 
    }
    public void StageEnd(int stage, bool cleared)
    {
        if (cleared)
        {
            //Buttons to clear out
        }
        else
        {
            
        }
    }

    public void GameEnd()
    {
        StageResult.text = gameWon;
    }
}
