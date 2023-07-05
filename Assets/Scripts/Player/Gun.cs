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
    }

    private void OnDisable()
    {
        GameManager.Pool.Release(this.gameObject);
    }
    Vector3 rayOrigin; 
    public override void Fire()
    {
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
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            hitDir = (hit.point - transform.position).normalized;
            Debug.DrawRay(transform.position, hitDir, Color.blue);
            //이펙트에 대해서 오브젝트 풀링으로 구현 
            projectile = GameManager.Resource.Instantiate<Projectile>(projectile, muzzlePoint.transform.position, Quaternion.LookRotation(hitDir), true);
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
}
