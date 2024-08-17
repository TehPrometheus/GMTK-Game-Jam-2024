using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVirus : MonoBehaviour
{
    private enum enemyAIStates
    {
        wander,
        evade
    }
    private enemyAIStates currentState = enemyAIStates.wander;
    [Range(0, 10)]
    public float speed = 5f;
    private Transform playerTransform;
    private Vector3 targetDir;
    private NavMeshAgent agent;
    private float dirChangeCooldown;
    private int pointValue = 100; // the amount of point awarded for killing this enemy
    private UIManager uiManager;
    public event Action<int> enemyKilled;
    [Range(0, 10)]
    public float evadeDistance = 5f; // the minimal distance between the enemy and the player before it begins to evade the player
    // Start is called before the first frame update
    void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        playerTransform = player.transform;
        uiManager = FindAnyObjectByType<UIManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        targetDir = transform.up;
        enemyKilled += uiManager.IncrementScore;
        enemyKilled += uiManager.IncrementEnemiesKilled;
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

    private void ExecuteWanderState()
    {
        HandleDirectionChange();
        var destination = transform.position + targetDir;
        agent.SetDestination(destination);
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            Die();
    }

    public void Die()
    {
        enemyKilled?.Invoke(pointValue);
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
}
