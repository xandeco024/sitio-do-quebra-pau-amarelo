using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] clips;
    private AudioSource audioSource;

    [Header("Menu")]
    [SerializeField] private bool optionsOpen;
    [SerializeField] private GameObject optionsCanvas,usernameCanvas,championSelectCanvas;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI textTitle;
    void Start()
    {
        usernameInput.text = (PlayerPrefs.GetString("username") == null)?"":PlayerPrefs.GetString("username");
        audioSource = GetComponent<AudioSource>();

        if (Time.timeScale < 1) Time.timeScale = 1;
    }

    void Update(){
        ChosseYourUsername();
    }

    public void ChosseYourUsername()
    { 
        //audioSource.PlayOneShot(clips[0]);

        string username = usernameInput.text;
        if(username.Length <= 12){
            textTitle.text = "Start the game";
            PlayerPrefs.SetString("username",username);
        }

        else if(username.Length > 12)
            textTitle.text = "The username cannot to be longer 12 caracters";
        
        else if(username == "")
            textTitle.text = "Type your username";
    }

    public void StartTheGame(){
        usernameCanvas.SetActive(false);
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
