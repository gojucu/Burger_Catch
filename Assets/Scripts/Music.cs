using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;

    public bool isMute,isSoundMute;

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

        }
        else if (isMute == true)
        {
            StopMusic();
            PlayerPrefs.SetInt("music", 0);
            PlayerPrefs.Save();
        }
    }
    public void MuteSound()//burda mute işlemi değil sadece ayarı kaydediyor OrderControl kısmında mute yapılıyor olması lazım.
    {
        isSoundMute = !isSoundMute;
        if (isSoundMute == false)
        {
            PlayerPrefs.SetInt("sounds", 1);
            PlayerPrefs.Save();
        }else if (isSoundMute == true)
        {
            PlayerPrefs.SetInt("sounds", 0);
            PlayerPrefs.Save();
        }
    }
}
