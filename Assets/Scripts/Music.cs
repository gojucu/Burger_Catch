using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;

    public bool isMute,isSoundMute;

    public Sprite MusicOffSprite, MusicOnSprite, SoundOffSprite, SoundOnSprite;

    GameObject musicButtonImg;
    GameObject audioButtonImg;

    #region SINGLETON PATTERN
    private static Music _instance;

    public static Music Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)//Singleton - Menuye geri geldiğinde diğer music objesini yoketmek için.
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion

        DontDestroyOnLoad(transform.gameObject);


        _audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("music", 1) == 0)//İlk başta susturulmuş mu bak
        {
            MuteMusic();
        }
        if (PlayerPrefs.GetInt("sounds", 1) == 0)//İlk başta susturulmuş mu bak
        {
            MuteSound();
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void MuteMusic()
    {
        isMute = !isMute;
        if (isMute == false)
        {
            PlayMusic();
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.Save();

            musicButtonImg = GameObject.Find("MusicButtonImg");
            musicButtonImg.GetComponent<Image>().sprite = MusicOnSprite;

        }
        else if (isMute == true)
        {
            StopMusic();
            PlayerPrefs.SetInt("music", 0);
            PlayerPrefs.Save();

            musicButtonImg = GameObject.Find("MusicButtonImg");
            musicButtonImg.GetComponent<Image>().sprite = MusicOffSprite;

        }
    }
    public void MuteSound()//burda mute işlemi değil sadece ayarı kaydediyor OrderControl kısmında ses çıkmadan önce kontrol ediyor
    {
        isSoundMute = !isSoundMute;
        if (isSoundMute == false)
        {
            PlayerPrefs.SetInt("sounds", 1);
            PlayerPrefs.Save();

            audioButtonImg = GameObject.Find("AudioButtonImg");
            audioButtonImg.GetComponent<Image>().sprite = SoundOnSprite;

        }
        else if (isSoundMute == true)
        {
            PlayerPrefs.SetInt("sounds", 0);
            PlayerPrefs.Save();
            
            audioButtonImg = GameObject.Find("AudioButtonImg");
            audioButtonImg.GetComponent<Image>().sprite= SoundOffSprite;

        }
    }
}
