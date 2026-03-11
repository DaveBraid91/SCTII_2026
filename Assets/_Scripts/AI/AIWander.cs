using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIWander : AiBase
{
    [SerializeField] private float wanderRadius;
    private Vector3 _initialPosition;
    private Vector3 _randomEndPoint;

    protected override void Awake()
    {
        base.Awake();

        _initialPosition = transform.position;
        SetNewRandomPoint();
    }

    private void Update()
    {
        if(!agent.enabled) return;

        if (agent.remainingDistance <= breakingDistance && !agent.pathPending)
        {
            SetNewRandomPoint();
            //_initialPosition = transform.position;
        }
    }

    private void SetNewRandomPoint()
    {
        _randomEndPoint = _initialPosition + Random.insideUnitSphere * wanderRadius;

        _randomEndPoint.y = 0;

        agent.SetDestination(_randomEndPoint);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _initialPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (agent != null)
        {
            Gizmos.DrawWireSphere(_randomEndPoint, 0.5f);
            Gizmos.DrawWireSphere(agent.destination, 0.5f);
        }
    }
}
