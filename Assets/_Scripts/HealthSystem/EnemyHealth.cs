using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : BaseHealth
{
    private NavMeshAgent _agent;
    private EnemyBehaviour _enemyBehaviour;

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _enemyBehaviour.IsDead = false;
    }


    protected override void Die()
    {
        _agent.ResetPath();
        _enemyBehaviour.IsDead = true;
        Destroy(gameObject, deathCooldown);
    }
}
