using System;
using UnityEngine;
using UnityEngine.XR;

public enum EnemyState
{
    Wander,
    FollowTarget,
    Patrol,
    Death,
    Attack
}

public class EnemyBehaviour : MonoBehaviour
{
    [field: SerializeField] 
    public Transform target {  get; private set; }

    public float speed { get; private set; }

    [Header("Current State")]
    [SerializeField] private EnemyState state;

    [SerializeField] private float detectionRadius;

    [SerializeField] private AiBase[] states;

    private SphereCollider _collider;

    private void Start()
    {
        states = GetComponents<AiBase>();
        _collider = gameObject.AddComponent<Collider>() as SphereCollider;
        _collider.radius = detectionRadius;
        _collider.isTrigger = true;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Wander:
                UpdateWander();
                break;
            case EnemyState.FollowTarget:
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Death:
                break;
            case EnemyState.Attack:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateWander()
    {
        if (!PlayerIsOnRange(detectionRadius) /*&& _enemyHealth.CurrentHealth > 0*/ ) return;

        if (false)
        {
            ChangeState(EnemyState.Death);
            speed = 0f;
            return;
        }

        speed = 7f;
        ChangeState(EnemyState.FollowTarget);
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
