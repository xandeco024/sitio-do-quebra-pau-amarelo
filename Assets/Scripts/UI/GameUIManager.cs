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
        
    }

    public void HandleGameOver()
    {
        if(!gameOverCanvas.activeSelf)
        {
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
