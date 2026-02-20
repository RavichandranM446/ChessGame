using System.Collections;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;

    public Piece[,] board = new Piece[8, 8];
    public bool isWhiteTurn = true;
    public bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    // ---------------------------------------------------------
    //                     MOVE PIECE ENTRY
    // ---------------------------------------------------------
    public void MovePiece(Piece piece, Vector2Int newPos)
    {
        if (gameOver) return;

        // MULTIPLAYER MODE
        if (HomeMenu.playMultiplayer)
        {
            photonView.RPC("RPC_MovePiece", RpcTarget.All,
                piece.photonView.ViewID, newPos.x, newPos.y);
        }
        else
        {
            // LOCAL / AI MODE
            StartCoroutine(SmoothMove(piece, newPos));
        }
    }

    // ---------------------------------------------------------
    //                   RPC: SYNC MOVE
    // ---------------------------------------------------------
    [PunRPC]
    public void RPC_MovePiece(int pieceID, int x, int y)
    {
        Piece piece = PhotonView.Find(pieceID).GetComponent<Piece>();
        Vector2Int newPos = new Vector2Int(x, y);

        StartCoroutine(SmoothMove(piece, newPos));
    }

    // ---------------------------------------------------------
    //                    SMOOTH MOVEMENT
    // ---------------------------------------------------------
    IEnumerator SmoothMove(Piece piece, Vector2Int newPos)
    {
        Vector3 start = piece.transform.position;
        Vector3 target = new Vector3(newPos.x, 0.5f, newPos.y);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            piece.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        piece.transform.position = target;

        // ----- CAPTURE -----
        if (board[newPos.x, newPos.y] != null)
        {
            Piece enemy = board[newPos.x, newPos.y];

            // KING CAPTURE
            if (enemy is King)
            {
                gameOver = true;
                string winner = piece.isWhite ? "White" : "Black";

                UIManager.Instance?.ShowWinner(winner);
            }

            Destroy(enemy.gameObject);
        }

        // BOARD UPDATE
        board[piece.boardPos.x, piece.boardPos.y] = null;
        piece.boardPos = newPos;
        board[newPos.x, newPos.y] = piece;

        // VISUAL
        piece.ResetToNormal();
        TileHighlighter.Instance?.MarkLastMove(newPos);

        // SWITCH TURN
        isWhiteTurn = !isWhiteTurn;

        // --------------------------------------------
        // ?? UPDATE TURN UI IMAGE
        // --------------------------------------------
        UIManager.Instance?.UpdateTurn(isWhiteTurn);

        // -----------------------------------------------------
        //                        AI TURN
        // -----------------------------------------------------
        if (HomeMenu.playWithAI && !isWhiteTurn && !gameOver)
        {
            AIMoveManager.Instance.PlayAI();
        }

        // -----------------------------------------------------
        //                   CHECKMATE LOGIC
        // -----------------------------------------------------
        bool currentTurn = isWhiteTurn;

        if (IsKingInCheck(currentTurn) && !HasAnyLegalMove(currentTurn))
        {
            gameOver = true;

            string winner = currentTurn ? "Black" : "White";
            UIManager.Instance?.ShowWinner(winner);
            yield break;
        }
    }

    // ---------------------------------------------------------
    //                  CHECK IF KING IN CHECK
    // ---------------------------------------------------------
    public bool IsKingInCheck(bool whiteKing)
    {
        Vector2Int kingPos = new Vector2Int(-1, -1);

        // FIND KING
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                if (board[x, y] is King && board[x, y].isWhite == whiteKing)
                {
                    kingPos = new Vector2Int(x, y);
                    goto FOUND;
                }

            FOUND:

        if (kingPos.x == -1) return false;

        // ENEMY ATTACKS KING?
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
            {
                Piece p = board[x, y];
                if (p != null && p.isWhite != whiteKing)
                {
                    var moves = p.GetAvailableMoves(board);
                    if (moves.Contains(kingPos))
                        return true;
                }
            }

        return false;
    }

    // ---------------------------------------------------------
    //               SIMULATE MOVES FOR CHECKMATE
    // ---------------------------------------------------------
    public bool HasAnyLegalMove(bool whiteTurn)
    {
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
            {
                Piece p = board[x, y];
                if (p != null && p.isWhite == whiteTurn)
                {
                    var moves = p.GetAvailableMoves(board);

                    foreach (var move in moves)
                    {
                        Piece target = board[move.x, move.y];
                        Vector2Int originalPos = p.boardPos;

                        // simulate
                        board[originalPos.x, originalPos.y] = null;
                        p.boardPos = move;
                        board[move.x, move.y] = p;

                        bool kingSafe = !IsKingInCheck(whiteTurn);

                        // undo
                        board[move.x, move.y] = target;
                        p.boardPos = originalPos;
                        board[originalPos.x, originalPos.y] = p;

                        if (kingSafe) return true;
                    }
                }
            }

        return false;
    }
}
