using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] itens;
    private Champion player;

    void Start()
    {
        player = FindObjectOfType<GameManager>().CurrentPlayer;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Champion")
        {
            Vector3 directionInstantiate = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            if(collision.gameObject.GetComponent<Champion>().Interacting)
                Instantiate(itens[Random.Range(0, itens.Length)], directionInstantiate,transform.rotation);
        }
    }
}
