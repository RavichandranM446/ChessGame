using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> GetAvailableMoves(Piece[,] board)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        int dir = isWhite ? 1 : -1;

        int x = boardPos.x;
        int y = boardPos.y;

        // Move forward 1
        if (IsInside(x, y + dir) && board[x, y + dir] == null)
            moves.Add(new Vector2Int(x, y + dir));

        // Move forward 2 (only starting row)
        if (isWhite && y == 1 && board[x, y + 1] == null && board[x, y + 2] == null)
            moves.Add(new Vector2Int(x, y + 2));

        if (!isWhite && y == 6 && board[x, y - 1] == null && board[x, y - 2] == null)
            moves.Add(new Vector2Int(x, y - 2));

        // Capture left
        if (IsInside(x - 1, y + dir))
        {
            Piece left = board[x - 1, y + dir];
            if (left != null && left.isWhite != isWhite)
                moves.Add(new Vector2Int(x - 1, y + dir));
        }

        // Capture right
        if (IsInside(x + 1, y + dir))
        {
            Piece right = board[x + 1, y + dir];
            if (right != null && right.isWhite != isWhite)
                moves.Add(new Vector2Int(x + 1, y + dir));
        }

        return moves;
    }
    public override int GetValue() => 1;

}
