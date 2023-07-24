using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public GameObject player;

    public override void SetPlayerPos()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position; 
    }
    protected override IEnumerator LoadingRoutine()
    {
        //TODO: ADD new Data for game object Data like diamonds, kills, Target Diamonds, 
        // 
        GameManager.Instance.InitInGameManagers();
        GameManager.Instance.GameSetup?.Invoke(); 
        GameManager.Instance.GameSetUpUI?.Invoke();
        GameManager.CombatManager.SetPlayerLoc();
        
        progress = 0; 
        yield return new WaitForSecondsRealtime(0.5f);
        progress = 0.3f;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f;
        InitilaizeGeneralSetting();
        SetPlayerPos();
        yield return new WaitForSecondsRealtime(1f);
        progress = 1f;
        StartCoroutine(SetPlayerLoc()); 
    }

    public void InitilaizeGeneralSetting()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    IEnumerator SetPlayerLoc()
    {
        SetPlayerPos();
        yield return new WaitForSecondsRealtime(1f); 
        SetPlayerPos();
        yield return null;
        SetPlayerPos(); 
    }
}
