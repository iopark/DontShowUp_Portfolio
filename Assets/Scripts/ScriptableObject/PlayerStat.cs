using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat_", menuName = "Registry/PlayerInformation")]
public class PlayerStat : ScriptableObject
{
    public int health;
    public float normalSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpSpeed; 
    public float mouseSensitivity;
    public int meleeDamage;
    public int meleeFlankDamage; 
}
