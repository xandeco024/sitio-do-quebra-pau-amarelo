using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuca : Champion
{


    [Header("Poison Attack")]
    [SerializeField] private float radiusPoisounAttack;

    [Header("Habilities")]
    float cooldownInimig = 0;
    float cooldownPoisounAttack = 0;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(isPlayer)
        {
            base.Update();
            //PoisonousFood();
        }

        else
        {
            BasicIA();
        }
    }

    protected override void FixedUpdate()
    {
        if(isPlayer)
        {
            base.FixedUpdate();
        }
    }

    /*void PoisonousFood()
    {
        float damageInterval = 1.0f;
        float lastDamageTime = 0;

        bool canAttack = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radiusPoisounAttack);

        cooldownInimig -= (canAttack && cooldownInimig > 0) ? Time.deltaTime : 0;
        cooldownPoisounAttack-= (canAttack && cooldownPoisounAttack > 0) ? Time.deltaTime : 0;

        if (Input.GetKeyDown(KeyCode.Z) && canAttack)
        {
            cooldownPoisounAttack += 40;
            cooldownInimig += 10;
          
            foreach (Collider2D hitEnemy in hitEnemies)
            {
                hitEnemy.GetComponent<Emilia>().moveSpeed = 0;
                hitEnemy.GetComponent<Emilia>()
                       .TakeDamage(0, 0, magicDamage, magicPenetration, false, hitEnemy.transform.position);
                StartCoroutine(ContinuousDamage(hitEnemy));
            }
        }
        if (cooldownInimig > 0 && Time.time - lastDamageTime >= damageInterval)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy != null && enemy.GetComponent<Emilia>() != null)
                {
                    if (health > 0)
                    {
                        enemy.GetComponent<Emilia>()
                            .TakeDamage(0, 0, magicDamage, magicPenetration, false, enemy.transform.position);
                    }
                    else
                        Destroy(enemy);
                }
            }
            lastDamageTime = Time.time;
        }

        if (cooldownInimig <= 0)
        {
            foreach (Collider2D enemy in hitEnemies) {
                if (enemy != null && enemy.GetComponent<TiaNastacia>() != null)
                {
                    enemy.GetComponent<Emilia>().moveSpeed = 3;
                }
            }
        }
    }

    IEnumerator ContinuousDamage(Collider2D enemy)
    {
        float finalTime = Time.time + 20f; 
        while (Time.time < finalTime)
        {
            enemy.GetComponent<TiaNastacia>()
                   .TakeDamage(0, 0, magicDamage, magicPenetration, false, enemy.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }

    void IA ()
    {
        championRB.velocity = new Vector2(moveSpeed, 0);
    }*/
}
