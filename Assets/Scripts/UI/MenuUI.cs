using UnityEngine;

public class MenuUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["Menu_Start"].onClick.AddListener(() => { StartGame(); });
        buttons["Menu_Configure"].onClick.AddListener(() => { ConfigureSound(); });
        buttons["Menu_End"].onClick.AddListener(() => { EndGame(); });
    }

    private void StartGame()
    {
        string gameKey = GameManager.DataManager.CurrentGameData.stageName; 
        GameManager.SceneManager.LoadScene(gameKey); 
    }
    public void ConfigureSound()
    {
        GameManager.UIManager.ShowPopUpUI<PopUpUI>("UI/SoundPopUpUI"); 
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit(); 
    }
}
