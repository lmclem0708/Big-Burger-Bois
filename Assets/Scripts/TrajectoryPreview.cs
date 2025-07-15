using UnityEngine;

// Ensure a LineRenderer exists on this GameObject
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;        // assign your BurgerSpawnPoint here

    [Header("Settings")]
    public float previewTime = 0.5f;    // seconds ahead to preview

    // These get driven each frame by AngleArcher
    [HideInInspector] public float burgerSpeed;
    [HideInInspector] public float powerFactor;  // 0–1 from your power slider

    private LineRenderer lr;

    void Awake()
    {
        // Auto‑grab the existing LineRenderer on this object
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (spawnPoint == null || lr == null)
            return;

        // Starting point of the line
        Vector3 start = spawnPoint.position;

        // Compute exact velocity your burger will have
        Vector3 velocity = spawnPoint.right * (burgerSpeed * powerFactor);

        // Where it will be after previewTime seconds
        Vector3 end = start + velocity * previewTime;

        // Draw a simple 2‑point line
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
