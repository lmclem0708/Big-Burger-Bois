using UnityEngine;
using UnityEngine.UI;

public class AngleArcher : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private float startX;

    [Header("Shooting & Power Meter")]
    public Transform burgerSpawnPoint;
    public GameObject burgerPrefab;
    public float burgerSpeed = 15f;
    public float fireRate = 5f;
    public Slider powerSlider;
    public float powerSpeed = 80f;

    [Header("Trajectory Preview")]
    public TrajectoryPreview trajectoryPreview;

    private float fireCooldown = 0f;
    private float currentPower = 0f;
    private bool rampUp = true;
    private Transform target;

    void Start()
    {
        // lock X
        startX = transform.position.x;
        // find the customer
        var t = GameObject.FindWithTag("Target");
        if (t != null) target = t.transform;
        // init slider range
        if (powerSlider != null)
        {
            powerSlider.minValue = 0f;
            powerSlider.maxValue = 100f;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleAiming();
        UpdatePowerMeter();

        // feed preview exactly how fast & how strong
        float pf = currentPower / 100f;
        if (trajectoryPreview != null)
        {
            trajectoryPreview.powerFactor = pf;
            trajectoryPreview.burgerSpeed = burgerSpeed;
        }

        HandleShooting();

        // cooldown tick
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    void HandleMovement()
    {
        float y = 0f;
        if (Input.GetKey(KeyCode.W)) y = 1f;
        if (Input.GetKey(KeyCode.S)) y = -1f;

        Vector3 delta = new Vector3(0, y, 0) * moveSpeed * Time.deltaTime;
        transform.position += delta;
        // lock X
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    void HandleAiming()
    {
        if (target == null) return;
        Vector2 dir = (Vector2)target.position - (Vector2)burgerSpawnPoint.position;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        burgerSpawnPoint.rotation = Quaternion.Euler(0, 0, ang);
    }

    void UpdatePowerMeter()
    {
        if (powerSlider == null) return;

        // oscillate 0 → 100 → 0
        if (rampUp)
        {
            currentPower += powerSpeed * Time.deltaTime;
            if (currentPower >= 100f)
            {
                currentPower = 100f;
                rampUp = false;
            }
        }
        else
        {
            currentPower -= powerSpeed * Time.deltaTime;
            if (currentPower <= 0f)
            {
                currentPower = 0f;
                rampUp = true;
            }
        }

        powerSlider.value = currentPower;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fireCooldown <= 0f)
        {
            fireCooldown = 1f / fireRate;
            float powerFactor = currentPower / 100f;

            if (burgerPrefab == null || burgerSpawnPoint == null)
            {
                Debug.LogError("Assign burgerPrefab & burgerSpawnPoint in the Inspector!");
                return;
            }

            // spawn & fire straight along the preview line
            GameObject b = Instantiate(
                burgerPrefab,
                burgerSpawnPoint.position,
                burgerSpawnPoint.rotation
            );

            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = burgerSpawnPoint.right * (burgerSpeed * powerFactor);
            }
            else
            {
                Debug.LogError("Burger prefab needs a Rigidbody2D!");
            }
        }
    }
}
