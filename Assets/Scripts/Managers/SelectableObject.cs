using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnMouseDown()
    {
        Debug.Log("penis");
        //gameManager.target = gameObject.transform.parent.gameObject;
        //Debug.Log(transform.parent.name + " foi clicado!");
    }
}
