using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPositions;

    [SerializeField] private float timeBetweenEvents;
    private float timeSinceLastEvent;

    private const float maxDangerTime = 600;


    private void Update()
    {
        timeSinceLastEvent += Time.deltaTime;
        if (timeSinceLastEvent > timeBetweenEvents)
        {
            spawnEvent();
            timeSinceLastEvent = 0;
        }
    }

    private void spawnEvent()
    {
        float roll = Random.Range(0f, 1f);

        float timePassedRatio = ((maxDangerTime - Time.time) / maxDangerTime);
        if (timePassedRatio < 0) timePassedRatio = 0;
        float probability = 1 - timePassedRatio;

        if (roll < probability)
        {
            Transform closestSpawner = findClosestPosition(spawnPositions, GameObject.FindWithTag("Player").transform);
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(closestSpawner.position, out hit, 1, UnityEngine.AI.NavMesh.AllAreas))
            {
                GameObject agent = Instantiate(enemyPrefab, hit.position, Quaternion.identity);

                UnityEngine.AI.NavMeshAgent navAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (navAgent != null)
                {
                    navAgent.SetDestination(GameObject.FindWithTag("PlayerCapsule").transform.position);
                }
                agent.GetComponent<MonsterAI>().setTarget(GameObject.FindWithTag("PlayerCapsule").transform);
            }
            else
            {
                Debug.LogError("No valid NavMesh found near the spawn position!");
            }
        }
    }

    private Transform findClosestPosition(List<Transform> positions, Transform playerPosition)
    {
        (float, Transform) pair = (Int32.MaxValue, null);
        foreach (var t in positions)
        {
            float distance = Vector3.Magnitude(t.position - playerPosition.position);
            if (distance < pair.Item1) pair = (distance, t);
        }

        return pair.Item2;
    }
}
