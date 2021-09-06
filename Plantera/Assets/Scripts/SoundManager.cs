using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;
    public AudioSource UISource;
    public AudioSource sfxSource;
    [Range(0f,1f)] public float musicVolume;
    [Range(0f,1f)]public float sfxVolume;
    
    public static SoundManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        musicVolume = PlayerPrefs.GetFloat("musicVol", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("sfxVol", sfxVolume);
        UISource.volume = sfxVolume;
        musicSource.volume = musicVolume;
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVol",volume);
    }
    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        UISource.volume = volume;
        PlayerPrefs.SetFloat("sfxVol", volume);
    }
    public void PlayMusic(AudioClip clip)
    {
        if(musicClip == null || musicClip.name != clip.name || musicSource.pitch!=1)
        {
            musicClip = clip;
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.pitch = 1;
            musicSource.Play();
        }
    }
    public void LowerMusicPitch()
    {
        musicSource.pitch = 0.5f;
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;
        sfxSource.Play();
    }
}
