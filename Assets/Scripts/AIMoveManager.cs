using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveManager : MonoBehaviour
{
    public static AIMoveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAI()
    {
        StartCoroutine(DoAIMove());
    }

    IEnumerator DoAIMove()
    {
        yield return new WaitForSeconds(0.5f); // delay for realism

        List<(Piece piece, Vector2Int move, int score)> allMoves = new();

        // Scan all black pieces
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Piece p = GameManager.Instance.board[x, y];

                if (p != null && p.isWhite == false) // AI = black
                {
                    var moves = p.GetAvailableMoves(GameManager.Instance.board);
                    foreach (var m in moves)
                    {
                        int score = 0;

                        Piece target = GameManager.Instance.board[m.x, m.y];
                        if (target != null)
                            score = target.GetValue(); // capture score

                        allMoves.Add((p, m, score));
                    }
                }
            }
        }

        if (allMoves.Count == 0)
            yield break;

        // Pick highest score move
        var best = allMoves[0];

        foreach (var mv in allMoves)
        {
            if (mv.score > best.score)
                best = mv;
        }

        // Move piece
        GameManager.Instance.MovePiece(best.piece, best.move);
    }
}
