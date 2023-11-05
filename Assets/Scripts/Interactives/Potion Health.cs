using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Champion")
        {
            if (collision.gameObject.GetComponent<Champion>().player == true)
                collision.gameObject.GetComponent<Champion>().Health += 50;
        }
    }
}
