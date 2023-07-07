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
    WaitForSeconds strikeInterval = new WaitForSeconds(0.5f); 
#endregion 
    private void Awake()
    {
        camera = Camera.main; 
        rig = GetComponentInChildren<Rig>();
        GameManager.CombatManager.weaponHolder = weaponHolder;
        Cursor.lockState = CursorLockMode.Locked;
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
    public void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Confined; 
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
        //transform.rotation = Quaternion.LookRotation(camera.transform.forward, transform.up); 
        //TODO: Melee attempt 
        gambleStrike = StartCoroutine(GambleStrike()); 
    }
    RaycastHit targetInfo;
    Coroutine gambleStrike;
    IEnumerator GambleStrike()
    {
        anim.SetTrigger("Melee");
        isStriking = true;
        //재장전 시작시 weight 재설정 
        rig.weight = 0f;
        yield return strikeInterval;
        isStriking = false;
        rig.weight = 1;
        gambleStrike = null; 
    }
    Vector3 centrePoint; 
    private void P_TryStrike()
    {
        centrePoint = camera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0)); 
        if (Physics.SphereCast(centrePoint, transform.lossyScale.x/2, camera.transform.forward, out targetInfo, 5f, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("Player Strikes!"); 
            Debug.DrawRay(transform.position, transform.forward, Color.green); 
            tempTarget = targetInfo.collider.transform;
            GameManager.CombatManager.FlankJudgement(tempTarget, TryFlank);
        }
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
