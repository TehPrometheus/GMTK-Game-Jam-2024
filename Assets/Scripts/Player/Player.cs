using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1,10)]
    public float speedMultiplier = 1f;
    [Range(10, 100)]
    public float dashMultiplier = 10f;
    [Range(0, 10)]
    public float speed = 5f;

    [SerializeField]
    Transform cameraTransform;

    InputReader input;
    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<InputReader>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.Dash>0f)
        {
            Dash();
        }
        else
        {
            MovePlayer();
        }
        
        MoveCamera();
    }

    void MovePlayer()
    {
        // Create a new Vector3 representing the change in movement since the last frame
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);
        // Multiply by speed so that we can control it
        delta *= speed*speedMultiplier;
        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }

    void MoveCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }
    void Dash()
    {
        // Get currentDirection
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);
        
        // Multiply by dashmultiplier
        delta *= speed * speedMultiplier*dashMultiplier;
        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }
}
