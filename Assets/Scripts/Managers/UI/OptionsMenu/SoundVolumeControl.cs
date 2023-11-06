using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeControl : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    public AudioMixer BGM; // Referência ao Audio Mixer
    public AudioMixer SFX;

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
        if(SFX != null)
        {
            SFX.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    private void UpdateBGMVolume(float volume)
    {
        if(BGM != null)
        {
            BGM.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
    }
}