using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera miniMapCamera;
    private GameObject target;
    private Champion currentPlayer;

    // Variavies Acessiveis
    public GameObject Target { get { return target; } }
    public Champion CurrentPlayer { get {  return currentPlayer; } }

    private int SelectedChampion;
    private Vector2 spawnPoint = new Vector2(14, 10);

    [SerializeField] private GameObject[] championPrefabList;
    [SerializeField] private Champion[] sceneChampionList;


    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("selectedChampion"));
        miniMapCamera.transform.parent = currentPlayer.gameObject.transform;
        GameObject currentPlayerObject = Instantiate(championPrefabList[PlayerPrefs.GetInt("selectedChampion")], spawnPoint, Quaternion.identity);
        currentPlayerObject.GetComponent<Champion>().SetPlayer(true);
        currentPlayer = currentPlayerObject.GetComponent<Champion>();
    }

    void Start()
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (virtualCamera != null && currentPlayer != null)
        {
            virtualCamera.Follow = currentPlayer.transform;
        }
    }

    void Update()
    {
        TargetHandler();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("penis");
            PlayerPrefs.SetInt("selectedChampion", 1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            PlayerPrefs.SetInt("selectedChampion", 2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            Debug.Log(PlayerPrefs.GetInt("selectedChampion"));
        }
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
