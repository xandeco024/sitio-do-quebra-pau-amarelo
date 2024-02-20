using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetationManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // O Tilemap que você quer modificar
    [SerializeField] private TileBase targetTile; // O Tile que você quer substituir
    [SerializeField] private TileBase newTile; // O Tile que você quer usar para substituir
    [SerializeField] private float chanceToChange; // A chance de um Tile ser substituído

    private int notNullTileStreak = 0;
    List<Vector3> clusterTiles = new List<Vector3>();

    void Start()
    {
        // Percorre cada tile do tilemap
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            // Se o tile atual é o tile alvo
            if (tilemap.GetTile(localPlace) != null && tilemap.GetTile(localPlace) == targetTile)
            {
                notNullTileStreak++;

                if (notNullTileStreak > 5)
                {
                    // Se um número aleatório for menor que a chance de mudar
                    if (Random.Range(0f, 1f) < chanceToChange)
                    {
                        // Substitui o tile
                        clusterTiles.Add(localPlace);
                        tilemap.SetTile(localPlace, newTile);
                        notNullTileStreak = 0;
                    }
                }
            }

            else
            {
                Debug.Log("quebrou o streak de " + notNullTileStreak + "  o tile " + localPlace);
                notNullTileStreak = 0;
            }
        }
    }
}
