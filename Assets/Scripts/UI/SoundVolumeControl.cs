using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolumeControl : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    public AudioSource sfxAudioSource;
    public AudioSource bgmAudioSource;

    private void Start()
    {
        // Configurar os valores iniciais dos Sliders
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);

        // Associar os Sliders aos métodos de ajuste de volume
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBGMVolume);

        // Configurar os volumes iniciais
        UpdateSFXVolume(sfxVolumeSlider.value);
        UpdateBGMVolume(bgmVolumeSlider.value);
    }

    private void UpdateSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void UpdateBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
}
