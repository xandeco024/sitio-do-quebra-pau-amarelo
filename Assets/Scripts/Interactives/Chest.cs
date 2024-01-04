using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random= UnityEngine.Random;
public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] itens;
    private Transform chestPosition;
    private GameManager gameManager;
    void Start(){
        gameManager = FindObjectOfType<GameManager>();
        chestPosition = GetComponent<Transform>();
    }
    void OnTriggerStay2D(Collider2D other)
    {   
        if(gameManager.CurrentPlayer.Interacting){
            Vector2 spawnPosition = new Vector2(chestPosition.position.x,chestPosition.position.y + 1);

            Instantiate(itens[Random.Range(0,itens.Length)],spawnPosition,chestPosition
            .rotation);
            Destroy(this);
        }
    }
}
