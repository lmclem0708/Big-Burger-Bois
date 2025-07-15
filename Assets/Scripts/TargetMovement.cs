using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 2f;    // how far up/down
    public float frequency = 1f;    // speed of oscillation

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // vertical sine wave around startPos.y
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
