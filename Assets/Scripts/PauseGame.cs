using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

    //bool isPaused = false;//Bunu kullanmıyor gibisin kontrol et sil
    public Animator anim;

    public GameObject gameUI, pauseScreen, optionsScreen;
    public Sprite MusicOffSprite, MusicOnSprite;
    public Sprite SoundOffSprite, SoundOnSprite;
    public Image musicImg, audioImg;

    public Button pauseButton;

    bool isOptions;
    Music music;
    private void Start()
    {
        music = FindObjectOfType<Music>();

        pauseButton.interactable = false;//Oyun açılırken geri sayım animasyonunda butona basılmaması için
        anim = GetComponent<Animator>();
        anim.Play("CountDownAnim2");//Oyun ilk açılırkenki geri sayım.
        StartCoroutine(ActivateGameUI());//Butonun animasyonu beklemesi için gereken fonksiyon
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            musicImg.sprite = MusicOnSprite;
        }
        else
        {
            musicImg.sprite = MusicOffSprite;
        }
        if (PlayerPrefs.GetInt("sounds", 1) == 1)
        {
            audioImg.sprite = SoundOnSprite;
        }
        else
        {
            audioImg.sprite = SoundOffSprite;
        }
    }

    IEnumerator ActivateGameUI()
    {
        //2 saniye bekle butonu aktifleştir
        yield return new WaitForSecondsRealtime(2);
        pauseButton.interactable = true;
    }

    public void Pause()
    {
        Time.timeScale = 0;

        gameUI.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void StartCountDownToResume()
    {
        gameUI.SetActive(true);
        pauseScreen.SetActive(false);
        pauseButton.interactable = false;
        anim.Play("CountDownAnim");//Bu animasyon sonunda ResumeGame() çağırıyor
    }
     
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.interactable = true;
    }

    public void OpenOptions()
    {
        Debug.Log("sonucilk:" + isOptions);
        isOptions = !isOptions;
        Debug.Log("sonucSon:" + isOptions);
        if (isOptions == false)
        {
            pauseScreen.SetActive(true);
            optionsScreen.SetActive(false);
        }
        else if (isOptions == true)
        {
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(true);
        }

    }

    public void MuteMusic()
    {
        music.MuteMusic();
    }
    public void MuteSound()
    {
        music.MuteSound();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
