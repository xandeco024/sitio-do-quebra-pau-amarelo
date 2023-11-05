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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.transform.position) < 2)
            Instantiate(itens[Random.Range(0, itens.Length)], transform);
    }
}