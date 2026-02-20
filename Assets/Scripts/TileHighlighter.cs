using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    public static TileHighlighter Instance;
    private GameObject lastMoveTile;

    public GameObject[,] tiles = new GameObject[8, 8];

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterTile(int x, int y, GameObject tile)
    {
        tiles[x, y] = tile;
    }

    // ?? UPDATED: Different colors for moves (yellow) and capture (red)
    public void HighlightTiles(List<Vector2Int> positions, Piece[,] board)
    {
        foreach (Vector2Int pos in positions)
        {
            Piece p = board[pos.x, pos.y];

            if (p != null)
            {
                // Enemy piece ? RED
                tiles[pos.x, pos.y].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                // Normal move ? YELLOW
                tiles[pos.x, pos.y].GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    public void ClearHighlights()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // If last move tile ? restore default first
                if (tiles[x, y] == lastMoveTile)
                {
                    lastMoveTile = null;
                }

                // Reset board colors
                if ((x + y) % 2 == 0)
                    tiles[x, y].GetComponent<Renderer>().material.color = Color.white;
                else
                    tiles[x, y].GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }

    // ?? SAVE THE LAST MOVE (cyan)
    public void MarkLastMove(Vector2Int pos)
    {
        // Reset old tile
        if (lastMoveTile != null)
        {
            Vector2Int oldPos = GetTilePosition(lastMoveTile);

            if ((oldPos.x + oldPos.y) % 2 == 0)
                lastMoveTile.GetComponent<Renderer>().material.color = Color.white;
            else
                lastMoveTile.GetComponent<Renderer>().material.color = Color.black;
        }

        // Mark new tile
        lastMoveTile = tiles[pos.x, pos.y];
        lastMoveTile.GetComponent<Renderer>().material.color = Color.cyan;
    }

    public Vector2Int GetTilePosition(GameObject tile)
    {
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                if (tiles[x, y] == tile)
                    return new Vector2Int(x, y);

        return Vector2Int.zero;
    }
}
