using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    public float stopDistance;
    public float idleSpeed;
    public float chaseSpeed;
    private float wanderTimer = 0;
    private float scaredTimer = 0;
    private float scaredPeriod = 30;
    private float timeForNewDir = 1;
    private bool isScared = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        halfWanderDirectionRange = wanderDirectionRange / 2;
    }

    void Update()
    {
        if (target == null) return;
        if (isScared)
        {
            runAway();
            scaredTimer += Time.deltaTime;
        }
        else
        {
            ChaseTarget();
            //wander();
        }
        if (scaredTimer > scaredPeriod)
        {
            scaredTimer = 0;
            isScared = false;
        }
    }

    public void ScareAway()
    {
        isScared = true;
    }

    void runAway()
    {
        agent.speed = chaseSpeed;

        Vector3 runDirection = (transform.position - target.position).normalized;

        Vector3 potentialDestination = transform.position + runDirection;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(potentialDestination, out hit, 1, UnityEngine.AI.NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else { scaredTimer = 0; isScared = false; }

    }

    void ChaseTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        agent.speed = chaseSpeed;

        if (distanceToPlayer > stopDistance)
        {
            agent.SetDestination(target.position);
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
        wanderTimer += Time.deltaTime;
        agent.speed = idleSpeed;

        if (wanderTimer >= timeForNewDir)
        {
            chooseNewDest();
            wanderTimer = 0;
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
