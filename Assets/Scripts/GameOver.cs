using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score, highScore;
    public GameObject health1, health2, health3, gameOverPanel, gameUi;
    public static int health;
    public bool isDead;

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

    void Update()//bunu düzeltmeye gerek var mı ? Değiştimi diye bir bool değer atıp  can azalırsa bu bool değer true
    {  //olur.  true ise devam et diyip işlemi yaptıktan sonra bool false çevrilir.
        if (health > 3)
            health = 3;
        if (isDead == true)
        {
            return;
        }
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

    private void GameOverActive()///*** Bunun sonunda score u para olarak varsayıp SaveSystemden addcoin filan diyebilirsin
    {
        //Higscore getir ***Belki bunu ayrı fonksiyon yaparsın
        if (scoreBoard.GetScore() > scoreBoard.highScore)
        {
            scoreBoard.highScore = scoreBoard.GetScore();

            SaveSystem.SetHighScore(scoreBoard.GetScore());//**yeni
            //SaveSystem.SavePlayer(scoreBoard);// Bunu set score yapıcakın
            highScore.text = scoreBoard.GetScore().ToString();
        }
        else
        {
            highScore.text = scoreBoard.highScore.ToString();
        }
        score.text = scoreBoard.GetScore().ToString();//Yeni score
        SaveSystem.AddCoins(scoreBoard.GetScore());
        isDead = true;

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
