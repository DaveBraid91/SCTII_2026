using System;
using UnityEngine;
using UnityEngine.AI;

public class SimplePatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoint;

    private NavMeshAgent _agent;
    private int _currentPatrolPointIndex;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

    private void Update()
    {
        if (_agent.remainingDistance < _agent.stoppingDistance && !_agent.pathPending)
            GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        if(_currentPatrolPointIndex >= patrolPoint.Length) _currentPatrolPointIndex = 0;
            _agent.SetDestination(patrolPoint[_currentPatrolPointIndex++].position);
    }
}
