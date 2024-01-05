using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas, winCanvas, settingsCanvas, pauseCanvas, hudCanvas;
    private GameManager gameManager;

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

        if(Input.GetKeyDown(KeyCode.Escape) && pauseCanvas.activeSelf)
            pauseCanvas.SetActive(false);

        else if(Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas.activeSelf)
            pauseCanvas.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
