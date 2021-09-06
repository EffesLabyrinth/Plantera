using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AudioSource audioSource;
    public void PlayHurt()
    {
        audioSource.volume = SoundManager.instance.sfxVolume;
        audioSource.pitch = Random.Range(1.3f, 1.7f);
        audioSource.Play();
    }
    public void PlayHurtDead()
    {
        audioSource.volume = SoundManager.instance.sfxVolume;
        audioSource.pitch = 0.5f;
        audioSource.Play();
    }
}
