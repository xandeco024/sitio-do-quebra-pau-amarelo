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
    [SerializeField] private bool usernameTextValid = false;
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

        string nickName = usernameInput.text;

        if(nickName.Length <= 18 && nickName != ""){
            textTitle.text = "Comece o jogo";
            usernameTextValid = true;
        }

        else if(nickName.Length > 18){
            textTitle.text = "O nome de usuario não pode ter mais de 12 caracteres";
            usernameTextValid = false;
        }
        
        else if(nickName == ""){
            textTitle.text = "Digite seu nome de usuario";
            usernameTextValid = false;
        }

        if(usernameTextValid)
            PlayerPrefs.SetString("nickName",nickName);
    }

    public void StartTheGame(){
        if(usernameTextValid){
            usernameCanvas.SetActive(false);
            championSelectCanvas.SetActive(true);
        }
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
