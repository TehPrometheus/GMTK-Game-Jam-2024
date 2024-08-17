using UnityEngine;
using UnityEngine.AI;

public class EnemyVirus : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 5f;

    private Vector3 targetDir;
    private NavMeshAgent agent;
    private float dirChangeCooldown;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        targetDir = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position + targetDir;
        Debug.DrawLine(transform.position, newPos, Color.red);
        HandleDirectionChange();
        agent.SetDestination(newPos);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void HandleDirectionChange()
    {
        dirChangeCooldown -= Time.deltaTime;

        if (dirChangeCooldown < 0)
        {
            var randomAngle = Random.Range(0f, 360f);
            var rotation = Quaternion.AngleAxis(randomAngle, transform.forward);

            targetDir = rotation * targetDir;
            dirChangeCooldown = Random.Range(0f, 2f);
        }
    }
}
