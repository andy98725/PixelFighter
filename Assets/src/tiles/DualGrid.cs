using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TileType;

public class DualGrid : MonoBehaviour
{
    protected static Vector3Int[] NEIGHBOURS = new Vector3Int[] {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0)
    };


    public Tilemap map, display;

    public Tile castleTile;
    public Tile[] castleDisplayTiles;
    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> castleTileRule;
    public void Awake()
    {
        // This dictionary stores the "rules", each 4-neighbour configuration corresponds to a tile
        // |_1_|_2_|
        // |_3_|_4_|
        castleTileRule = new() {
            {new (Castle, Castle, Castle, Castle), castleDisplayTiles[6]},
            {new (None, None, None, Castle), castleDisplayTiles[13]}, // OUTER_BOTTOM_RIGHT
            {new (None, None, Castle, None), castleDisplayTiles[0]}, // OUTER_BOTTOM_LEFT
            {new (None, Castle, None, None), castleDisplayTiles[8]}, // OUTER_TOP_RIGHT
            {new (Castle, None, None, None), castleDisplayTiles[15]}, // OUTER_TOP_LEFT
            {new (None, Castle, None, Castle), castleDisplayTiles[1]}, // EDGE_RIGHT
            {new (Castle, None, Castle, None), castleDisplayTiles[11]}, // EDGE_LEFT
            {new (None, None, Castle, Castle), castleDisplayTiles[3]}, // EDGE_BOTTOM
            {new (Castle, Castle, None, None), castleDisplayTiles[9]}, // EDGE_TOP
            {new (None, Castle, Castle, Castle), castleDisplayTiles[5]}, // INNER_BOTTOM_RIGHT
            {new (Castle, None, Castle, Castle), castleDisplayTiles[2]}, // INNER_BOTTOM_LEFT
            {new (Castle, Castle, None, Castle), castleDisplayTiles[10]}, // INNER_TOP_RIGHT
            {new (Castle, Castle, Castle, None), castleDisplayTiles[7]}, // INNER_TOP_LEFT
            {new (None, Castle, Castle, None), castleDisplayTiles[14]}, // DUAL_UP_RIGHT
            {new (Castle, None, None, Castle), castleDisplayTiles[4]}, // DUAL_DOWN_RIGHT
            {new (None, None, None, None), castleDisplayTiles[12]},
        };

        // Hide base
        map.GetComponent<TilemapRenderer>().enabled = false;

        RefreshDisplay();
    }
    public void SetCell(Vector3Int coords, Tile tile)
    {
        map.SetTile(coords, tile);
        RefreshTile(coords);
    }

    protected void RefreshDisplay()
    {
        for (int x = map.cellBounds.xMin; x < map.cellBounds.xMax; x++)
            for (int y = map.cellBounds.yMin; y < map.cellBounds.yMax; y++)
            {
                RefreshTile(new Vector3Int(x, y));

            }
    }

    protected void RefreshTile(Vector3Int pos)
    {
        for (int i = 0; i < NEIGHBOURS.Length; i++)
        {
            Vector3Int newPos = pos + NEIGHBOURS[i];
            display.SetTile(newPos, calculateDisplayTile(newPos));
        }
    }

    protected Tile calculateDisplayTile(Vector3Int coords)
    {
        TileType topRight = tileAt(coords - NEIGHBOURS[0]);
        TileType topLeft = tileAt(coords - NEIGHBOURS[1]);
        TileType botRight = tileAt(coords - NEIGHBOURS[2]);
        TileType botLeft = tileAt(coords - NEIGHBOURS[3]);

        Tuple<TileType, TileType, TileType, TileType> neighbourTuple = new(topLeft, topRight, botLeft, botRight);

        return castleTileRule[neighbourTuple];
    }
    private TileType tileAt(Vector3Int coords)
    {
        if (map.GetTile(coords) == castleTile)
            return Castle;
        else
            return None;
    }
}


public enum TileType
{
    None,
    Castle
}