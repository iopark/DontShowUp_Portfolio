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

    Camera camera; 
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
    [SerializeField]private bool isStriking; 
#endregion 
    private void Awake()
    {
        camera = Camera.main; 
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
        if (isStriking)
            return;
        transform.rotation = Quaternion.LookRotation(camera.transform.forward, transform.up); 
        //TODO: Melee attempt 
        rig.weight = 0; 
        anim.SetTrigger("Melee");
        isStriking = true;
        gambleStrike = StartCoroutine(GambleStrike()); 
    }
    RaycastHit targetInfo;
    private void TryStrike()
    {
        if (Physics.Raycast(transform.position, transform.forward, out targetInfo, 1.5f, LayerMask.GetMask("Enemy")))
        {
            tempTarget = targetInfo.collider.transform;
            GameManager.CombatManager.FlankJudgement(tempTarget, TryFlank);
        }
    }
    Coroutine gambleStrike; 
    IEnumerator GambleStrike()
    {
        rig.weight = 0;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                TryStrike(); 
            }
            yield return null;
        }
        isStriking = false; 
        rig.weight = 1;
        gambleStrike = null; 
    }

    private void OnReload(InputValue input)
    {
        currentWeapon.Reload();
    }

    public void TakeHit(int damage)
    {
        Debug.Log("Player Hit"); 
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
