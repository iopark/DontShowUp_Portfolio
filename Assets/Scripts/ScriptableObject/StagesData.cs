using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Stages", menuName = "Registry/Game")]
public class StagesData : ScriptableObject
{
    [SerializeField]
    StageList[] stageLists; 

    public StageList[] StageLists { get => stageLists; } 
    [Serializable]
    public class StageList
    {
        public SingleStage stage;
        public string StageName
        {
            get => stage.stageName; 
        }
    }
}
