using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChampionSelectManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI selectedChampionText;
    [SerializeField] private Animator selectedChampionImageAnimator;
    [Header("Botões")]
    [SerializeField] private Button[] championButtons;
    [SerializeField] private Button[] statsButtons;
   
    [SerializeField] private Button[] skillsButtons;
  
    [SerializeField] GameObject[] championsPrefabs;
    [SerializeField] private Button[] rotateButtons;

    [Header("Canvas")]
    [SerializeField] GameObject menuCanvas, championSelectCanvas, optionsGame;

    [Header("Variables")]
    public int rotateChampion;
    private Champion selectedChampion;
    private TextMeshProUGUI[] statsText;
    [SerializeField] private AudioSource clickSFX;


    void Start()
    {
        statsText = new TextMeshProUGUI[statsButtons.Length];
        for (int i = 0; i < statsButtons.Length;  i++) 
        {
            statsText[i] = statsButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
        SelectChampion(0);
    }

    void Update()
    {
        selectedChampionImageAnimator.SetFloat("Horizontal", rotateChampion);
    }

    public void SelectChampion(int championIndex)
    {
        switch (championIndex)
        {
            case 0:
                PedrinhoSelected();
                break;

            case 1:
                RabicoSelected();
                break;

            case 2:
                CucaSelected();
                break;

            case 3:
                ViscondeSelected();
                break;

            case 4:
                SaciSelected();
                break;

            case 5:
                EmiliaSelected();
                break;

            default:
                PedrinhoSelected();
                break;
        }
    }

    void PedrinhoSelected()
    {
        selectedChampion = championsPrefabs[0].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Pedrinho");
        PlayerPrefs.SetInt("selectedChampion", 0);
        ChampionUpdate(selectedChampion);
        championButtons[0].Select();
    }

    void RabicoSelected()
    {
        selectedChampion = championsPrefabs[1].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Rabico");
        PlayerPrefs.SetInt("selectedChampion", 1);
        ChampionUpdate(selectedChampion) ;
    }
    void CucaSelected()
    {
        selectedChampion = championsPrefabs[2].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Cuca");
        PlayerPrefs.SetInt("selectedChampion", 2);
        ChampionUpdate(selectedChampion);
    }

    void ViscondeSelected()
    {
        selectedChampion = championsPrefabs[3].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Visconde");
        PlayerPrefs.SetInt("selectedChampion", 3);
        ChampionUpdate(selectedChampion);
    }

    void SaciSelected()
    {
        selectedChampion = championsPrefabs[4].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Saci");
        PlayerPrefs.SetInt("selectedChampion", 4);
        ChampionUpdate(selectedChampion);
    }

    void EmiliaSelected()
    {
        selectedChampion = championsPrefabs[5].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger("Emilia");
        PlayerPrefs.SetInt("selectedChampion", 5);
        ChampionUpdate(selectedChampion);
    }

    void ChampionUpdate(Champion selectedChampion)
    {
        //StatsUpdate 
        statsText[0].text = selectedChampion.AttackDamage.ToString();
        statsText[1].text = selectedChampion.MagicDamage.ToString();
        statsText[2].text = selectedChampion.MaxHealth.ToString();
        statsText[3].text = selectedChampion.MaxMana.ToString();
        statsText[4].text = selectedChampion.MaxStamina.ToString();
        statsText[5].text = selectedChampion.AttackResistance.ToString();
        statsText[6].text = selectedChampion.MagicResistance.ToString();
        statsText[7].text = selectedChampion.MoveSpeed.ToString();
        selectedChampionText.text = selectedChampion.ChampionName.ToString();

        for(int i = 0; i < skillsButtons.Length-1; i++)
        {
            if (skillsButtons[i].gameObject.activeSelf) skillsButtons[i].gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Sitio");
    }

    public void ReturnToMenu()
    {
        menuCanvas.SetActive(true);
        championSelectCanvas.SetActive(false);
    }

    public void GoToOptions()
    {
        optionsGame.SetActive(true);
        championSelectCanvas.SetActive(false);
    }
}
