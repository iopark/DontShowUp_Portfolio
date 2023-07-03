using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPausable
{
    [SerializeField] float bulletMoveSpeed;
    int damage;
    Vector3 targetLoc; 
    Collider collider; 

    public void Pause()
    {
        throw new System.NotImplementedException();
    }
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        damage = GameManager.DataManager.Damage; 

    }
    private void FixedUpdate()
    {

    }

    private void Launch()
    {

    }

    private void OnDisable()
    {
        targetLoc = Vector3.zero;
        damage = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Damage the opponent. if its a wall, simply explode. 
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(damage); 
        //TODO: Add particle effect for after hit. 
        GameManager.Resource.Destroy(collider);
    }
}
