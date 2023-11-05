using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject target;
    private Champion currentPlayer;

    // Variavies Acessiveis
    public GameObject Target { get { return target; } }
    public Champion CurrentPlayer { get {  return currentPlayer; } }


    private void Awake()
    {
        List<Champion> champions = new List<Champion>(FindObjectsOfType<Champion>());

        foreach (Champion champion in champions)
        {
            if (champion.player)
            {
                currentPlayer = champion;
                break;
            }
        }
    }

    void Start()
    {
        List<Champion> champions = new List<Champion>(FindObjectsOfType<Champion>());

        foreach (Champion champion in champions)
        {
            if (champion.player)
            {
                currentPlayer = champion;
                break;
            }
        }

        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (virtualCamera != null && currentPlayer != null)
        {
            virtualCamera.Follow = currentPlayer.transform;
        }
    }

    void Update()
    {
        TargetHandler();
    }

    void TargetHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = 1 << LayerMask.NameToLayer("Champion");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
            }
            else
            {
                target = null;
            }
        }
    }
}
