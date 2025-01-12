using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    public float stopDistance;
    public float chaseSpeed;
    private float scaredTimer = 0;
    private float scaredPeriod = 150;
    private bool isScared = false;
    public float scaryNoiseThreshold = 1.5f;

    [SerializeField] private float maxLifeTime = 20;
    [SerializeField] private float lifeTimer = 0;
    private Vector3 spawnerPos;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spawnerPos = transform.position;
        agent.speed = chaseSpeed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (lifeTimer > maxLifeTime)
        {
            Debug.Log("returnHome");
            agent.ResetPath();
            returnHome();
            return;
        }
        lifeTimer += Time.deltaTime;
        if (target == null) return;
        if (isScared)
        {
            returnHome();
            scaredTimer += Time.deltaTime;
        }
        else
        {
            if (AudioLoudnessDetector.instance.GetLoudnessFromMicrophone() > scaryNoiseThreshold)
            {
                isScared = true;
            }
            else { ChaseTarget(); }
        }
        if (scaredTimer > scaredPeriod)
        {
            scaredTimer = 0;
            isScared = false;
        }
    }

    void returnHome()
    {
        agent.SetDestination(spawnerPos);
    }

    void ChaseTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

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

    void attack()
    {
        anim.SetTrigger("isAttacking");
        Debug.Log("ATTACK");
    }

    public void setTarget(Transform pTarget)
    {
        target = pTarget;
    }

    /*void wander()
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
    }*/
}
