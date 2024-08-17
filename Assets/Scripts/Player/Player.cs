using UnityEngine;

public class Player : MonoBehaviour
{
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
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        // Create a new Vector3 representing the change in movement since the last frame
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);
        // Multiply by speed so that we can control it
        delta *= speed;
        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }

    void MoveCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("I collided with something");
    }


}
