using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class EboloaRuna : MonoBehaviour
{

    private Player player;
    [Range(0f, 5f)]
    public float evadeDistance;
    private Animator animator;
    private NavMeshAgent agent;
    public Animation dashAnim;

    [Header("Dash Variables")]    
    public float dashSpeed;
    public float maxDashCoolDown;
    private float dashCoolDown;
    public float maxDashMultiplier = 10f;
    private float maxDashTime = 0.5f;
    private float currentDashTime = 0f;

    private float speed = 2.5f;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        animator: GetComponent<Animator>();
        dashCoolDown = maxDashCoolDown;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If player is in range and ebola can dash, do the dash!
        if (distanceToPlayer < evadeDistance && dashCoolDown >= maxDashCoolDown)
        {
            agent.isStopped = true;
            // Is currently dashing
            while (currentDashTime <= maxDashTime)
            {
                // Add dashTime until max is reached
                currentDashTime += Time.deltaTime;
                Dash(currentDashTime);

            }

            // Reset variables
            currentDashTime = 0f;
            dashCoolDown = 0f;
        }
        else
        {
            agent.isStopped = false;
            

        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            Destroy(gameObject);
    }
    void Dash(float time)
    {
        // Calculate angle between player and ebola
        Vector2 delta = -(player.transform.position - transform.position).normalized;
 
        // e^-time
        // See link: https://www.transum.org/Maths/Activity/Graph/Desmos.asp
        float currentDashMultiplier = Mathf.Exp(-time) * maxDashMultiplier;
        delta *= speed * currentDashMultiplier;


        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += (Vector3)delta;
    }
   


}
