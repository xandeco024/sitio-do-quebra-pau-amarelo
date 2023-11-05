using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonStartPressed : MonoBehaviour
{
    [SerializeField] string mainSceneName;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        audioSource.Play();
        SceneManager.LoadScene(mainSceneName);
    }
}