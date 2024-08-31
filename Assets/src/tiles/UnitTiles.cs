

using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitTiles : MonoBehaviour
{

    public Tile empty, enemy, ally;

    public Tilemap terrain, unitTiles;


    public void Awake()
    {
        terrain.CompressBounds();
        for (int x = terrain.cellBounds.xMin; x < terrain.cellBounds.xMax; x++)
            for (int y = terrain.cellBounds.yMin; y < terrain.cellBounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                if (terrain.GetTile(pos) == null) unitTiles.SetTile(pos, null);
                else unitTiles.SetTile(pos, empty);
            }

        UpdateUnits();

    }
    private List<Vector3Int> previousUnitCoords = new List<Vector3Int>();
    public void UpdateUnits()
    {
        // Clear old unit tiles
        foreach (Vector3Int pos in previousUnitCoords)
            unitTiles.SetTile(pos, empty);
        previousUnitCoords.Clear();

        // Set new unit tiles
        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            Vector3Int pos = unitTiles.WorldToCell(u.transform.position);
            previousUnitCoords.Add(pos);

            if (u.locallyControlled) unitTiles.SetTile(pos, ally);
            else unitTiles.SetTile(pos, enemy);
        }
    }



}