using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    public int score = 0;
    public int lives = 5;
    public int pointsPerHit = 50;
    public int pointsToWin = 100;
    public GameObject loss;
    public GameObject win;
    public GameObject player;
    public GameObject customer;

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

    private void Update()
    {   
        if (lives <=0)
        {
            lives = 0;
            Lose();
        }

        if (score == 100)
        {
            Win();
        }
    }
    public void Hit()
    {
        score += 50;
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
            lives = 0;
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
        Time.timeScale = 0;
        win.SetActive(true);
        player.SetActive(false);
        customer.SetActive(false);
        ShowResult("You win!");
        if (explosionPrefab != null && target != null)
            Instantiate(explosionPrefab, target.position, Quaternion.identity);
        if (target != null) Destroy(target.gameObject);
        // disable further shooting if you like:
        // FindObjectOfType<AngleArcher>().enabled = false;
    }

    void Lose()
    {
        player.SetActive(false);
        customer.SetActive(false);
        Time.timeScale = 0;
            loss.SetActive(true);
            ShowResult("You're fired!");
               // FindObjectOfType<AngleArcher>().enabled = false;
    }
}
