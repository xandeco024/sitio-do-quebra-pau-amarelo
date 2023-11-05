using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ConfirmSelectButton : MonoBehaviour
{
    public Text warningSelectHero;
    bool championConfirmed;
    string GameScene;
    private AudioSource audioSource;
    public GameObject ScriptChampionSelection;
    string championSelectedConfirmed;
    void OnButtonPressed()
    {
        if(championConfirmed)
        {
            audioSource.Play();
            SceneManager.LoadScene(GameScene);
        }

    }
    void Start()
    {
        championConfirmed = ScriptChampionSelection.GetComponent<ChampionSelection>().isChampionSelected;
        championSelectedConfirmed = ScriptChampionSelection.GetComponent<ChampionSelection>().ChampionSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if(!championConfirmed)
        {
            VisibleText(); //Mensagem de Aviso para Escolher um Herói
        }
        else
        {
            InvisibleText(); //Se não escolheu, não há porque avisar
        }
    }

    public void VisibleText()
    {
        Color textColor = warningSelectHero.color;
        textColor.a = 1f;
        warningSelectHero.color = textColor;

    }

    public void InvisibleText()
    {
        Color textColor = warningSelectHero.color;
        textColor.a = 0f;
        warningSelectHero.color = textColor;
    }
}
