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
    [SerializeField] private Sprite bulletSprite;
    private SpriteRenderer bulletSR;

    //Variaveis Acessaveis
    public float BulletSpeed { get { return bulletSpeed; } set {  if(value > 0) bulletSpeed = value; } }
    public float BulletLifetime { get { return bulletLifetime; } set { if (value > 0) bulletLifetime = value; } }
    public Champion Champion { get {return champion;} set { champion = value; } }
    public Sprite BulletSprite { set { bulletSprite = value; } }

    protected void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        champion = GameObject.FindObjectOfType<GameManager>().CurrentPlayer;
    }

    protected void Start()
    {
        Destroy(gameObject, bulletLifetime);
        bulletRB.velocity = transform.right * bulletSpeed;

        bulletSR = GetComponent<SpriteRenderer>();
        bulletSR.sprite = bulletSprite;
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
                    if(collision.gameObject.GetComponent<Champion>() != champion)
                    {
                        collision.GetComponent<Champion>().TakeDamage(champion.AttackDamage, champion.AttackPenetration, 0, 0, false,0, transform.position);
                        DestroyBullet();
                    }
                }
            }
        }
    }

    protected void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
