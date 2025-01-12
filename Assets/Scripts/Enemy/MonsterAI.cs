using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    public float stopDistance;
    public float idleSpeed;
    public float chaseSpeed;
    private float scaredTimer = 0;
    private float scaredPeriod = 15;
    private bool isScared = false;

    [SerializeField] private float maxLifeTime = 20;
    [SerializeField] private float lifeTimer = 0;
    private GameObject spawner { get; set; }
    private Animator anim;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            runAway();
            scaredTimer += Time.deltaTime;
        }
        else
        {
            if (AudioLoudnessDetector.instance.GetLoudnessFromMicrophone() > 1)
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
        if (spawner == null) return;
        agent.SetDestination(spawner.transform.position);
    }

    void runAway()
    {
        agent.speed = chaseSpeed;

        Vector3 runDirection = (transform.position - target.position);
        agent.SetDestination(transform.position + runDirection);

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

    void attack() { 
        anim.SetTrigger("isAttacking");
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
