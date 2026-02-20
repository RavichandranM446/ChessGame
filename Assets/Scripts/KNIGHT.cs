using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private static readonly int[,] moveset =
    {
        { 1, 2 }, { 2, 1 }, { -1, 2 }, { -2, 1 },
        { 1, -2 }, { 2, -1 }, { -1, -2 }, { -2, -1 }
    };

    public override List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        List<Vector2Int> moves = new();

        for (int i = 0; i < moveset.GetLength(0); i++)
        {
            int nx = boardPos.x + moveset[i, 0];
            int ny = boardPos.y + moveset[i, 1];

            if (!IsInside(nx, ny)) continue;

            Piece p = board[nx, ny];

            if (p == null || p.isWhite != isWhite)
                moves.Add(new Vector2Int(nx, ny));
        }

        return moves;
    }
    public override int GetValue() => 3;

}
