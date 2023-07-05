using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Launcher
{
    [SerializeField] ParticleSystem muzzleEffect;

    protected override void Awake()
    {
        base.Awake(); 
    }

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        GameManager.Pool.Release(this.gameObject);
    }
    Vector3 rayOrigin;
    //DEBUGGING
    LineRenderer lineRenderer; 
    public override void Fire()
    {
        lineRenderer.SetPosition(0, muzzlePoint.position); 
        if (nextFire != 0)
        {
            GameManager.CombatManager.CombatAlert?.Invoke("Not yet");
            return;
        }
        else if (currentRounds <= 0)
        {
            GameManager.CombatManager.CombatAlert?.Invoke("Reload!");
            return;
        }
        rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        CurrentRounds--;
        soundMaker.TriggerSound(weaponInfo.noiseIntensity);
        fire = StartCoroutine(FireRoutine());
        muzzleEffect.Play(); // 총구불빛 
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            lineRenderer.SetPosition(1, hit.point);
            hitDir = (hit.point - transform.position).normalized;
            Debug.DrawRay(transform.position, hitDir, Color.blue);
            //이펙트에 대해서 오브젝트 풀링으로 구현 
            GameManager.Resource.Instantiate<Projectile>(projectile, muzzlePoint.transform.position, Quaternion.LookRotation(hitDir)).TrajectoryHit(hit);
            //projectile.TrajectoryHit(hit);
        }
        else
        {
            Ray ray = new Ray(rayOrigin, camera.transform.forward);
            ray.GetPoint(maxDistance);
            lineRenderer.SetPosition(1, rayOrigin + (camera.transform.forward * maxDistance));
            GameManager.Pool.Get<Projectile>(projectile, muzzlePoint.transform.position, Quaternion.LookRotation(transform.forward)).TrajectoryMiss(ray.GetPoint(maxDistance));
        }
    }
}
