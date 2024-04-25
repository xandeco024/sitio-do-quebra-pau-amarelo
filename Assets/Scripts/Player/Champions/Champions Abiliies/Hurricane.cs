using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    [SerializeField] private float attractionAreaRadius;
    [SerializeField] private float damageAreaRadius;
    [SerializeField] private float forceOfAttractionScale;
    [SerializeField] private float hurricaneLifeTime;
    [SerializeField] private Skill hurricaneSkill;
    private Champion currentPlayer;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentPlayer = gameManager.CurrentPlayer;
    }

    private void Update()
    {
        Destroy(gameObject, hurricaneLifeTime);
    }

    private void FixedUpdate()
    {
        AttractionArea();
        DamageArea();
    }

    private void AttractionArea()
    {
        Collider2D[] attractedEnemies = Physics2D.OverlapCircleAll(transform.position, attractionAreaRadius);

        foreach (Collider2D atractedEnemy in attractedEnemies)
        {
            if (atractedEnemy.GetComponent<Champion>() && atractedEnemy.GetComponent<Champion>() != currentPlayer)
            {
                float distance = Vector2.Distance(transform.position, atractedEnemy.transform.position);
                float forceAttraction = forceOfAttractionScale / distance;
                Vector2 direction = transform.position - atractedEnemy.transform.position;
                atractedEnemy.GetComponent<Rigidbody2D>().AddForce(direction * forceAttraction, ForceMode2D.Force);
            }
        } 
    }
    private void DamageArea()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);
        foreach(Collider2D hitEnemy in hitEnemies) 
        {
            if(hitEnemy.GetComponent<Champion>() && hitEnemy.GetComponent<Champion>() != currentPlayer)
                hitEnemy.GetComponent<Champion>().TakeDamage(0, 0, hurricaneSkill.magicDamage, currentPlayer.MagicPenetration,false,0,Vector2.zero);
        }
    }
}
