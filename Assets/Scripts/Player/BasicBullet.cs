using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D bulletRB;

    [Header("Stats")]
    private Champion champion;
    [SerializeField] private float bulletSpeed = 1;
    [SerializeField] private float bulletLifetime = 5;

    //Variaveis Acessaveis
    public float BulletSpeed { get { return bulletSpeed; } set {  if(value > 0) bulletSpeed = value; } }
    public float BulletLifetime { get { return bulletLifetime; } set { if (value > 0) bulletLifetime = value; } }
    public Champion Champion { set { champion = value; } }

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
        bulletRB.velocity = transform.right * bulletSpeed;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag != "Ignore")
            {
                if(collision.gameObject.tag == "Champion")
                {
                    collision.GetComponent<Champion>().TakeDamage(champion.AttackDamage, champion.AttackPenetration, 0, 0, false, transform.position);
                }

                DestroyBullet();
            }
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
