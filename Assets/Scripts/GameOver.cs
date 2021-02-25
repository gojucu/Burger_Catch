using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score, highScore;
    public GameObject health1, health2, health3, gameOverPanel, gameUi;
    public static int health;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        score.text = scoreBoard.GetScore().ToString();

        Time.timeScale = 1;
        health = 3;
        health1.gameObject.SetActive(true);
        health2.gameObject.SetActive(true);
        health3.gameObject.SetActive(true);
    }

    void Update()
    {
        if (health > 3)
            health = 3;

        switch (health)
        {
            case 3:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(true);
                break;
            case 2:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(false);
                break;
            case 1:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                break;
            case 0:
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                GameOverActive();
                break;
        }
    }

    private void GameOverActive()
    {
        //Higscore getir ***Belki bunu ayrı fonksiyon yaparsın
        if (scoreBoard.GetScore() > scoreBoard.highScore)
        {
            //PlayerPrefs.SetInt("HighScore", scoreBoard.GetScore()); silinicek**

            scoreBoard.highScore = scoreBoard.GetScore();
            SaveSystem.SavePlayer(scoreBoard);
            highScore.text = scoreBoard.GetScore().ToString();
        }
        else
        {
            highScore.text = scoreBoard.highScore.ToString();
        }
        score.text = scoreBoard.GetScore().ToString();//Yeni score

        gameUi.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

    }
 
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
