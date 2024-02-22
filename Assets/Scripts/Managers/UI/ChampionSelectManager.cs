using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChampionSelectManager : MonoBehaviour
{
    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI titleShopChampionText;
    [SerializeField] private TextMeshProUGUI selectedChampionText;
    [SerializeField] private TextMeshProUGUI priceChampionSelectText;
    [SerializeField] private TextMeshProUGUI coinsHaveBuyingPanelText;
    [SerializeField] private TextMeshProUGUI priceChampionShoppingText;
    [SerializeField] private TextMeshProUGUI coinsRemainingText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [Header("Components")]
    [SerializeField] private Animator selectedChampionImageAnimator;

    [Header("Panels")]
    [SerializeField] private GameObject championBuyingPanel;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button openShopChampionButton;
    [SerializeField] private Button[] championButtons;
    [SerializeField] private Button[] statsButtons;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] GameObject[] championsPrefabs;

    [Header("Canvas")]
    [SerializeField] GameObject menuCanvas, championSelectCanvas, optionsGame;

    [Header("Lists")]
    private List<string> championsNames = new List<string>();

    [Header("Variables")]
    private Champion selectedChampion;
    private TextMeshProUGUI[] statsText;


    [SerializeField] private TMP_InputField nicknameInputField;

    void Start()
    {
        championsNames.Add("Pedrinho");
        championsNames.Add("Rabico");
        championsNames.Add("Cuca");
        championsNames.Add("Visconde");
        championsNames.Add("Saci");
        championsNames.Add("Emilia");

        statsText = new TextMeshProUGUI[statsButtons.Length];
        for (int i = 0; i < statsButtons.Length;  i++) 
        {
            statsText[i] = statsButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        SelectChampion(0);
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("nickname")))
        {
            nicknameInputField.text = PlayerPrefs.GetString("nickname");
        }
    }

    void Update()
    {
        titleShopChampionText.text = (PlayerPrefs.GetInt("coins") >= selectedChampion.Price)
        ?"Você tem certeza que quer comprar o (a)" + selectedChampion.ChampionName :
        "Você não tem moedas o suficiente para comprar o (a)" + selectedChampion.ChampionName;;

        HandleSpriteButtonsChampions();
        SetCoinsText();
    }

    private void HandleSpriteButtonsChampions(){
        championButtons[0].image.sprite = (championsPrefabs[0].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[0].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[0].GetComponent<Champion>().imageButtonChampionLocked;

        championButtons[1].image.sprite = (championsPrefabs[1].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[1].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[1].GetComponent<Champion>().imageButtonChampionLocked;

        championButtons[2].image.sprite = (championsPrefabs[2].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[2].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[2].GetComponent<Champion>().imageButtonChampionLocked;

        championButtons[3].image.sprite = (championsPrefabs[3].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[3].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[3].GetComponent<Champion>().imageButtonChampionLocked;

        championButtons[4].image.sprite = (championsPrefabs[4].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[4].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[4].GetComponent<Champion>().imageButtonChampionLocked;

        championButtons[5].image.sprite = (championsPrefabs[5].GetComponent<Champion>().Purchased == 1)?
        championsPrefabs[5].GetComponent<Champion>().imageButtonChampionUnlocked : championsPrefabs[5].GetComponent<Champion>().imageButtonChampionLocked;
    }

    public void SelectChampion(int championIndex){
        selectedChampion = championsPrefabs[championIndex].GetComponent<Champion>();
        selectedChampionImageAnimator.SetTrigger(championsNames[championIndex]);
        PlayerPrefs.SetInt("selectedChampion", championIndex);
        ChampionUpdate(selectedChampion);

        //print(selectedChampion.Purchased);
        if(selectedChampion.Purchased == 0)
        {

            if(playButton.gameObject.activeSelf) playButton.gameObject.SetActive(false);
            if(!buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(true);

            priceChampionSelectText.text = "" + selectedChampion.Price;
            priceChampionSelectText.gameObject.SetActive(true);
            openShopChampionButton.gameObject.SetActive(true); 
        }

        else if(selectedChampion.Purchased == 1)
        {

            if (!playButton.gameObject.activeSelf) playButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

            priceChampionSelectText.gameObject.SetActive(false);
            openShopChampionButton.gameObject.SetActive(false); 
        }
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

        for(int i = 0; i < skillButtons.Length-1; i++)
        {
            if (skillButtons[i].gameObject.activeSelf) skillButtons[i].gameObject.SetActive(false);
        }
    }

    public void ChampionBuyingPanel(){
        if(selectedChampion.Purchased == 0){
            int coinsHave = PlayerPrefs.GetInt("coins");
            int priceChampion = selectedChampion.Price;
            int coinsRemaining = coinsHave - priceChampion;

            
            coinsHaveBuyingPanelText.text = "" + coinsHave;
            priceChampionShoppingText.text = "" + priceChampion;
            coinsRemainingText.text = "" + coinsRemaining;

            if(selectedChampion != null)
                buyButton.gameObject.SetActive(true);
            
            else if(selectedChampion != null && coinsHave < priceChampion)
                buyButton.gameObject.SetActive(false);
        }
    }

    public void BuyChampion(){
        int coins = PlayerPrefs.GetInt("coins");
        int championPrice = selectedChampion.Price;
        int remainingCoins = coins - championPrice;

        if(coins >= championPrice){
            PlayerPrefs.SetInt("coins",remainingCoins); 
            selectedChampion.Purchased = 1; 

            PlayerPrefs.SetInt(selectedChampion.ChampionName + ":purchased",selectedChampion.Purchased);
            championBuyingPanel.SetActive(false);

            /*
            priceChampionSelectText.gameObject.SetActive(false);
            openShopChampionButton.gameObject.SetActive(false); 
            playButton.interactable = true;
            */
        }
    }

    public void StartGame()
    {
        if(selectedChampion.Purchased == 1)
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

    public void SetNickname(string nickname)
    {
        PlayerPrefs.SetString("nickname", nickname);
        Debug.Log("Setou " + nickname);
    }

    private void SetCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt("coins").ToString();
    }
}
