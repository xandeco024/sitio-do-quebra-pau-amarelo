using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private Champion currentPlayer;
    // Variavies Acessiveis
    public Champion CurrentPlayer { get { return currentPlayer; } }
    public List<Champion> ChampionsList { get { return championsList; } }

    private Vector2 spawnPoint = new Vector2(14, 10);

    private List<Champion> championsList;

    [SerializeField] private GameObject[] championPrefabList;
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] private GameObject iconMiniMap;
    [SerializeField] private GameUIManager gameUIManager;

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private int enemyCount;

    private void Awake()
    {

        GameObject currentPlayerObject = Instantiate(championPrefabList[PlayerPrefs.GetInt("selectedChampion")], spawnPoint, Quaternion.identity);
        currentPlayerObject.GetComponent<Champion>().SetPlayer(true);
        currentPlayer = currentPlayerObject.GetComponent<Champion>();

        miniMapCamera.transform.parent = currentPlayer.transform;
        miniMapCamera.transform.position = new Vector3(
            currentPlayer.transform.position.x,
            currentPlayer.transform.position.y, -19);
        iconMiniMap.transform.parent = currentPlayer.transform;
    }

    void Start()
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (virtualCamera != null && currentPlayer != null)
        {
            virtualCamera.Follow = currentPlayer.transform;
        }

        Champion[] championsArray = GameObject.FindObjectsOfType<Champion>();
        championsList = new List<Champion>(championsArray);

        SpawnEnemies();
    }

    void Update()
    {
        iconMiniMap.transform.position = currentPlayer.transform.position;

        Champion[] championsArray = GameObject.FindObjectsOfType<Champion>();
        championsList = new List<Champion>(championsArray);

        if (currentPlayer.IsDead)
        {
            Debug.Log("Game over");
            gameUIManager.HandleGameOver();
        }

        else if (championsList.Count == 1)
        {
            if (championsList[0] == currentPlayer)
            {
                Debug.Log("Win");
                gameUIManager.HandleWin();
            }
        }
    }

    void SpawnEnemies()
    {
        List<Vector3> possiblePositions = new List<Vector3>();

        // Primeiro, coletamos todas as posições possíveis
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);

                if (tileMap.HasTile(localPlace))
                {
                    possiblePositions.Add(place);
                }
            }
        }

        // Em seguida, geramos os inimigos em posições aleatórias
        for (int i = 0; i < enemyCount; i++)
        {
            if (possiblePositions.Count > 0)
            {
                int randomIndex = Random.Range(0, possiblePositions.Count);
                Vector3 spawnPosition = possiblePositions[randomIndex];
                possiblePositions.RemoveAt(randomIndex); // Removemos a posição para não gerar dois inimigos no mesmo lugar

                Instantiate(championPrefabList[Random.Range(0, championPrefabList.Length)], spawnPosition, Quaternion.identity);
            }
        }

    }
}
