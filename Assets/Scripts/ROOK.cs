using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        List<Vector2Int> moves = new();

        int x = boardPos.x;
        int y = boardPos.y;

        // Right
        for (int i = x + 1; i < 8; i++)
        {
            if (!AddMoveIfValid(moves, board, i, y)) break;
        }

        // Left
        for (int i = x - 1; i >= 0; i--)
        {
            if (!AddMoveIfValid(moves, board, i, y)) break;
        }

        // Up
        for (int j = y + 1; j < 8; j++)
        {
            if (!AddMoveIfValid(moves, board, x, j)) break;
        }

        // Down
        for (int j = y - 1; j >= 0; j--)
        {
            if (!AddMoveIfValid(moves, board, x, j)) break;
        }

        return moves;
    }

    bool AddMoveIfValid(List<Vector2Int> list, Piece[,] board, int x, int y)
    {
        Piece p = board[x, y];

        if (p == null)
        {
            list.Add(new Vector2Int(x, y));
            return true; // continue
        }

        if (p.isWhite != isWhite)
            list.Add(new Vector2Int(x, y));

        return false; // stop
    }
    public override int GetValue() => 5;

}
