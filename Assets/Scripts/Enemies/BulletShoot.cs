using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;
    private Rigidbody2D gunBullet;
    public float intensity;
    public int shootRatio;
    void Start()
    {
        gunBullet = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        gunBullet.velocity = new Vector2(direction.x, direction.y).normalized * intensity;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0 ,rot + 90);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 10)
        {
            timer += Time.deltaTime;
        }
        if(timer > shootRatio)
        {
            timer = 0;
            Shoot();
        }
        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Champion>().Health -= 5;
            Destroy(gameObject);
        }
    }
        
    

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
