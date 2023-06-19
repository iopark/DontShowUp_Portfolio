using BeeState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [Header("Bee's Current State")]
    [SerializeField] private StateBase<Bee>[] states;
    [SerializeField] public STATE curState; 

    [Header("Bee's properties")]
    [SerializeField] public float detectRange;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float attackRange;
    [SerializeField] public Transform[] patrolPoints;

    [SerializeField] public Vector3 returnPosition;
    [SerializeField] public Transform player;
    public int patrolIndex = 0; // debuggin purposes, to show which patrolpoint target is searching 

    // ������ ���³��� ĸ��ȭ�� �������� ��Ȳ�� ���ؼ���, ���³��� ���� ���ؼ� ������ �����ϴ�. (e.g. Trace ���� Idle�� ���� �����Ҽ� �ְԵȴ�.) 
    // ����, �� ���¿� ���� ���ο� Ŭ������ ĸ��ȭ�� �ϰ� �ȴٸ�, �� ���¿� ���ԵǴ� �ɹ������鳢���� ������ �Ұ����ϰ� �ȴ�. 
    // ĸ��ȭ ���Ŀ���, �� ���º��� �迭�� ���ϰ� �Ҽ� �ְڴ�. 

    // ĸ��ȭ �Ҷ� ���ǻ��� : �߻�ȭ ��ü�� ���ؼ�, State�� ���ϰ� �ִ� ��ü�� �����Ҽ� �ֵ���, �����ڿ��� ��������� �����Ҽ� �ֵ��� �ϴ°��� �����. 

    private void Awake()
    {
        states = new StateBase<Bee>[(int)STATE.Size]; // Size ���� �̿��ؼ�, �������� ������� �迭���������� �����Ͽ����ڴ�.
        states[(int)STATE.Idle] = new IdleState(this);
        states[(int)STATE.Trace] = new TraceState(this);
        states[(int)STATE.Return] = new ReturnState(this);
        states[(int)STATE.Attack] = new AttackState(this);
        states[(int)STATE.Patrol] = new PatrolState(this);
    }

    private void Update()
    {
        states[(int)curState].Update(); // where ĸ��ȭ�� ��ü�鳢�� �˾Ƽ� ���� ���¿� ���ؼ� evaluate ���ֱ� ������, frame �� ������Ʈ�Ҷ����� ���� ������¿� ���� update function�� �������ָ� �ǰڴ�. 

    }

    private void Start()
    {
        detectRange = 4;
        moveSpeed = 1; 
        attackRange = 1;
        curState = STATE.Idle;
        states[(int)curState].Enter(); // �̷��� ���¸ӽ��� ���鶧��, ���� �����ϴ� ���¸� Ʈ���� �Ͽ� �ָ� ��ü�� ���ѻ��¸ӽ��� ������ �Ҽ� �ְڴ�. 

        player = GameObject.FindGameObjectWithTag("Player").transform; // where some state of monster depend on player's current position. 
        returnPosition = transform.position; 
    }

    public void ChangeState(STATE state)
    {
        states[(int)curState].Exit();
        curState = state;
        states[(int)curState].Enter();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

namespace BeeState
{
    public enum STATE
    {
        Idle, Trace, Return, Attack, Patrol, Size
    }
    public class IdleState : StateBase<Bee>
    {
        private float idleTime; 
        public IdleState(Bee owner) : base(owner)
        {}
        public override void Enter()
        {
            Debug.Log("Idle Enter");
        }
        public override void Exit()
        {
            Debug.Log("Idle Exit"); 
        }
        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            idleTime += Time.deltaTime;
            if (idleTime > 2)
            {
                idleTime = 0; 
                owner.ChangeState(STATE.Patrol);
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
            {
                owner.ChangeState(STATE.Idle);
            }
        }
    }

    public class TraceState : StateBase<Bee>
    {
        public TraceState(Bee owner) : base(owner)
        {
        }
        public override void Enter()
        {
            Debug.Log("Trace Enter");
        }

        public override void Exit()
        {
            Debug.Log("Trace Exit");
        }

        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            Vector2 dir = (owner.player.position - owner.transform.position).normalized;
            // �÷��̾� �i�ư��� 
            owner.transform.Translate(dir * owner.moveSpeed * Time.deltaTime);

            if (Vector2.Distance(owner.player.position, owner.transform.position) > owner.detectRange)
            {
                owner.ChangeState(STATE.Return);
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.attackRange)
            {
                owner.ChangeState(STATE.Attack);
            }
        }
    }

    public class ReturnState : StateBase<Bee>
    {
        public ReturnState(Bee owner) : base(owner)
        {
        }

        public override void Enter()
        {
            Debug.Log("Return Enter");
        }

        public override void Exit()
        {
            Debug.Log("Return Exit");
        }

        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            // ���� �ڸ��� ���ư��� ���� 
            Vector2 dir = (owner.returnPosition - owner.transform.position).normalized;
            owner.transform.Translate(dir * owner.moveSpeed * Time.deltaTime);

            //���� �ڸ��� �����ߴٸ�, 
            if (Vector2.Distance(owner.transform.position, owner.returnPosition) < 0.02f)
            {
                owner.ChangeState(STATE.Idle);
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
            {
                owner.ChangeState(STATE.Trace); 
            }
        }
    }

    public class AttackState : StateBase<Bee>
    {
        private float lastAttackTime = 0;
        public AttackState(Bee owner) : base(owner)
        {
        }

        public override void Enter()
        {
            Debug.Log("Attack Enter");
        }
        public override void Exit()
        {
            Debug.Log("Attack Exit");
        }
        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }
        public override void Update()
        {
            if (lastAttackTime > 1)
            {
                Debug.Log("����");
                lastAttackTime = 0;
            }
            lastAttackTime += Time.deltaTime;

            //�����ϴ��� ���� ��Ÿ� �ۿ� �ְԵȴٸ�, 
            if (Vector2.Distance(owner.player.position, owner.transform.position) > owner.attackRange)
            {
                owner.ChangeState(STATE.Trace);
            }
        }
    }

    public class PatrolState : StateBase<Bee>
    {
        public PatrolState(Bee owner) : base(owner)
        {
        }

        public override void Enter()
        {
            owner.patrolIndex = (owner.patrolIndex + 1) % owner.patrolPoints.Length; //�̷��� Modulus �� �̿��ؼ� list�� �ִ� ������ ����������, count�� ���� �ʴ� �������� �ݺ�Ȯ���� �����ϴ�.
            Debug.Log("Patrol Enter");
        }
        public override void Exit()
        {
            Debug.Log("Patrol Exit");
        }
        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }
        public override void Update()
        {
            // ���� ����: ��Ʈ�� ����Ʈ���� 


            // Patrol point�� ���� 
            Vector2 dir = (owner.patrolPoints[owner.patrolIndex].position - owner.transform.position).normalized;
            owner.transform.Translate(dir * owner.moveSpeed * Time.deltaTime);

            if (Vector2.Distance(owner.transform.position, owner.patrolPoints[owner.patrolIndex].position) < 0.02f)
            {
                owner.ChangeState(STATE.Idle); 
            }

            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
            {
                owner.ChangeState(STATE.Trace);
            }
        }
    }

}
