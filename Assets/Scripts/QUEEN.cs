using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        List<Vector2Int> moves = new();
        int x = boardPos.x;
        int y = boardPos.y;

        // ---------- ROOK MOVES (straight) ----------
        // Right
        for (int i = x + 1; i < 8; i++)
            if (!AddMoveIfValid(moves, board, i, y)) break;

        // Left
        for (int i = x - 1; i >= 0; i--)
            if (!AddMoveIfValid(moves, board, i, y)) break;

        // Up
        for (int j = y + 1; j < 8; j++)
            if (!AddMoveIfValid(moves, board, x, j)) break;

        // Down
        for (int j = y - 1; j >= 0; j--)
            if (!AddMoveIfValid(moves, board, x, j)) break;


        // ---------- BISHOP MOVES (diagonal) ----------
        // Top-right
        for (int i = 1; i < 8; i++)
            if (!AddMoveIfValid(moves, board, x + i, y + i)) break;

        // Top-left
        for (int i = 1; i < 8; i++)
            if (!AddMoveIfValid(moves, board, x - i, y + i)) break;

        // Bottom-right
        for (int i = 1; i < 8; i++)
            if (!AddMoveIfValid(moves, board, x + i, y - i)) break;

        // Bottom-left
        for (int i = 1; i < 8; i++)
            if (!AddMoveIfValid(moves, board, x - i, y - i)) break;

        return moves;
    }

    bool AddMoveIfValid(List<Vector2Int> list, Piece[,] board, int x, int y)
    {
        if (!IsInside(x, y)) return false;

        Piece p = board[x, y];

        if (p == null)
        {
            list.Add(new Vector2Int(x, y));
            return true;
        }

        if (p.isWhite != isWhite)
            list.Add(new Vector2Int(x, y));

        return false;
    }
    public override int GetValue() => 9;

}
