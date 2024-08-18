using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Enemy spike ability -> get spiked by enemy -> lower your size level
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
    private const float sizeChange = 0.2f;
    private List<int> sizeLevelThresholds = new List<int>(6) { 0, 10, 25, 50, 100, 175 };
    private int sizeXP = 0;
    private int sizeLevel = 1;
    public event Action<int> sizeLevelChanged;
    public UIManager uiManager;

    // ExecuteDashState variables
    [Header("ExecuteDashState Variables")]
    [Range(1, 10)]
    public float maxDashMultiplier = 10f;
    private float maxDashTime = 0.5f;
    private float currentDashTime = 0f;
    [Range(0, 10)]
    public float maxDashCoolDown = 6f;
    private float dashCoolDown;
    public TextMeshProUGUI coolDownText;

    // Speed variables
    [Header("Speed Variables")]
    [Range(0, 10)]
    public float speed = 5f;
    


    [Header("Camera Variables")]
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Camera mainCamera;
    [Range(0, 0.01f)]
    public float cameraZoomFactor = 0.01f;
    [Range(5f, 20f)]
    public float maxCameraSize = 20f;
    [Range(5f, 20f)]
    public float minCameraSize = 5f;


    private Resources resources;

    InputReader input;
    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<InputReader>();
    }
    private void Start()
    {

        resources = GetComponent<Resources>();
        dashCoolDown = maxDashCoolDown;
        sizeLevelChanged += uiManager.UpdateMultiplierValue;
    }
    // Update is called once per frame
    void Update()
    {
        // If player presses shift and player has waited cooldown seconds, start dashing
        if (input.Dash > 0f && dashCoolDown >= maxDashCoolDown && input.Move.magnitude > 0)
        {
            // Is currently dashing
            while (currentDashTime <= maxDashTime)
            {
                // Add dashTime until max is reached
                currentDashTime += Time.deltaTime;
                Dash(currentDashTime);
                DecreaseSizeLevel();
            }

            // Reset variables
            currentDashTime = 0f;
            dashCoolDown = 0f;


        }
        else
        {
            // Increase cooldown until at max dash cooldown
            dashCoolDown += Time.deltaTime;
            maxDashCoolDown = 6f;
            maxDashCoolDown -= resources.dashLevel*0.55f;
            dashCoolDown = Mathf.Min(dashCoolDown, maxDashCoolDown);
            
            // Only move player when not in the action of Dashing
            MovePlayer();
        }

        // Debug text
        coolDownText.text = dashCoolDown.ToString();

        // Growing Mechanic
        //if (input.Grow > 0f)
        //{
        //    isShrinking = false;
        //    Grow();
        //}
        //else if (input.Shrink > 0f)
        //{
        //    isGrowing = false;
        //    Shrink();
        //}
        //else
        //{
        //    isGrowing = false;
        //    isShrinking = false;
        //}


        MoveCamera();
    }

    void MovePlayer()
    {
        // Create a new Vector3 representing the change in movement since the last frame
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0);
        // Multiply by speed so that we can control it
        delta *= speed * (resources.speedLevel + 1);
        // Multiply by deltaTime so that the movement is framerate independent
        delta *= Time.deltaTime;

        // Update the player's position by adding the change in movement
        transform.position += delta;
    }

    void MoveCamera()
    {
        // If player is changing size zoom in or out, otherwise keep the same distance.
        if (isGrowing)
        {
            mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize + transform.localScale.x * cameraZoomFactor, maxCameraSize);

        }
        else if (isShrinking)
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
        speed /= Time.deltaTime * growMultiplier + 1;
        isGrowing = true;

    }
    void Shrink()
    {
        // Decrease totalSize var and scale (until min is reached). Reduce speedMultiplier
        totalSize -= Time.deltaTime * growMultiplier;
        totalSize = Mathf.Max(totalSize, minTotalSize);
        transform.localScale = new Vector3(totalSize, totalSize, 0);
        speed *= Time.deltaTime * growMultiplier + 1;
        isShrinking = true;

    }

    public void UpdateSizeXP(int basePoints)
    {
        sizeXP++;
        //Debug.Log("My sizeXP is " + sizeXP);
        IncreaseSizeLevel();
    }

    private void IncreaseSizeLevel()
    {
        for (int i = 0; i < sizeLevelThresholds.Count - 1; i++)
        {
            if (sizeXP == sizeLevelThresholds[i])
            {
                transform.localScale += new Vector3(sizeChange, sizeChange, 0);
                sizeLevel = i + 1;
                Debug.Log("I have increased to level " + sizeLevel);
                Debug.Log("My sizeXP is " + sizeXP);
                sizeLevelChanged?.Invoke(sizeLevel);
                return;
            }
        }
    }

    public void DecreaseSizeLevel()
    {
        if (sizeLevel <= 1)
            return;
        sizeXP = sizeLevelThresholds[sizeLevel - 2];
        sizeLevel--;
        Debug.Log("I have decreased to level " + sizeLevel);
        Debug.Log("My sizeXP is " + sizeXP);
        transform.localScale -= new Vector3(sizeChange, sizeChange, 0);
        sizeLevelChanged?.Invoke(sizeLevel);
    }

    void ResetSizeXP()
    {
        sizeXP = 0;
    }

}
