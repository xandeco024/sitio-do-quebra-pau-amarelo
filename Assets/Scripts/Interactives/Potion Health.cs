using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    GameManager gameManager;
    void Start(){
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Champion")
        {
            if(gameManager.CurrentPlayer.gameObject == collision.gameObject)
            {
                if(gameManager.CurrentPlayer.Health + 50 < gameManager.CurrentPlayer.MaxHealth){    
                    gameManager.CurrentPlayer.Health += 50;
                    Destroy(gameObject);
                }

                else if(gameManager.CurrentPlayer.Health == gameManager.CurrentPlayer.MaxHealth)
                    return;
                
                else if(gameManager.CurrentPlayer.Health + 50 > gameManager.CurrentPlayer.MaxHealth){
                    gameManager.CurrentPlayer.Health = gameManager.CurrentPlayer.MaxHealth;
                    Destroy(gameObject);
                }
            }
        }
    }
}
