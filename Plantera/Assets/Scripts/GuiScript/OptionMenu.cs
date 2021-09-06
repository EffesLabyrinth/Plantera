using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void OnEnable()
    {
        musicSlider.value = SoundManager.instance.musicVolume;
        sfxSlider.value = SoundManager.instance.sfxVolume;
    }

    public void UpdateMusicVolume()
    {
        SoundManager.instance.SetMusicVolume(musicSlider.value);
    }
    public void UpdateSfxVolume()
    {
        SoundManager.instance.SetSfxVolume(sfxSlider.value);
    }
}
