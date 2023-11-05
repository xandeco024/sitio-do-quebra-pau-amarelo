 using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Emilia : Champion
{
    [Header("Annoy Power")]
    [SerializeField] private float radiusCircleAnnoy;
    [SerializeField] private float cooldownAnnoyPower;
    [SerializeField] private float cooldownAnnoyEnemy;

    [Header("Summon Guardian Power")]
    [SerializeField] private float cooldownSummonGuardian;
    [SerializeField] private GameObject guardian;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (player)
        {
            base.Update();
            //AnnoyPower();
            //PowerGuardianImaginary();
        }
    }

    
    protected override void FixedUpdate()
    {
        if(player)
        {
            base.FixedUpdate();
        }
    }
  
    /*void AnnoyPower()
    {
        float damageInterval = 1.0f;
        float lastDamageTime = 0;
        cooldownAnnoyPower -= (cooldownAnnoyPower > 0) ? Time.deltaTime : 0;
        cooldownAnnoyEnemy -= (cooldownAnnoyEnemy > 0) ? Time.deltaTime : 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radiusCircleAnnoy);

        if (Input.GetKeyDown(KeyCode.Z) && cooldownAnnoyPower == 0)
        {
            cooldownAnnoyPower += 30;
            cooldownAnnoyEnemy += 20;
            moveSpeed = 0;
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<TiaNastacia>() != null)
                {
                    enemy.GetComponent<TiaNastacia>().moveSpeed = 0;
                    enemy.GetComponent<TiaNastacia>()
                        .TakeDamage(0, 0, magicDamage, magicPenetration, false, enemy.transform.position);
                }
            }
        }
        if (cooldownAnnoyEnemy > 0 && Time.time - lastDamageTime >= damageInterval)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy != null && enemy.GetComponent<TiaNastacia>() != null)
                {
                    if (health > 0)
                    {
                        enemy.GetComponent<TiaNastacia>()
                            .TakeDamage(0, 0, magicDamage, magicPenetration, false, enemy.transform.position);
                    }
                    else
                        Destroy(enemy);
                }
            }
            lastDamageTime = Time.time;
        }

        if (cooldownAnnoyEnemy <= 0)
        {
            moveSpeed = 5;
            foreach (Collider2D enemy in hitEnemies)
            {

                if (enemy != null && enemy.GetComponent<TiaNastacia>() != null )
                    enemy.GetComponent<TiaNastacia>().moveSpeed = 3;
            }

        }
        if (cooldownAnnoyPower < 0)
            cooldownAnnoyPower = 0;

        if (cooldownAnnoyEnemy < 0)
            cooldownAnnoyEnemy = 0;
    }
  
    void PowerGuardianImaginary()
    {
        cooldownSummonGuardian -= (cooldownSummonGuardian > 0) ? Time.deltaTime : 0;
        if (Input.GetKeyDown(KeyCode.X) && cooldownSummonGuardian == 0)
        {
            cooldownSummonGuardian += 50;
            Instantiate(guardian, transform.position, transform.rotation);
        }
    }

    
    void IA()
    {
        // boa sorte.
    }*/
}
