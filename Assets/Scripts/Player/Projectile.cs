using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundMaker))] 
public class Projectile : MonoBehaviour, IPausable
{
    [Header("Launcher Independent Attributes")]
    Camera cam; 
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] WaitForSeconds particleRes = new WaitForSeconds(1f);

    float currentMoveSpeed; 
    [SerializeField] float projectileMoveSpeed;
    [SerializeField] float pauseMovespeed = 0.1f; 
    SoundMaker soundMaker;

    [Header("Launcher Dependent Attributes")]
    [SerializeField] RangedWeapon launcher;
    [SerializeField] int damage;
    [SerializeField] float soundIntensity;
    [SerializeField] float maxDistance;

    [Header("Calculation Cacheing")]
    [SerializeField] Transform targetLoc; 
    RaycastHit hitLoc;
    float distance;
    float trajectoryOffset = 0.5f; 
    
    private void Awake()
    {
        damage = launcher.damage;
        soundIntensity = launcher.noiseIntensity;
        maxDistance = launcher.weaponRange; 
        soundMaker = GetComponent<SoundMaker>();
    }
    private void Start()
    {
        soundIntensity = 30f;
        currentMoveSpeed = projectileMoveSpeed; 
        damage = 51; 
        transform.LookAt(targetLoc);
    }
    public void TrajectoryMiss(Vector3 endPoint)
    {
        currentMoveSpeed = projectileMoveSpeed;
        //Gun should enter with the bullet end point. 
        transform.LookAt(endPoint);
        StartCoroutine(TrajectoryMissRoutine());
    }
    public void TrajectoryHit(in RaycastHit hit)
    {
        currentMoveSpeed = projectileMoveSpeed;
        hitLoc = hit;
        targetLoc = hit.collider.transform;
        StartCoroutine(TrajectoryHitRoutine()); 
    }
    private void OnEnable()
    {
        cam = Camera.main;
    }
    private void OnDisable()
    {
        targetLoc = null; 
    }

    private void Explode()
    {
        soundMaker.TriggerSound(soundIntensity);
        StartCoroutine(ParticleRoutine()); 
    }

    IEnumerator ParticleRoutine()
    {
        hitEffect = GameManager.Resource.Instantiate(hitEffect, hitLoc.point, Quaternion.LookRotation(hitLoc.normal), true);
        soundMaker.TriggerSound(soundIntensity); 
        //TODO: hit against the wall 
        yield return particleRes; 
        GameManager.Resource.Destroy(hitEffect.gameObject); // Instead of Destroy, simply return it to the ObjectPool within the Dict 
    }

    IEnumerator TrajectoryHitRoutine()
    {
        Vector3 delta;
        delta = hitLoc.point - transform.position; 
        Vector3 endPoint = hitLoc.point; 
        distance = Vector3.Dot(delta, delta); 
        while (distance > trajectoryOffset)
        {
            if (!targetLoc.IsValid())
            {
                //transform.LookAt(hitLoc.point);
                endPoint = transform.forward * maxDistance;// Vector3.Dot(transform.forward * maxDistance, transform.forward);
            }
            delta = endPoint - transform.position;
            distance = Vector3.Dot(delta, delta);

            transform.position = Vector3.MoveTowards(transform.position, endPoint, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
        if (targetLoc.IsValid())
        {
            Explode(); 
            IHittable hittable = hitLoc.collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage); 
        }
        GameManager.Pool.Release(gameObject);
    }
    IEnumerator TrajectoryMissRoutine()
    {
        Vector3 endPoint = cam.transform.forward * maxDistance;
        Vector3 delta = endPoint - transform.position;
        distance = Vector3.Dot(delta, delta); 
        while (distance > trajectoryOffset)
        {
            delta = endPoint - transform.position;
            distance = Vector3.Dot(delta, delta);
            transform.position = Vector3.MoveTowards(transform.position, endPoint, currentMoveSpeed * Time.deltaTime);
            yield return null; 
        }
        GameManager.Pool.Release(gameObject);
    }
    Coroutine freezer; 
    float timer; 
    public void Pause(float time)
    {
        currentMoveSpeed = pauseMovespeed; 
        freezer = StartCoroutine(Freeze(time)); 
    }
    // Either this is called after the coroutine freeze, or during in which pause activity is finished; 
    // How do you get all the subject with IPausable Interface?
    public void Resume()
    {
        StopCoroutine(freezer); 
        timer = 0f; 
        currentMoveSpeed = projectileMoveSpeed; 
    }
    IEnumerator Freeze(float time) 
    { 
        time = 0f; 
        while (timer < time) 
        { 
            timer += Time.deltaTime; 
            yield return null; 
        }
        Resume(); 
    }

    IEnumerator ReleaseRoutine(GameObject effect)
    {
        yield return particleRes;
        GameManager.Resource.Destroy(effect);
    }
}