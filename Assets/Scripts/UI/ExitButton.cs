using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnButtonClick()
    {
        audioSource.Play();
        Application.Quit();
    }
}
