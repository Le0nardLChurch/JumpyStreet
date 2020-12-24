using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject highScorePrefab;
#pragma warning restore 0649
    private Player player;
    private UIController uIController;
    private TMP_Text highscoreText;

    //List<int> highScores;
    int highScore;
    int score;

    private void Update()
    {
        if (score < Mathf.RoundToInt(playerGO.transform.position.z))
        {
            score++;
            uIController.UpdateScore(score);
        }
        if (player.IsDead)
        {
            OnDeath();
        }
    }

    public void SetHighScorePos()
    {
        Vector3 highScorePos = highScorePrefab.transform.position;
        highScorePos.z += highScore;
        highScorePrefab.transform.position = highScorePos;
        uIController.UpdateTopScore(highScore);
    }

    private void OnDeath()
    {
        if (score > highScore)
        {
            highScore = score;
            highscoreText.text = "Highscore: " + highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
            SetHighScorePos();
        }
    }

    private void Awake()
    {
        playerGO = playerGO == null ? FindObjectOfType<Player>().gameObject : playerGO;
        player = playerGO.GetComponent<Player>();
        uIController = FindObjectOfType<UIController>();
        highscoreText = highScorePrefab.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", -1);
        if (highScore == -1)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScore = 0;
        }

        score = 0;
        uIController.UpdateScore(score);
        highscoreText.text = "Highscore: " + highScore.ToString();
        SetHighScorePos();
    }

}





