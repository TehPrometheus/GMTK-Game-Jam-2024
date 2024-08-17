using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{


    // Dash variables
    [Header("Dash Variables")]
    [Range(1, 10)]
    public float maxDashMultiplier = 10f;
    private float maxDashTime = 0.5f;
    private float currentDashTime = 0f;
    [Range(0, 10)]
    public float maxDashCoolDown = 2f;
    private float dashCoolDown;
    public TextMeshProUGUI coolDownText;

    // Speed variables
    [Header("Speed Variables")]
    [Range(0, 10)]
    public float speed = 5f;
    [Range(1, 10)]
    public float speedMultiplier = 1f;

    [SerializeField]
    Transform cameraTransform;

    InputReader input;
    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<InputReader>();
    }
    private void Start()
    {

        dashCoolDown = maxDashCoolDown;
    }
    // Update is called once per frame
    void Update()
    {
        // If player presses shift and player has waited cooldown seconds
        if(input.Dash > 0f && dashCoolDown >= maxDashCoolDown && input.Move.magnitude>0)
        {
            // Is currently dashing
            while(currentDashTime <= maxDashTime)
            {
                // Add dashTime
                currentDashTime += Time.deltaTime;
                Dash(currentDashTime);
                
            }
            currentDashTime = 0f;
            dashCoolDown = 0f;


        }
        else
        {
            // Increase cooldown until at max dash cooldown
            dashCoolDown += Time.deltaTime;
            // Dash is done, reset all variables
            
            MovePlayer();
        }
        coolDownText.text = dashCoolDown.ToString();
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
    void Dash(float time)
    {
        // Get currentDirection
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);

        // Linear interpolation between the minimum dash value and maximum dash value.
        // See link: https://www.transum.org/Maths/Activity/Graph/Desmos.asp
        float currentDashMultiplier = Mathf.Exp(-time) * maxDashMultiplier;
        delta *= speed * speedMultiplier* currentDashMultiplier;


        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }
}
