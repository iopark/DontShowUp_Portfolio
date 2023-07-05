using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    //Player Combat: Materials 
    public UnityAction WeaponSwitch;
    public UnityAction<bool> MeleeStrikeAtest; 

    public Launcher currentWeapon; 
    public WeaponList weaponList;
    public Transform weaponHolder;
    public Queue<Launcher> weapons; 

    //Player Combat: Conditionals
    Vector3 enemyLookDir;
    Vector3 playerLookDir;

    Launcher tempContainer;
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>("Data/Weapon/Player_WeaponList");
        for (int i = 0; i < weaponList.RangedLists.Length; i++)
        {
            tempContainer = GameManager.Resource.Instantiate(weaponList.RangedLists[i].weapon.launcher, true); 
            weapons.Enqueue(tempContainer); 
        }
        WeaponSwitch += NewWeapon;
        MeleeStrikeAtest += FlankJudgement; 
    }

    public void SetWeapon()
    {
        if (weapons.Count <= 0)
            weapons.Enqueue(GameManager.Resource.Instantiate(weaponList.RangedLists[0].weapon.launcher, true));
        tempContainer = weapons.Dequeue();
        currentWeapon = GameManager.Resource.Instantiate(tempContainer, weaponHolder.position, Quaternion.identity, weaponHolder, true); 
        weapons.Enqueue(tempContainer);
    }


    private void NewWeapon()
    {
        tempContainer = weapons.Dequeue();
        currentWeapon = GameManager.Resource.Instantiate(tempContainer, weaponHolder.position, Quaternion.identity, weaponHolder, true);
        weapons.Enqueue(tempContainer);
    }
    public void FlankJudgement(bool testament)
    {

    }

    public bool IsHeadShot()
    {
        return true;
    }
    //Simply regards player's combat status. 
    //Flank Attack judgements, 
    //Account for any headshots.
    //Account for any playerget hit; 
}
