using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : PopUpUI
{
    const string cleared = "Stage Cleared";
    const string failed = "Stage Failed";
    const string gameWon = "You Won!";
    TMP_Text StageResult;
    Button intoNext;
    Button intoMenu; 
    protected override void Awake()
    {
        base.Awake();
        GameManager.DataManager.StageEnd += StageEnd; 
    }

    private void Start()
    {
        GameManager.DataManager.GameEnd += GameEnd;
        GameManager.DataManager.StageEnd += StageEnd;
        StageResult = texts["Content_GameResult"];

        transforms["GameFinishedPopUpUI_Content"].gameObject.SetActive(false);
        transforms["GameFinishedPopUpUI_Buttons"].gameObject.SetActive(false);
        intoNext = buttons["Buttons_Continue"];
        buttons["Buttons_Continue"].onClick.AddListener(UntoNext);

        intoMenu = buttons["Buttons_ExitToMain"];
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

    public void UntoNext()
    {
        
    }

    public void GameEnd()
    {
        StageResult.text = gameWon;

    }
}
