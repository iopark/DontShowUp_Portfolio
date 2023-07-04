using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crossbow : Launcher
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] Transform arrowSlot;
    public UnityAction crossbowReload; 

    public override void Fire()
    {
        if (nextFire != 0)
        {
            return;
        }
        fire = StartCoroutine(FireRoutine());

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            hitDir = (hit.point - transform.position).normalized;
            projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, Quaternion.identity, true);
            projectile.TrajectoryHit(hit);
        }
        else
        {
            Ray ray = new Ray(transform.position, transform.forward);
            ray.GetPoint(maxDistance);
            projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, Quaternion.LookRotation(transform.forward), true);
            projectile.TrajectoryMiss(ray.GetPoint(maxDistance));
        }
    }

    public override void Reload()
    {
        if (isReloading)
        {
            return;
        }
        reload = StartCoroutine(BowReload()); 
    }
    IEnumerator BowReload()
    {
        //TODO: Trigger reload event;
        isReloading = true;
        //After reload, it should instantiate arrow to the arrow slot. 
        yield return reloadInterval;
        projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, Quaternion.identity, true);
        isReloading = false;
    }
}