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
    RectTransform StageResult_Rect;
    RectTransform buttonCollection; 
    Button untoNext_Button;
    Button untoMain_Button; 
    protected override void Awake()
    {
        base.Awake();
        GameManager.DataManager.StageEnd += StageEnd; 
        GameManager.DataManager.GameEnd += GameEnd;
    }

    private void Start()
    {
        GameManager.DataManager.GameEnd += GameEnd;
        GameManager.DataManager.StageEnd += StageEnd;
        StageResult = texts["Content_GameResult"];

        StageResult_Rect = transforms["GameFinishedPopUpUI_Content"];
        StageResult_Rect.gameObject.SetActive(false);
        buttonCollection = transforms["GameFinishedPopUpUI_Buttons"];

        //Button Initialization. 
        untoNext_Button = buttons["Buttons_Continue"];
        buttons["Buttons_Continue"].onClick.AddListener(UntoNext);
        untoMain_Button = buttons["Buttons_ExitToMain"];
        buttons["Buttons_ExitToMain"].onClick.AddListener(() => GameManager.SceneManager.LoadScene("TitleScene"));
        buttonCollection.gameObject.SetActive(false);
    }
    public void StageEnd(int stage, bool cleared)
    {
        if (cleared)
        {
            StageResult.text = $"{cleared}: Stage {stage}"; 
        }
        else
        {
            StageResult.text = failed;
            untoNext_Button.gameObject.SetActive(false);
        }
    }

    public void UntoNext()
    {
        GameManager.DataManager.InitializeStageData();
        GameManager.UIManager.ClosePopUpUI(); 
    }

    public void GameEnd()
    {
        StageResult.text = $"{cleared}: {gameWon}";
    }
}
