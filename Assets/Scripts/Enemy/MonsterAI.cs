using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Transform targetPos;
    private UnityEngine.AI.NavMeshAgent agent;
    private float stopDistance;
    private DangerZone dangerZone;
    private float idleSpeed;
    private float chaseSpeed;
    private float timer = 0;
    private float timeForNewDir = 1;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        halfWanderDirectionRange = wanderDirectionRange / 2;
    }

    void Update()
    {
        if (targetPos == null || dangerZone == null) return;
        if (dangerZone.IsIinDangerZone(targetPos.position)) ChaseTarget();
        else wander();
    }

    void ChaseTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPos.position);
        agent.speed = chaseSpeed;

        if (distanceToPlayer > stopDistance)
        {
            agent.SetDestination(targetPos.position);
        }
        else
        {
            attack();
            agent.ResetPath();
        }
    }

    void attack() { }

    void wander()
    {
        timer += Time.deltaTime;
        agent.speed = idleSpeed;

        if (timer >= timeForNewDir || !dangerZone.IsIinDangerZone(GetPointAtOffset(transform.position, transform.rotation, wanderRayNorm * 2, 0)))
        {
            chooseNewDest();
            timer = 0;
        }
    }

    private float wanderDirectionRange = 0.52f; // pi/6 radians
    private float halfWanderDirectionRange;
    private float wanderRayNorm = 1;
    void chooseNewDest()
    {
        Vector3 newDest = GetPointAtOffset(transform.position, transform.rotation, wanderRayNorm, Random.Range(0, wanderDirectionRange) - halfWanderDirectionRange);
        if (dangerZone.IsIinDangerZone(newDest))
        {
            agent.SetDestination(newDest);
        }
        else
        {
            agent.SetDestination(GetPointAtOffset(transform.position, transform.rotation, wanderRayNorm, 3.14f));
        }
    }

    Vector3 GetPointAtOffset(Vector3 origin, Quaternion rotation, float distance, float angleOffset)
    {
        Quaternion offsetRotation = Quaternion.Euler(0, angleOffset, 0) * rotation;
        Vector3 direction = offsetRotation * Vector3.forward;
        return origin + direction * distance;
    }
}
