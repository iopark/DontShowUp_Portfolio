using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //[SerializeField] TrailRenderer bulletTrail;
    //[SerializeField] float bulletSpeed; 
    // Generally speaking, this should be done on the target being hit by the target . 

    private Camera camera;
    [SerializeField] LayerMask targetMask; 
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] Bullet bullet; 
    [SerializeField] float maxDistance;
    [SerializeField] float fireRate; // defined by the time, (s) 
    [SerializeField] int damage;

    private void Start()
    {
        camera = Camera.main;
        damage = 1;
        maxDistance = 100;
    }

    Vector3 hitDir; 
    public void Fire()
    {
        muzzleEffect.Play(); // 총구불빛 
        
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, targetMask))
        {
            hitDir = (hit.point - transform.position).normalized; 
            //이펙트에 대해서 오브젝트 풀링으로 구현 
            bullet = GameManager.Resource.Instantiate<Bullet>(bullet, transform.position, Quaternion.LookRotation(hitDir), true);
            bullet.BulletHit(hit); 
        }
        else
        {
            Ray ray = new Ray(transform.position, transform.forward);
            ray.GetPoint(maxDistance); 
            bullet = GameManager.Resource.Instantiate<Bullet>(bullet, transform.position, Quaternion.LookRotation(transform.forward), true);
            bullet.BulletMiss(ray.GetPoint(maxDistance)); 
        }
    }

    IEnumerator ReleaseRoutine(GameObject effect)
    {
        yield return new WaitForSeconds(3f); 
        GameManager.Resource.Destroy(effect); // Instead of Destroy, simply return it to the ObjectPool within the Dict 
    }

    //IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    //{
    //    ////TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
    //    //TrailRenderer trail = GameManager.Resource.Instantiate(bulletTrail, startPoint, Quaternion.identity, true); 
    //    //trail.Clear(); 

    //    //float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

    //    //float rate = 0; 
    //    //while (rate <1)
    //    //{
    //    //    trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate); // 시작점부터 끝점까지 시간을 단위로 이동하는데, 
    //    //    rate += Time.deltaTime / totalTime; // 해당 시간은 매 반복마다 deltatime/ total time 으로 증감시킨다. 

    //    //    yield return null; 
    //    //}
    //    ////Destroy(trail.gameObject, 3f);
    //    //GameManager.Resource.Destroy(trail.gameObject);

    //    //yield return null;

    //    //if (!trail.IsValid())
    //    //{
    //    //    Debug.Log("트레일이 없다"); 
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("트레일이 있다"); 
    //    //}
    //}
}