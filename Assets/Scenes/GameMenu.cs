using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;

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
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            highScore.text = "0" ;
        }
        else
        {
            data.highScore.ToString();
            highScore.text = data.highScore.ToString(); ;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Time.timeScale = 1;
    }
}
