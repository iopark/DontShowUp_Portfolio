using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundMaker))] 
public class Projectile : MonoBehaviour, IPausable
{
    [Header("Launcher Independent Attributes")]
    Camera cam; 
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] LayerMask explodable; 

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
        //currentMoveSpeed = 0f; 
        damage = 51; 
    }


    private void OnEnable()
    {
        cam = Camera.main;
        //currentMoveSpeed = projectileMoveSpeed;
    }
    Coroutine trajectoryMovement; 
    public void TrajectoryMiss(Vector3 endPoint)
    {
        currentMoveSpeed = projectileMoveSpeed;
        //Gun should enter with the bullet end point. 
        transform.LookAt(endPoint);
        trajectoryMovement = StartCoroutine(TrajectoryMissRoutine(endPoint));
    }
    public void TrajectoryHit(in RaycastHit hit)
    {
        currentMoveSpeed = projectileMoveSpeed;
        hitLoc = hit;
        targetLoc = hit.collider.transform;
        transform.LookAt(targetLoc);
        trajectoryMovement = StartCoroutine(TrajectoryHitRoutine()); 
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
        targetLoc = null;
        hitLoc = default; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        ContactPoint contactPoint = collision.GetContact(0);
        RandomExplosion(contactPoint, collision.gameObject.transform);
    }
    private void RandomExplosion(ContactPoint target, Transform hitTransform)
    {
        Vector3 contactPoint = target.point;
        Vector3 contactNormal = target.normal;
        soundMaker.TriggerSound(soundIntensity);
        //explosion = StartCoroutine(RandomParticleRoutine(contactPoint, contactNormal)); 
        ParticleSystem splash = GameManager.Resource.Instantiate(hitEffect, contactPoint, Quaternion.LookRotation(contactNormal), hitTransform, true);
    }

    private void Explode()
    {
        soundMaker.TriggerSound(soundIntensity);
        ParticleSystem splash = GameManager.Resource.Instantiate(hitEffect, hitLoc.point, Quaternion.LookRotation(hitLoc.normal), hitLoc.transform, true);
    }
    Coroutine explosion;
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
            endPoint = hitLoc.point;
            delta = endPoint - transform.position;
            distance = Vector3.SqrMagnitude(delta);
            Debug.Log(distance); 
            transform.position = Vector3.MoveTowards(transform.position, endPoint, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
        if (targetLoc.IsValid())
        {
            Explode(); 
            IHittable hittable = hitLoc.collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage); 
        }
        GameManager.Resource.Destroy(this.gameObject);
    }
    Vector3 rayOrigin; 
    private IEnumerator TrajectoryMissRoutine(Vector3 endPoint)
    {
        Vector3 delta = endPoint - transform.position;
        distance = Vector3.Dot(delta, delta); 
        while (distance > trajectoryOffset)
        {
            delta = endPoint - transform.position;
            distance = Vector3.Dot(delta, delta);
            Debug.Log(distance); 
            transform.position = Vector3.MoveTowards(transform.position, endPoint, currentMoveSpeed * Time.deltaTime);
            yield return null; 
        }
        GameManager.Resource.Destroy(this.gameObject);
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
}