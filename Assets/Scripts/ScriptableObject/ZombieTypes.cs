using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Registry/ZombieList")]
public class ZombieTypes: ScriptableObject
{
    public Enemy[] zombieList;
}
