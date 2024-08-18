using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EbolaBehaviour : MonoBehaviour
{
    private enum enemyAIStates
    {
        wander,
        evade
    }
    private enemyAIStates currentState = enemyAIStates.wander;
    [Range(0, 10)]
    public float speed = 3f;
    private Transform playerTransform;
    private Vector3 targetDir;
    private NavMeshAgent agent;
    private float dirChangeCooldown;
    public int pointValue = 100; // the amount of point awarded for killing this enemy
    public int pointIncreaseValue = 50; // the extra amount of points for gluttony level
    private UIManager uiManager;
    private SpawnVirus virusSpawner;
    public event Action<int> enemyKilled;
    public event Action playerSpiked;
    public event Action<int[]> resourcesReleased;
    private Resources resources;
    private Animator animator;
    [Range(0, 10)]
    public float evadeDistance = 5f; // the minimal distance between the enemy and the player before it begins to evade the player
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        Player player = FindAnyObjectByType<Player>();
        resources = player.GetComponent<Resources>();
        playerTransform = player.transform;
        uiManager = FindAnyObjectByType<UIManager>();
        virusSpawner = FindAnyObjectByType<SpawnVirus>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        agent.updateUpAxis = false;
        agent.speed = speed;
        targetDir = transform.up;
        enemyKilled += uiManager.IncrementScore;
        enemyKilled += virusSpawner.VirusDied;
        resourcesReleased += resources.AddResources;
        enemyKilled += player.UpdateSizeXP;
        playerSpiked += player.DecreaseSizeLevel;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < evadeDistance)
        {
            currentState = enemyAIStates.evade;
        }
        else
        {
            currentState = enemyAIStates.wander;
        }
        ExecuteBehaviour();
    }

    private void ExecuteBehaviour()
    {
        switch (currentState)
        {
            case enemyAIStates.wander:
                ExecuteWanderState();
                break;
            case enemyAIStates.evade:
                ExecuteEvadeState();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Die();
        }
    }

    private void ExecuteWanderState()
    {
        HandleDirectionChange();
        var destination = transform.position + targetDir;
        agent.SetDestination(destination);
        FaceTarget();
        Debug.DrawLine(transform.position, destination, Color.red);
    }
    

    private void ExecuteEvadeState()
    {
        var playerPos = playerTransform.position;
        var myPos = transform.position;
        var vectorToPlayerNormalized = (playerPos - myPos).normalized;
        var destination = -vectorToPlayerNormalized * speed + myPos;

        Debug.DrawLine(transform.position, destination, Color.green);

        agent.SetDestination(destination);
        FaceTarget();
    }

    public void Die()
    {
        pointValue += resources.gluttonyLevel * pointIncreaseValue;
        enemyKilled?.Invoke(pointValue);
        int[] resourceAmounts = new int[] { 105, 0, 0, 0 };
        resourcesReleased?.Invoke(resourceAmounts);
        Destroy(gameObject);
    }

    void HandleDirectionChange()
    {
        dirChangeCooldown -= Time.deltaTime;

        if (dirChangeCooldown < 0)
        {
            var randomAngle = UnityEngine.Random.Range(0f, 360f);
            var rotation = Quaternion.AngleAxis(randomAngle, transform.forward);
            
            targetDir = rotation * targetDir;
            dirChangeCooldown = UnityEngine.Random.Range(0f, 2f);
        }

    }
    void FaceTarget()
    {
        var vel = agent.velocity;
        vel.z = 0;

        if (vel != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,vel);
        }
    }
}
