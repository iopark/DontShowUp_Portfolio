using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundMaker))]
public class Launcher : MonoBehaviour
{
    //[SerializeField] TrailRenderer bulletTrail;
    //[SerializeField] float bulletSpeed; 
    // Generally speaking, this should be done on the target being hit by the target . 
    [Header("Holder Dependent Attribute")]
    PlayerAttacker player; 

    [Header("Launcher attributes")]
    [SerializeField] protected RangedWeapon weapon;
    [SerializeField] protected Camera camera;
    [SerializeField] protected LayerMask targetMask; 
    [SerializeField] protected Projectile projectile;

    [SerializeField] protected WaitForSeconds reloadInterval; 
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float fireRate; // defined by the time, (s) 
    [SerializeField] protected int damage;

    [Header("Calculation Cacheing")]
    [SerializeField] protected float nextFire;


    [Header("Launcher Mechanism")]
    protected bool isReloading; 
    public bool IsReloading { get { return isReloading; } }

    SoundMaker soundMaker;
    private void Awake()
    {
        soundMaker = GetComponent<SoundMaker>();
    }
    private void Start()
    {
        this.nextFire = 0;
        this.fireRate = weapon.attackRate;
        this.reloadInterval = new WaitForSeconds(weapon.reloadRate); 
        this.maxDistance = weapon.weaponRange;
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
            //TODO: Invoke Event asking for the reload.
            // UI should show "Must Reload!" 
            return;
        }
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


    public void Reload()
    {
        if (isReloading) return;
        reload = StartCoroutine(ReloadRoutine());
    }

    protected IEnumerator ReloadRoutine()
    {
        player.anim.SetTrigger("Reload");
        isReloading = true;
        //재장전 시작시 weight 재설정 
        //aimRig.weight = 0f; 
        yield return reloadInterval;
        isReloading = false;
        //aimRig.weight = 1f;
        reload = null; 
    }
    protected IEnumerator FireRoutine()
    {
        if (nextFire > fireRate)
        {
            nextFire = 0;
            yield break;
        }
        nextFire += Time.deltaTime;
        yield return null;
    }
}