using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Slider healthSlider, manaSlider;
    //private Image healthHandleImage, manaHandleImage, staminaHandleImage;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //healthHandleImage = healthSlider.handleRect.GetComponent<Image>();
        //manaHandleImage = manaSlider.handleRect.GetComponent<Image>();
        //staminaHandleImage = staminaHandleImage.GetComponent<Image>();
    }

    void Update()
    {
        HandleSliders();
    }

    void HandleSliders()
    {
        float normalizedHealth = gameManager.CurrentPlayer.Health / gameManager.CurrentPlayer.MaxHealth * 100;
        healthSlider.value = Mathf.Lerp(healthSlider.value, normalizedHealth, Time.deltaTime * 5);

         // Se o valor do controle deslizante for 0, torne a imagem do Handle transparente
        /*if (healthSlider.value == 0)
        {
            healthHandleImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            healthHandleImage.color = new Color(1, 1, 1, 1);
        }*/

        float normalizedMana = gameManager.CurrentPlayer.Mana / gameManager.CurrentPlayer.MaxMana * 100;
        manaSlider.value = Mathf.Lerp(manaSlider.value, normalizedMana, Time.deltaTime * 5);

        /*if (manaSlider.value == 0)
        {
            manaHandleImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            manaHandleImage.color = new Color(1, 1, 1, 1);
        }*/

        /*float normalizedStamina = gameManager.CurrentPlayer.Stamina / gameManager.CurrentPlayer.MaxStamina * 100;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, normalizedStamina, Time.deltaTime * 5);

        if (staminaSlider.value == 0)
        {
            staminaHandleImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            staminaHandleImage.color = new Color(1, 1, 1, 1);
        }*/
    }

}
