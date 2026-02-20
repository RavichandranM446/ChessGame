using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        List<Vector2Int> moves = new();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = boardPos.x + dx;
                int ny = boardPos.y + dy;

                if (!IsInside(nx, ny)) continue;

                Piece p = board[nx, ny];

                if (p == null || p.isWhite != isWhite)
                    moves.Add(new Vector2Int(nx, ny));
            }
        }

        return moves;
    }
    public override int GetValue() => 100;

}
