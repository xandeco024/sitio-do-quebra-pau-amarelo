using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoButton : MonoBehaviour
{
    private GameManager gameManager;
    private Emilia emilia;
    public AudioSource audioSource;
    [SerializeField] private float magicDamage;
    [SerializeField] private float magicPenetration;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        emilia = gameManager.CurrentPlayer.GetComponent<Emilia>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == emilia.gameObject && emilia.yoyoReturn)
        {
            emilia.yoyoSkill.state = Skill.StateSkill.cooldown;
            Destroy(gameObject);
        }

        else if (other.gameObject != emilia.gameObject && other.gameObject.GetComponent<Champion>() != null)
        {
            other.gameObject.GetComponent<Champion>().
                TakeDamage(0, 0, emilia.MagicDamage + magicDamage, magicPenetration, false, 0, transform.position);
        }
    }
}

