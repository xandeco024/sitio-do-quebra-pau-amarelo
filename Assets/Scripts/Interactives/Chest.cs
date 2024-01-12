using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random= UnityEngine.Random;
public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject proximityPrompt;
    [SerializeField] private GameObject[] itens;

    private Transform chestPosition;
    private GameManager gameManager;

    private Champion player;
    private bool playerIsInRange;

    void Start(){
        gameManager = FindObjectOfType<GameManager>();
        player = gameManager.CurrentPlayer;
        chestPosition = GetComponent<Transform>();
        playerIsInRange = false;

        proximityPrompt = Instantiate(proximityPrompt,new Vector2(chestPosition.position.x,chestPosition.position.y + 2),chestPosition.rotation);
        proximityPrompt.SetActive(false);
    }

    void Update(){
        if(playerIsInRange){
            proximityPrompt.SetActive(true);
            if(player.Interacting){
                proximityPrompt.SetActive(false);
                Vector2 spawnPosition = new Vector2(chestPosition.position.x,chestPosition.position.y + 1);
                Instantiate(itens[Random.Range(0,itens.Length)],spawnPosition,chestPosition
                .rotation);
                Destroy(this);
            }
        }
        else
            proximityPrompt.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if(player.gameObject == other.gameObject)
            playerIsInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if(player.gameObject == other.gameObject)
             playerIsInRange = false;
    }
}
