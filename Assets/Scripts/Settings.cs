using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer _musicMixer;
    [SerializeField] Slider _bgMusicSlider;
    [SerializeField] Slider _sfxVolumeSlider;

    public void BGMusicMixerController()
    {
        _musicMixer.SetFloat("BG_Music", _bgMusicSlider.value);
    }

    public void SFXVolumeMixerController()
    {
        _musicMixer.SetFloat("SFX", _sfxVolumeSlider.value);
    }
}
