using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [Header("Lifetime Settings")]
    public float maxLifetime = 3f;

    private float lifeTimer;
    private GameManager gm;

    void Start()
    {
        lifeTimer = maxLifetime;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gm.Miss();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Target"))
        {
            gm.Hit();
            Destroy(gameObject);
        }
    }
}
