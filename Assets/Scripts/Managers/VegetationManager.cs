using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetationManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // O Tilemap que voc� quer modificar
    [SerializeField] private TileBase targetTile; // O Tile que voc� quer substituir
    [SerializeField] private TileBase newTile; // O Tile que voc� quer usar para substituir
    [SerializeField] private float chanceToChange; // A chance de um Tile ser substitu�do

    void Start()
    {
        // Percorre cada tile do tilemap
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            // Se o tile atual � o tile alvo
            if (tilemap.GetTile(localPlace) == targetTile)
            {
                // Se um n�mero aleat�rio for menor que a chance de mudar
                if (Random.Range(0f, 1f) < chanceToChange)
                {
                    // Substitui o tile
                    tilemap.SetTile(localPlace, newTile);
                }
            }
        }
    }
}
