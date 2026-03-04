using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyBehaviour))]
public abstract class AiBase : MonoBehaviour
{
    protected EnemyBehaviour enemyBehaviour;
    protected NavMeshAgent agent;
    [SerializeField] protected float breakingDistance;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        agent.stoppingDistance = breakingDistance;
    }

    protected virtual void OnEnable()
    {
        agent.ResetPath();
        agent.stoppingDistance = breakingDistance;
        //TODO: agent.speed = enemyBehaviour.speed;
    }
}
