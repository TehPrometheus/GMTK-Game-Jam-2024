using DG.Tweening.Core.Easing;
using System.Data.SqlTypes;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Size Variables")]
    [Range(0, 1f)]
    public float growMultiplier = 0.05f;
    public float totalSize = 1f;
    [Range(1, 10f)]
    public float maxTotalSize = 1f;
    [Range(1, 10f)]
    public float minTotalSize = 1f;
    public bool isGrowing = false;
    public bool isShrinking = false;


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

    [Header("Camera Variables")]
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Camera mainCamera;
    [Range(0, 0.01f)]
    public float cameraZoomFactor = 0.01f;
    [Range(5f,20f)]
    public float maxCameraSize = 20f;
    [Range(5f, 20f)]
    public float minCameraSize = 5f;

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
        // If player presses shift and player has waited cooldown seconds, start dashing
        if(input.Dash > 0f && dashCoolDown >= maxDashCoolDown && input.Move.magnitude>0)
        {
            // Is currently dashing
            while(currentDashTime <= maxDashTime)
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
            // Increase cooldown until at max dash cooldown
            dashCoolDown += Time.deltaTime;
            dashCoolDown = Mathf.Min(dashCoolDown, maxDashCoolDown);

            // Only move player when not in the action of Dashing
            MovePlayer();
        }

        // Debug text
        coolDownText.text = dashCoolDown.ToString();


        // Growing Mechanic
        if(input.Grow>0f)
        {
            isShrinking = false;
            Grow();
        }
        else if ( input.Shrink > 0f)
        {
            isGrowing = false;
            Shrink();
        }
        else { 
            isGrowing = false;
            isShrinking = false;
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
        // If player is changing size zoom in or out, otherwise keep the same distance.
        if(isGrowing)
        {
            mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize + transform.localScale.x * cameraZoomFactor, maxCameraSize);
            
        }
        else if(isShrinking)
        {
            mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize - transform.localScale.x * cameraZoomFactor, minCameraSize);
        }
        
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
        
    }
    void Dash(float time)
    {
        // Get currentDirection
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);

        // e^-time
        // See link: https://www.transum.org/Maths/Activity/Graph/Desmos.asp
        float currentDashMultiplier = Mathf.Exp(-time) * maxDashMultiplier;
        delta *= speed * currentDashMultiplier;


        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }
    void Grow()
    {
        // Increase totalSize var and scale (until max is reached). Reduce speedMultiplier
        totalSize += Time.deltaTime * growMultiplier;
        totalSize = Mathf.Min(totalSize, maxTotalSize);
        transform.localScale = new Vector3(totalSize, totalSize, 0);
        speedMultiplier /= Time.deltaTime * growMultiplier + 1;
        isGrowing = true;
        
    }
    void Shrink()
    {
        // Decrease totalSize var and scale (until min is reached). Reduce speedMultiplier
        totalSize -= Time.deltaTime * growMultiplier;
        totalSize = Mathf.Max(totalSize, minTotalSize);
        transform.localScale = new Vector3(totalSize, totalSize, 0);
        speedMultiplier *= Time.deltaTime * growMultiplier + 1;
        isShrinking = true;

    }
}
