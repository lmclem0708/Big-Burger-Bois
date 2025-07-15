using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    public int score = 0;
    public int lives = 5;
    public int pointsPerHit = 5;
    public int pointsToWin = 100;

    [Header("UI")]
    public Text scoreText;
    public Text livesText;
    public Text resultText;

    [Header("Win/Lose Effects")]
    public GameObject explosionPrefab;   // optional
    public Transform target;             // drag your Target here

    void Start()
    {
        UpdateUI();
    }

    public void Hit()
    {
        score += pointsPerHit;
        ShowResult("Hit!");
        UpdateUI();
        if (score >= pointsToWin) Win();
    }

    public void Miss()
    {
        lives--;
        ShowResult("Miss!");
        UpdateUI();
        if (lives <= 0)
        {
            Lose();
        }
        
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";
    }

    void ShowResult(string msg)
    {
        resultText.text = msg;
    }

    void Win()
    {
        ShowResult("You win!");
        if (explosionPrefab != null && target != null)
            Instantiate(explosionPrefab, target.position, Quaternion.identity);
        if (target != null) Destroy(target.gameObject);
        // disable further shooting if you like:
        // FindObjectOfType<AngleArcher>().enabled = false;
    }

    void Lose()
    {
        ShowResult("You're fired!");
        // FindObjectOfType<AngleArcher>().enabled = false;
    }
}
