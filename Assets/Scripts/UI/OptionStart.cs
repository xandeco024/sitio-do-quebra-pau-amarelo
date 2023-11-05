using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionStart : MonoBehaviour
{
    [SerializeField] string optionSceneName;
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnButtonClick()
    {
        audioSource.Play();
        SceneManager.LoadScene(optionSceneName);
    }
}
