using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTaskUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.GameSetUpUI += Initialize; 
    }
    private void Initialize()
    {
        GameManager.DataManager.Harvested += UpdateHarvest; 
        texts["TaskStatus_Current"].text = GameManager.DataManager.Diamond.ToString();
        texts["TaskStatus_Target"].text = GameManager.DataManager.TargetDiamonds.ToString();
    }
    public void UpdateHarvest(int diamond)
    {
        texts["TaskStatus_Current"].text = diamond.ToString();
    }
}