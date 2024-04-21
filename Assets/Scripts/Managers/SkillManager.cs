using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class SkillManager : MonoBehaviour
{
    [SerializeField] private Image[] icons = new Image[3];
    [SerializeField] private Image[] cooldownImages = new Image[3];
    [SerializeField] private TMP_Text[] textCooldowns = new TMP_Text[3];
    [SerializeField] private KeyCode[] keyCodes = new KeyCode[3];
    private float[] cooldownTimers = new float[3];
    private Champion currentPlayer;
       
    private void Start()
    {
        //atribuimos os valores dos icones,cooldowns e skills
        currentPlayer = FindObjectOfType<GameManager>().CurrentPlayer;
        for (int index = 0; index < currentPlayer.Skills.Length; index++)
        {
            icons[index].sprite = currentPlayer.Skills[index].icon;
            currentPlayer.Skills[index].state = Skill.StateSkill.ready;
            cooldownTimers[index] = currentPlayer.Skills[index].cooldownTime;
            cooldownImages[index].fillAmount = 0;
        }
    }

    private void Update()
    {        
        //aqui usamos a função para cada habilidade existente
        SkillStateHandler(0);
        SkillStateHandler(1);
        SkillStateHandler(2);
    }

    private void SkillStateHandler(int index)
    {
        // aqui vemos qual é o estado da habilidade
        switch (currentPlayer.Skills[index].state)
        {
            case Skill.StateSkill.ready:
                //caso a habilidade esteja preparada para ser usada atribuimos as variaveis ao seus padrões
                cooldownTimers[index] = currentPlayer.Skills[index].cooldownTime;
                cooldownImages[index].fillAmount = 0;
                textCooldowns[index].gameObject.SetActive(false);
                
                //se o usuario apertar a tecla definida para esta habilidade ele alternara para o estado ativo
                if (Input.GetKeyDown(keyCodes[index]))
                    currentPlayer.Skills[index].state = Skill.StateSkill.active;
            break;
           // o estado ativo não esta escrito pois é aonde a habilidade ocorre então ele fica nos scripts
           // dos campeões em cada habilidade
        
            case Skill.StateSkill.cooldown:
                // caso esteja em cooldown o tempo de cooldown diminui e a ui muda
                cooldownTimers[index] -= Time.deltaTime;
                cooldownImages[index].fillAmount = cooldownTimers[index] / currentPlayer.Skills[index].cooldownTime;
                textCooldowns[index].gameObject.SetActive(true);
                textCooldowns[index].text = Mathf.RoundToInt(cooldownTimers[index]).ToString();

                //e caso o cooldown chegue a zero ou meno que isso voltamos para o estado ready;
                if (cooldownTimers[index] <= 0)
                    currentPlayer.Skills[index].state = Skill.StateSkill.ready;
            break;
        }
    }
}
