using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas, winCanvas, settingsCanvas, pauseCanvas, hudCanvas;
    private GameManager gameManager;
    private bool isPaused;

   public bool IsPaused { get { return isPaused; } }

    void Start()
    {
       gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        PauseMenu();
    }

    public void HandleGameOver()
    {
        if(!gameOverCanvas.activeSelf)
        {
            if(pauseCanvas.activeSelf || settingsCanvas.activeSelf){
                pauseCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
            }

            gameOverCanvas.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void HandleWin()
    {
        if(!winCanvas.activeSelf)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private void PauseMenu(){
        if(Input.GetKeyDown(KeyCode.P) && pauseCanvas.activeSelf){
           Resume();
        }

        else if(Input.GetKeyDown(KeyCode.P) && !pauseCanvas.activeSelf){
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    public void Resume(){
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
