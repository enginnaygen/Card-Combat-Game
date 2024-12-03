using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource battleSelectMusic;
    [SerializeField] AudioSource[] sfxs;


    private void Awake()
    {
        Singleton();
    }
    void Singleton()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void StopMusic()
    {
        menuMusic.Stop();
        battleSelectMusic.Stop();

        foreach(AudioSource sfx in sfxs)
        {
            sfx.Stop();
        }
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlaySelectPanelMusic()
    {
        StopMusic();
        battleSelectMusic.Play();
    }

    public void PlaySFX(AudioSource sfx)
    {
        StopMusic();
        sfx.Play();
    }
}
