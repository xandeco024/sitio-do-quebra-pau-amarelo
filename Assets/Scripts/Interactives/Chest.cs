using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] itens;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Champion")
        {
            if (gameManager.CurrentPlayer.Interacting)
            {
                Vector3 directionSpawn = 
                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(itens[Random.Range(0, itens.Length)], directionSpawn, transform.rotation);
            }
        }
        
    }
}
