using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChampionSelection : MonoBehaviour
{
    private AudioSource chooseYourCharacter;
    private AudioSource audioSource;
    public Text buttonText;
    public bool isChampionSelected;
    public string ChampionSelected;
    void Start()
    {
        isChampionSelected = false;
        ChampionSelected = buttonText.text;
        chooseYourCharacter.Play();
    }

    public void OnButtonPressed()
    {
        audioSource.Play();
        isChampionSelected = true;
    }
}
