using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Champion")
        {
            if(FindObjectOfType<GameManager>().CurrentPlayer.gameObject == collision.gameObject)
            {
                FindObjectOfType<GameManager>().CurrentPlayer.Health += 50;
                Destroy(this);
            }
        }
    }
}
