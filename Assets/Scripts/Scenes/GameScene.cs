using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameScene : BaseScene
{
    //public GameObject player;
    public Transform playerPosition;

    private void Start()
    {
        if (GameManager.CombatManager == null)
        {
            //TODO: GameManager InitializeGameData
        }
    }
    protected override IEnumerator LoadingRoutine()
    {
        //TODO: ADD new Data for game object Data like diamonds, kills, Target Diamonds, 
        // 
        GameManager.Instance.InitInGameManagers();
        GameManager.Instance.GameSetup?.Invoke(); 
        GameManager.Instance.GameSetUpUI?.Invoke();
        progress = 0; 
        yield return new WaitForSecondsRealtime(0.5f);
        progress = 0.3f;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f; 

        yield return new WaitForSecondsRealtime(1f);
        GameManager.CombatManager.SetPlayerLoc(); 
        progress = 1f; 
    }

    public void InitilaizeGeneralSetting()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateInGameData()
    {
        //TODO: Link up InGameUI with each stage
    }
}
