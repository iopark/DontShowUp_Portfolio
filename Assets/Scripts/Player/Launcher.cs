using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.EditorUtilities;
using System;

[RequireComponent(typeof(SoundMaker))]
public class Launcher : MonoBehaviour, IEquatable<Launcher>
{
    [Header("Holder Dependent Attribute")]
    protected PlayerAttacker player;
    //====================================================================================
    [Header("Launcher attributes")]
    protected SoundMaker soundMaker;
    [SerializeField] public RangedWeapon weaponInfo;
    public string weaponName; 
    [SerializeField] protected Camera camera;
    [SerializeField] protected LayerMask targetMask; 
    [SerializeField] protected Projectile projectile;
    //====================================================================================
    [SerializeField] protected WaitForSeconds reloadInterval; 
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float fireRate; 
    [SerializeField] protected int damage;
    [SerializeField] protected Transform muzzlePoint; 
    //===========================WeaponRounds=============================================
    [SerializeField] protected int currentRounds;
    public int CurrentRounds
    {
        get { return currentRounds; } 
        set
        {
            currentRounds = value;
            if (currentRounds == 0)
                GameManager.CombatManager.CombatAlert?.Invoke("Out of Ammo"); 
            GameManager.CombatManager.WeaponFire.Invoke(currentRounds);
        }
    }

    [SerializeField] protected int maxRounds; 
    [Header("Calculation Cacheing")]
    [SerializeField] protected float nextFire;

    [Header("Launcher Mechanism")]
    protected bool isReloading; 
    public bool IsReloading { get { return isReloading; } }


    protected virtual void Awake()
    {
        soundMaker = GetComponent<SoundMaker>();
    }
    protected virtual void Start()
    {
        this.weaponName = weaponInfo.weaponName; 
        this.maxRounds = weaponInfo.roundLimit;
        this.currentRounds = maxRounds; 
        this.isReloading = false; 
        this.player = GameObject.Find("Player").GetComponent<PlayerAttacker>();
        this.nextFire = 0;
        this.fireRate = weaponInfo.attackRate;
        this.reloadInterval = new WaitForSeconds(weaponInfo.reloadRate); 
        this.maxDistance = weaponInfo.weaponRange;
        camera = Camera.main;
    }

    protected Vector3 hitDir;
    protected RaycastHit hit;
    protected Coroutine reload;
    protected Coroutine fire; 
    public virtual void Fire()
    {

        if (nextFire != 0)
        {
            GameManager.CombatManager.CombatAlert?.Invoke("Not yet"); 
            return;
        }
        else if (currentRounds <= 0)
        {
            GameManager.CombatManager.CombatAlert?.Invoke("Reload!");
        }

        CurrentRounds--;
        fire = StartCoroutine(FireRoutine()); 
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            hitDir = (hit.point - transform.position).normalized;
            //이펙트에 대해서 오브젝트 풀링으로 구현 
            projectile = GameManager.Resource.Instantiate<Projectile>(projectile, transform.position, Quaternion.LookRotation(hitDir), true);
            projectile.TrajectoryHit(hit); 
        }
        else
        {
            Ray ray = new Ray(transform.position, transform.forward);
            ray.GetPoint(maxDistance);
            projectile = GameManager.Resource.Instantiate<Projectile>(projectile, transform.position, Quaternion.LookRotation(transform.forward), true);
            projectile.TrajectoryMiss(ray.GetPoint(maxDistance)); 
        }
    }

    public virtual void Reload()
    {
        if (isReloading) return;
        reload = StartCoroutine(ReloadRoutine());
    }
    protected IEnumerator ReloadRoutine()
    {
        player.anim.SetTrigger("Reload");
        isReloading = true;
        yield return reloadInterval;
        CurrentRounds = maxRounds;  
        isReloading = false;
        //aimRig.weight = 1f;
        reload = null; 
    }
    protected IEnumerator FireRoutine()
    {
        while (nextFire < fireRate)
        {
            nextFire += Time.deltaTime;
            yield return null;
        }
        nextFire = 0;
        fire = null; 
    }

    public bool Equals(Launcher other)
    {
        return this.weaponInfo.weaponName == other.weaponInfo.weaponName; 
    }

    public override int GetHashCode()
    {
        int hash = weaponName != null ? weaponName.GetHashCode() : 0;
        return hash;
    }
}