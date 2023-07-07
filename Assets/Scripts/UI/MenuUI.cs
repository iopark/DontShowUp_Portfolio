using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MenuUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["Menu_Start"].onClick.AddListener(() => { StartGame(); });
        buttons["Menu_Configure"].onClick.AddListener(() => { ConfigureGame(); });
        buttons["Menu_End"].onClick.AddListener(() => { EndGame(); });
    }

    private void StartGame()
    {
        GameManager.SceneManager.LoadScene("Level1"); 
    }
    public void ConfigureGame()
    {
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/ConfigurePopUpUI"); 
    }

    public void EndGame()
    {
        Application.Quit(); 
    }
}
