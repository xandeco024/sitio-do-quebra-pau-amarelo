using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] clips;
    private AudioSource audioSource;

    [Header("Menu")]
    [SerializeField] private bool optionsOpen;
    [SerializeField] private GameObject optionsCanvas, championSelectCanvas;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Time.timeScale < 1) Time.timeScale = 1;
    }

    public void StartGame()
    { 
        //audioSource.PlayOneShot(clips[0]);
        championSelectCanvas.SetActive(true);
    }

    public void Options()
    {
        //audioSource.PlayOneShot(clips[0]);
        optionsCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        //audioSource.PlayOneShot(clips[0]);
        Application.Quit();
    }
}
