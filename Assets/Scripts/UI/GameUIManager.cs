using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas, winCanvas, 
    settingsCanvas, pauseCanvas, hudCanvas;

    [SerializeField] private TextMeshProUGUI coinsCountWinText;
    [SerializeField] private TextMeshProUGUI coinsCountDeathText;
    [SerializeField] private Camera playerCamera;

    private GameManager gameManager;
    private float coinsCount;
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

    private void SetCoinsCount(){
        coinsCount = (float) gameManager.Coins - (float) gameManager.CoinsEarned;
        float targetValueCoins = (float) gameManager.Coins;
        StartCoroutine(AddValue(coinsCount,targetValueCoins));
    }

    private IEnumerator AddValue(float coinsCount,float targetValueCoins){
        while(coinsCount != targetValueCoins){
            coinsCount+= 10;
            coinsCountWinText.text = ((int)coinsCount).ToString();
            coinsCountDeathText.text = ((int)coinsCount).ToString();
            yield return null;
        }
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
            SetCoinsCount();
        }
    }

    public void HandleWin()
    {
        if(!winCanvas.activeSelf)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0.0f;
            SetCoinsCount();
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

    public void PlayAgain(){
        SceneManager.LoadScene("Sitio");
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
