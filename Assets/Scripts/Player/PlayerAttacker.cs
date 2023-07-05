using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour, IHittable
{
    //[SerializeField] float attackSoundIntensity;
    [SerializeField] Launcher currentWeapon;
    [SerializeField] Transform weaponHolder; 

    [SerializeField]
    Launcher primary;
    [SerializeField]
    Launcher secondary; 

    [SerializeField] bool isReloading;
    [SerializeField] float reloadTime;

    public Animator anim;

    #region Temporary variables for the debugging purposes 
    private int health = 100; 
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }
    #endregion
    private void Awake()
    {
        GameManager.CombatManager.weaponHolder = weaponHolder;
        primary = weaponHolder.GetComponentInChildren<Launcher>(); //GameManager.Resource.Load<RangedWeapon>("Data/Weapon/Ranged_Shotgun");
        //secondary = GameManager.Resource.Instantiate<RangedWeapon>("Data/Weapon/Ranged_Crossbow", transform); 
        currentWeapon = primary;
        //GameManager.Resource.Instantiate<Launcher>(primary.launcher, weaponHolder.position, Quaternion.identity, weaponHolder, true); 
    }

    private void Start()
    {

        isReloading = false; 
        anim = GetComponent<Animator>();
    }

    private void OnFire(InputValue input)
    {
        if (currentWeapon.IsReloading)
            return;
        currentWeapon.Fire();
    }

    private void OnSwitch(InputValue input)
    {
        GameManager.CombatManager.WeaponSwitch?.Invoke(); 
        if (currentWeapon.name == primary.name)
        {
            currentWeapon = secondary;
            //TODO: Enable, Disable Weapon 
            return;
        }
        currentWeapon = primary;
        return;
    }

    private void OnReload(InputValue input)
    {
        //if (currentWeapon.type != WeaponType.Ranged)
        //    return;
        currentWeapon.Reload();
    }

    public void TakeHit(int damage)
    {
        GameManager.DataManager.Health -= damage;  
        AfterStrike(); 
    }

    public void AfterStrike()
    {
        anim.SetTrigger("TakeHit");
        //TODO: Sound Play for the player hit. 
        //Invoke after hit UI 
    }
}
