using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSelection : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas, optionCanvas, gameCanvas;

    private GameObject previousCanvas;

    public enum GameContext
    {
        MainMenu,
        ChampionSelection,
        InGame,
        Paused
    }

    public void OnMenuButtonPressed()
    {
        if (previousCanvas != null)
        {
            previousCanvas.SetActive(true);
        }
        optionCanvas.SetActive(false);
    }

    public void OnGameStartButtonPressed()
    {
        if (previousCanvas != null)
        {
            previousCanvas.SetActive(true);
        }
        optionCanvas.SetActive(false);
    }

    public void OnOptionsButtonPressed()
    {
        previousCanvas = menuCanvas.activeSelf ? menuCanvas : gameCanvas;
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
}