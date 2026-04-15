using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Wander,
    FollowTarget,
    Patrol,
    Death,
    Attack
}

[RequireComponent(typeof(AIWander), typeof(AIFollowTarget), typeof(AIPatrol)),
    RequireComponent(typeof(AIDeath), typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{
    [field: SerializeField] 
    public Transform target {  get; private set; }

    public float speed { get; private set; } = 3.5f;

    [Header("Current State")]
    [SerializeField] private EnemyState state;

    [SerializeField] private float detectionRadius;

    [SerializeField] private AiBase[] states;

    private SphereCollider _collider;
    private Animator _anim;
    private bool _isDead;
    public bool IsDead 
    {  
        get 
        { 
            return _isDead; 
        } 
        set 
        { 
            _isDead = value; 
        } 
    }

    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        states = GetComponents<AiBase>();
        ChangeState(state);
    }

    private void Start()
    {
        
        _anim = GetComponent<Animator>();
        _collider = gameObject.AddComponent<SphereCollider>();
        _collider.radius = detectionRadius;
        _collider.isTrigger = true;

        _anim.SetFloat(Speed, speed);
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Wander:
                UpdateWander();
                break;
            case EnemyState.FollowTarget:
                UpdateFollowTarget();
                break;
            case EnemyState.Patrol:
                UpdatePatrol();
                break;
            case EnemyState.Death:
                UpdateDeath();
                break;
            case EnemyState.Attack:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateDeath()
    {
        _anim.SetTrigger(Die);
    }

    private void UpdatePatrol()
    {
        //For now, the conditions to change from patrol are the same as 
        //from Wander, so we are going to save code:
        UpdateWander();
    }

    private void UpdateFollowTarget()
    {
        if (PlayerIsOnRange(detectionRadius) && !_isDead) return;

        if (_isDead)
        {
            ChangeState(EnemyState.Death);
            speed = 0;
            return;
        }

        speed = 3.5f;
        _anim.SetFloat (Speed, speed);
        var dice = Random.Range(0, 100);
        ChangeState(dice >= 50 ? EnemyState.Wander : EnemyState.Patrol);

    }

    private void UpdateWander()
    {
        if (!PlayerIsOnRange(detectionRadius) && !_isDead ) return;

        if (_isDead)
        {
            ChangeState(EnemyState.Death);
            speed = 0;
            return;
        }

        speed = 7f;
        ChangeState(EnemyState.FollowTarget);
        _anim.SetFloat(Speed, speed);
    }

    private void ChangeState(EnemyState newState)
    {
        state = newState;

        for (int i = 0; i < states.Length; i++)
        {
            states[i].enabled = i == (int)state;

            /*if(i == (int)state)
                states[i].enabled = true;
            else   
                states[i].enabled = false;*/
        }
    }

    private bool PlayerIsOnRange(float detectionRadius)
    {
        if(!target) return false;
        var sqrDistance = (target.position - transform.position).sqrMagnitude;
        return sqrDistance <= Mathf.Pow(detectionRadius, 2);
    }
}
