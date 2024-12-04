using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource battleSelectMusic;
    [SerializeField] AudioSource[] bgm;
    [SerializeField] AudioSource[] sfx;

    int currentBGM;
    bool playingBGM;


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

        foreach(AudioSource bgmusic in bgm)
        {
            bgmusic.Stop();
        }

        playingBGM = false;
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

    public void PlayBGMusic()
    {
        StopMusic();

        int currentMusic = currentBGM;

        currentBGM = Random.Range(0, bgm.Length);

        while(currentBGM == currentMusic)
        {
            currentBGM = Random.Range(0, bgm.Length);
        }

        bgm[currentBGM].Play();

        playingBGM = true;
    }

    public void PlayeSFX(int SFXToPlay)
    {
        sfx[SFXToPlay].Stop();
        sfx[SFXToPlay].Play();
    }
}
