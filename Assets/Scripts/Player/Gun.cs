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
        GameManager.Resource.Destroy(this);
    }
    public override void Fire()
    {
        if (nextFire != 0)
        {
            //TODO: Invoke Event asking for the reload.
            // UI should show "Must Reload!" 
            return;
        }
        fire = StartCoroutine(FireRoutine());
        muzzleEffect.Play(); // 총구불빛 
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
}
