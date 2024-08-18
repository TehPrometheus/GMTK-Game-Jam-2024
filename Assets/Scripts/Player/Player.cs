using System;
using System.Collections.Generic;
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
    private Rigidbody2D rb;
    private Vector2 mousePos;
    InputReader input;
    public float knockbackForce = 5f;
    private float delay = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody2D>();
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
        if (input.InfoScreen > 0f)
        {
            int[] res = new int[] { resources.gluttonyLevel, resources.speedLevel, resources.immunityLevel };
            uiManager.UpdateInfoScreen(true, res);
        }
        else
        {
            int[] res = new int[] { resources.gluttonyLevel, resources.speedLevel, resources.immunityLevel };
            uiManager.UpdateInfoScreen(false, res);
        }
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
            maxDashCoolDown -= resources.dashLevel * 0.55f;
            dashCoolDown = Mathf.Min(dashCoolDown, maxDashCoolDown);

            // Only move player when not in the action of Dashing
            MovePlayer();
        }

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

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void MovePlayer()
    {
        Vector3 delta = new Vector3(input.Move.x, input.Move.y, 0) * speed * Time.deltaTime;
        transform.position += delta;
        //rb.MovePosition(rb.position + input.Move * speed * Time.fixedDeltaTime);
    }

    void MoveCamera()
    {

        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);

    }

    void ZoomCamera(bool zoomOut)
    {
        // If player is changing size zoom in or out, otherwise keep the same distance.
        if (zoomOut)
            mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize - transform.localScale.x * cameraZoomFactor, minCameraSize);
        else
            mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize + transform.localScale.x * cameraZoomFactor, maxCameraSize);
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
                ZoomCamera(true);
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
        ZoomCamera(false);
        sizeLevelChanged?.Invoke(sizeLevel);
    }

    void ResetSizeXP()
    {
        sizeXP = 0;
    }

    public void PlayKnockBack(GameObject sender)
    {
        //Debug.Log("Knockback engaged");
        StopAllCoroutines();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private System.Collections.IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
    }
}
