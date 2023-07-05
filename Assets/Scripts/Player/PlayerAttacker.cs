using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour, IHittable
{
# region Attacker Essential Properties. 
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
    public Rig rig; 
    private int meleeDamage; 
    public int MeleeDamage
    {
        get { return meleeDamage; }
        set { meleeDamage = value; }
    }
    private int flankDamage; 
#endregion 
    private void Awake()
    {
        rig = GetComponentInChildren<Rig>();
        GameManager.CombatManager.weaponHolder = weaponHolder;
    }

    private void Start()
    {
        currentWeapon = weaponHolder.GetComponentInChildren<Launcher>();
        meleeDamage = GameManager.DataManager.MeleeDamage;
        flankDamage = GameManager.DataManager.MeleeFlank;
        SetWeapon(); 

        isReloading = false; 
        anim = GetComponent<Animator>();
    }
    public void SetWeapon()
    {
        currentWeapon = weaponHolder.GetComponentInChildren<Launcher>();
    }
    private void OnFire(InputValue input)
    {
        if (currentWeapon.IsReloading)
            return;
        currentWeapon.Fire();
    }

    private void OnSwitch(InputValue input)
    {
        if (input.isPressed)
            GameManager.CombatManager.WeaponSwitch?.Invoke(); 
        return;
    }

    public void TryFlank(Transform target, bool contestSuccess)
    {
        if (contestSuccess)
        {
            IHittable hittable = target.GetComponent<IHittable>();
            hittable?.TakeHit(flankDamage); 
        }
        else
        {
            IHittable hittable = target.GetComponent<IHittable>();
            hittable?.TakeHit(meleeDamage);
        }
    }

    Transform tempTarget; 
    private void OnMelee(InputValue input)
    {
        //TODO: Melee attempt 
        rig.weight = 0; 
        anim.SetTrigger("Melee");
        RaycastHit targetInfo;
        if (Physics.Raycast(transform.position, transform.forward, out targetInfo, 1.5f, LayerMask.GetMask("Enemy")))
        {
            tempTarget = targetInfo.collider.transform;
            GameManager.CombatManager.FlankJudgement(tempTarget, TryFlank); 
        }
        rig.weight = 1; 
    }
    private void OnReload(InputValue input)
    {
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
