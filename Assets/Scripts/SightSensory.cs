using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensory : MonoBehaviour
{
    float speed = 0.5f;
    [SerializeField] bool debug; 
    [SerializeField] float range;
    [SerializeField, Range(0, 360f)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;
    private Vector3 lookDir; 

    [SerializeField] private CharacterController body;
    public Animator anim; 
    private Vector3 traceTarget;

    //GetSet 
    public LayerMask TargetMask { get { return targetMask; } set { targetMask = value; } }
    public LayerMask ObstacleMask { get { return obstacleMask; } set { obstacleMask = value; } }
    public float Range { get { return range; } 
    set { range = value; } }

    public float Angle { get { return angle; }
        set { angle = value; } }

    private void Start()
    {
        body = GetComponentInParent<CharacterController>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        //FindTarget(); 
    }
    //TODO: Target must be continuing to search for the target, how can I implement this together with the FindTarget 

    public Vector3 FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            //2. 플레이어 기준 앞에 있는지에 대해서 확인작업 필요 
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            //3. 플레이어 지정 각도와 비교 
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            //4. 중간에 장애물이 없는지 
            float distToTarget = Vector3.Distance(transform.position, collider.gameObject.transform.position); 
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask)) 
                continue;

            if (collider.gameObject.tag == "Player")
            {
                //Activate Certain State based on this behaviour. 
                traceTarget = collider.transform.position;
                return traceTarget; 
            }
            //while (distToTarget <= range)
            //    Trace(traceTarget.position);

            //traceTarget = null; 
            //anim.SetBool("Walk Forward", false); 
            //Debug.DrawRay(transform.position, dirTarget * distToTarget, Color.green); 
        }
        return Vector3.zero; 
    }

    private void Trace(Vector3 target)
    {
        //This can transfer into TraceState 
        lookDir = (target - body.transform.position).normalized;
        lookDir.y = body.transform.position.y;
        body.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up); 
        body.Move(lookDir * speed *Time.deltaTime);
        anim.SetBool("Walk Forward", true); 
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        // where .eulerAngle.y returns rotation angle from the y-axis in a Space.World 
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        // 
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rightDir * range);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftDir * range);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
        //player 기준으로 생성하기에, where player front is z axis, 
    }

    //TODO: Shoot raycast at the 
}
