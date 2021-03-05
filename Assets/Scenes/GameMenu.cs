using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

using System.IO;
public class GameMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI menuCoinsUIText;
    [SerializeField] Text shopCoinsUIText;

    public Sprite MusicOffSprite;
    public Sprite MusicOnSprite;
    public Sprite SoundOffSprite;
    public Sprite SoundOnSprite;
    public Image musicBtnImg;
    public Image soundBtnImg;
    public GameObject spawner;

    Music music;
    private void Start()
    {
        music = FindObjectOfType<Music>();
        Time.timeScale = 1;
        GetHighscore();
        UpdateAllCoinsUIText();
    }
    public void UpdateAllCoinsUIText()//bütün coin textlerini değiştiriyor
    {
        int deneme = SaveSystem.GetCoins();//**** Yeni
        menuCoinsUIText.text = deneme.ToString();
        shopCoinsUIText.text = deneme.ToString();

        //for (int i = 0; i < allCoinsUIText.Length; i++)
        //{
        //    allCoinsUIText[i].text = deneme.ToString();
        //}
    }
    public static void DeleteSaveData()//Test için bu silinecek
    {
        string path = Application.persistentDataPath + "/shops.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        string path2 = Application.persistentDataPath + "/players.dat";
        if (File.Exists(path2))
        {
            File.Delete(path2);
        }
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            musicBtnImg.sprite = MusicOnSprite;
        }
        else
        {
            musicBtnImg.sprite = MusicOffSprite;
        }
        if (PlayerPrefs.GetInt("sounds", 1) == 1)
        {
            soundBtnImg.sprite = SoundOnSprite;
        }
        else
        {
            soundBtnImg.sprite = SoundOffSprite;
        }

    }
    public void MuteButton()
    {
        music.MuteMusic();
    }
    public void MuteSound()
    {
        music.MuteSound();
    }

    public void SpawnerKapa()
    {
        spawner.SetActive(false);
    }
    private void GetHighscore()
    {
        //PlayerData data = SaveSystem.LoadPlayer();//burayı ayarla nasıl geliceke

        highScore.text = SaveSystem.GetHighScore().ToString(); //olmadı bunu neresi çağırıyorsa direk oraya yaz
        //if (data == null)
        //{
        //    highScore.text = "0" ;
        //}
        //else
        //{
        //    data.highScore.ToString();
        //    highScore.text = data.highScore.ToString(); ;
        //}
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Time.timeScale = 1;
    }
}
