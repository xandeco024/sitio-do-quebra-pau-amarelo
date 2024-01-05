using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Image targetImage;
    [SerializeField] private Slider targetSlider;
    private float attackCDElapsedTime;
    [SerializeField] private Slider healthSlider, manaSlider, staminaSlider;
    [SerializeField] private TextMeshProUGUI remainingPlayersText;

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
        remainingPlayersText.text = gameManager.ChampionsList.Count.ToString();
        HandleSliders();
        HandleAim();
    }

    void HandleSliders()
    {
        print(gameManager.CurrentPlayer);
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

        float normalizedStamina = gameManager.CurrentPlayer.Stamina / gameManager.CurrentPlayer.MaxStamina * 100;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, normalizedStamina, Time.deltaTime * 5);

        /*
        if (staminaSlider.value == 0)
        {
            staminaHandleImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            staminaHandleImage.color = new Color(1, 1, 1, 1);
        }*/
    }

    void HandleAim()
    {
        // Converte a posição do personagem de espaço do mundo para espaço da tela
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(gameManager.CurrentPlayer.transform.position);

        // Converte a posição do mouse de espaço da tela para espaço do mundo
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcula a direção do mouse em relação ao personagem
        Vector2 direction = (mouseWorldPosition - (Vector2)gameManager.CurrentPlayer.transform.position).normalized;

        // Calcula a posição da mira em um raio ao redor do personagem na direção do mouse
        Vector2 targetWorldPosition = (Vector2)gameManager.CurrentPlayer.transform.position + direction * Mathf.Min(gameManager.CurrentPlayer.AttackRange, Vector2.Distance(gameManager.CurrentPlayer.transform.position, mouseWorldPosition));

        // Converte a posição da mira de espaço do mundo para espaço da tela
        Vector2 targetScreenPosition = Camera.main.WorldToScreenPoint(targetWorldPosition);

        // Atualiza a posição da mira na UI
        targetImage.transform.position = targetScreenPosition;


        if (!gameManager.CurrentPlayer.CanAttack)
        {
            targetImage.color = new Color(1, 1, 1, 0.5f) ;
            if (!targetSlider.gameObject.activeSelf) targetSlider.gameObject.SetActive(true);

            attackCDElapsedTime += Time.deltaTime;
            targetSlider.value = attackCDElapsedTime / gameManager.CurrentPlayer.AttackSpeed;
            if (attackCDElapsedTime >= gameManager.CurrentPlayer.AttackSpeed) attackCDElapsedTime = 0;
        }

        else
        {
            targetImage.color = new Color(1, 1, 1, 1);
            if(targetSlider.gameObject.activeSelf) targetSlider.gameObject.SetActive(false);
        }
    }
}
