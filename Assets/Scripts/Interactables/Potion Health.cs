using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Champion")
        { 
            if (collision.gameObject.GetComponent<Champion>() == gameManager.CurrentPlayer)
            {
                print("oi");
                collision.gameObject.GetComponent<Champion>().Health += 50;
                Destroy(this);
            }
        }
    }
}
