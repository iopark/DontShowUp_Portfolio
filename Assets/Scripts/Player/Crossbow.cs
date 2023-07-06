using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crossbow : Launcher
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] Transform arrowSlot;
    public UnityAction crossbowReload;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
    }

    Vector3 rayOrigin; 
    public override void Fire()
    {
        if (nextFire != 0)
        {
            return;
        }
        else if (currentRounds <= 0)
        {
            GameManager.CombatManager.CombatAlert?.Invoke("Reload!");
            return;
        }
        rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        fire = StartCoroutine(FireRoutine());

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            hitDir = (hit.point - transform.position).normalized;
            //projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, Quaternion.identity, true);
            projectile.TrajectoryHit(hit);
        }
        else
        {
            Ray ray = new Ray(rayOrigin, camera.transform.forward);
            ray.GetPoint(maxDistance);
            //projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, arrowSlot.rotation, true);
            projectile.TrajectoryMiss(ray.GetPoint(maxDistance));
        }
    }

    public override void Reload()
    {
        if (isReloading)
        {
            return;
        }
        crossbowReload?.Invoke(); 
        reload = StartCoroutine(BowReload()); 
    }
    IEnumerator BowReload()
    {
        isReloading = true;
        yield return reloadInterval;
        projectile = GameManager.Resource.Instantiate<Projectile>(projectile, arrowSlot.position, arrowSlot.rotation, arrowSlot.transform, true);
        isReloading = false;
    }
}