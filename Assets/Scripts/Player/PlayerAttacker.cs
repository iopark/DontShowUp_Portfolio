using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour, IHittable
{
    //[SerializeField] float attackSoundIntensity;
    [SerializeField] RangedWeapon currentWeapon;
    [SerializeField] Transform weaponHolder; 

    [SerializeField]
    RangedWeapon primary;
    RangedWeapon secondary; 

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
        primary = GameManager.Resource.Load<RangedWeapon>("Data/Weapon/Ranged_Shotgun");
        secondary = GameManager.Resource.Load<RangedWeapon>("Data/Weapon/Ranged_Crossbow"); 
        currentWeapon = primary; 
    }

    private void Start()
    {
        isReloading = false; 
        anim = GetComponent<Animator>();
    }
    private void OnAttack(InputValue value)
    {
        //if (isReloading)
        //    attack = false;
        //else
        //    attack = true;
        //soundMaker.TriggerSound(transform, attackSoundIntensity);
        //Debug.Log("Attack"); 
    }

    private void OnFire(InputValue input)
    {
        if (currentWeapon.launcher.IsReloading)
            return;
        currentWeapon.launcher.Fire();
    }

    private void OnSwitch(InputValue input)
    {
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
        currentWeapon.launcher.Reload();
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
