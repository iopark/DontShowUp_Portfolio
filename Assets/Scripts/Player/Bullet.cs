using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IPausable
{
    [SerializeField] ParticleSystem hitEffect;
    WaitForSeconds particleRes = new WaitForSeconds(1f); 
    [SerializeField] float bulletMoveSpeed;
    SoundMaker soundMaker;

    [Header("Bullet Attributes")]
    [SerializeField] int damage;
    [SerializeField] float soundIntensity;
    [SerializeField] float maxDistance;

    [Header("Calculation Cacheing")]
    Transform targetLoc; 
    RaycastHit hitLoc;
    float distance;
    float bulletOffset = 0.5f; 

    
    private void Awake()
    {
        soundMaker = GetComponent<SoundMaker>();
    }
    void Start()
    {
        soundIntensity = GameManager.DataManager.GunNoiseIntensity; 
        damage = GameManager.DataManager.Damage;
        transform.LookAt(targetLoc);

    }
    public void BulletMiss(Vector3 endPoint)
    {
        //Gun should enter with the bullet end point. 
        transform.LookAt(endPoint);
        StartCoroutine(BulletMissRoutine());
    }

    public void BulletHit(in RaycastHit hit)
    {
        hitLoc = hit;
        targetLoc = hit.collider.transform;
        StartCoroutine(BulletRoutine()); 
        //TODO: Location of hit is predetermined by the raycast hit of the gun. 
    }

    private void OnDisable()
    {
        targetLoc = null; 
        damage = 0;
    }

    private void Explode()
    {
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

    IEnumerator BulletRoutine()
    {
        Vector3 delta;
        delta = hitLoc.point - transform.position; 
        Vector3 endPoint = hitLoc.point; 
        //float time = Vector3.Distance(transform.position, endPoint) / bulletMoveSpeed; // time until it reaches the enemy location 
        distance = Vector3.Dot(delta, delta); 
        while (distance > bulletOffset)
        {
            // �߻� ���Ŀ� �ٽ� ���������� �ֱ� ������ 
            if (!targetLoc.IsValid())
            {
                //transform.LookAt(hitLoc.point);
                endPoint = transform.forward * maxDistance;// Vector3.Dot(transform.forward * maxDistance, transform.forward);
            }
            delta = endPoint - transform.position;
            distance = Vector3.Dot(delta, delta);

            transform.position = Vector3.MoveTowards(transform.position, endPoint, bulletMoveSpeed * Time.deltaTime);
            yield return null;
        }
        //After it reaches, double check if the target being strike is valid 
        //����ó�� �� Ȯ�� �۾� 
        if (targetLoc.IsValid())
        {
            Explode(); 
            IHittable hittable = hitLoc.collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage); 
        }
        GameManager.Pool.Release(gameObject);
    }

    IEnumerator BulletMissRoutine()
    {
        Vector3 endPoint = transform.forward * maxDistance;
        Vector3 delta = endPoint - transform.position;
        distance = Vector3.Dot(delta, delta); 
        while (distance > bulletOffset)
        {
            delta = endPoint - transform.position;
            distance = Vector3.Dot(delta, delta);
            transform.position = Vector3.MoveTowards(transform.position, endPoint, bulletMoveSpeed * Time.deltaTime);
            yield return null; 
        }
        GameManager.Pool.Release(gameObject);
    }

    public void Pause()
    {
        bulletMoveSpeed = 0;
    }
    public void Resume()
    {
        bulletMoveSpeed = GameManager.DataManager.BulletMoveSpeed; 
    }
}
