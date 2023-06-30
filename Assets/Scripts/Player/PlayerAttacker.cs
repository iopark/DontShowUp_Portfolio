using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour, IHittable
{
    [SerializeField] float attackSoundIntensity;
    [SerializeField] bool isReloading;
    [SerializeField] float reloadTime;

    WaitForSeconds reloadInterval;
    Coroutine reloading; 
    Animator anim;
    SoundMaker soundMaker;

    [Header("Pertaining to Gun Fire")]
    float fireRate = 1f; // should be updated based on the pre-written scriptable object on player stats. 
    float nextFire; 


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

    public bool debug; 

    private void Awake()
    {
        nextFire = 0; 
        soundMaker = GetComponent<SoundMaker>();
    }

    private void Start()
    {
        isReloading = false; 
        reloadInterval = new WaitForSeconds(reloadTime);
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
        if (isReloading || Time.time < nextFire)
            return;
        nextFire = Time.time + fireRate; 
        //TODO: Make Coroutine which triggers gun animation + relative sound + gun striking function. 
        soundMaker.TriggerSound(transform, attackSoundIntensity);
        Debug.Log("Attack");
    }

    private void OnReload(InputValue input)
    {
        if (isReloading)
            return;
        reloading = StartCoroutine(ReloadRoutine());

    }

    IEnumerator ReloadRoutine()
    {

        anim.SetTrigger("Reload");
        isReloading = true;
        //재장전 시작시 weight 재설정 
        //aimRig.weight = 0f; 
        yield return reloadInterval; 
        isReloading = false;
        //aimRig.weight = 1f;
    }


    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackSoundIntensity);
        }
    }

    public void TakeHit(int damage)
    {
        health -= damage;
    }
}
