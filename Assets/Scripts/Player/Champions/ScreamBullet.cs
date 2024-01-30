using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamBullet : BasicBullet
{
    [SerializeField] private float slowndallPercentage;
    [SerializeField] private float timeSlowndall;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D != null)
        {
            if (collider2D.gameObject.tag != "Ignore")
            {
                if(collider2D.gameObject.tag == "Champion")
                {
                    if(collider2D.gameObject.GetComponent<Champion>() != Champion)
                    {
                        collider2D.GetComponent<Champion>().TakeDamage(Champion.MagicDamage, Champion.MagicPenetration, 0, 0, false,0, transform.position);
                        collider2D.GetComponent<Champion>().SlowEffect(slowndallPercentage,timeSlowndall);
                        DestroyBullet();
                    }
                }
            }
        }
    }
   
}
