using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    public Piece selectedPiece;
    public List<Vector2Int> validMoves = new();

    private void Awake()
    {
        Instance = this;
    }

    // ----------------------------------------------------
    //                   UPDATE LOOP
    // ----------------------------------------------------
    void Update()
    {
        // --- MOBILE TOUCH ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleTap(touch.position);
            }
        }

        // --- PC MOUSE CLICK ---
        if (Input.GetMouseButtonDown(0))
        {
            HandleTap(Input.mousePosition);
        }
    }

    // ----------------------------------------------------
    //               RAYCAST TAP HANDLER
    // ----------------------------------------------------
    void HandleTap(Vector2 screenPos)
    {
        // GAME OVER? then ignore clicks
        if (GameManager.Instance.gameOver)
            return;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f))
            return;

        // ---------------- PIECE CLICKED ----------------
        if (hit.collider.CompareTag("Piece"))
        {
            Piece piece = hit.collider.GetComponent<Piece>();

            // -------- MULTIPLAYER TURN CHECK --------
            if (HomeMenu.playMultiplayer)
            {
                bool isLocalWhite = PhotonNetwork.IsMasterClient;

                // Not your piece ? ignore
                if (piece.isWhite != isLocalWhite)
                    return;

                // Not your turn ? ignore
                if (piece.isWhite != GameManager.Instance.isWhiteTurn)
                    return;
            }
            else
            {
                // -------- LOCAL / AI TURN CHECK --------
                if (piece.isWhite != GameManager.Instance.isWhiteTurn)
                    return;
            }

            SelectPiece(piece);
        }

        // ---------------- TILE CLICKED ----------------
        else if (hit.collider.CompareTag("Tile"))
        {
            MoveSelectedPiece(hit.collider.gameObject);
        }
    }

    // ----------------------------------------------------
    //                 SELECT PIECE
    // ----------------------------------------------------
    void SelectPiece(Piece piece)
    {
        if (selectedPiece != null)
            selectedPiece.SetSelected(false);

        selectedPiece = piece;
        selectedPiece.SetSelected(true);

        validMoves = piece.GetAvailableMoves(GameManager.Instance.board);

        TileHighlighter.Instance.HighlightTiles(validMoves, GameManager.Instance.board);
    }

    // ----------------------------------------------------
    //                MOVE SELECTED PIECE
    // ----------------------------------------------------
    void MoveSelectedPiece(GameObject tile)
    {
        if (selectedPiece == null)
            return;

        Vector2Int tilePos = TileHighlighter.Instance.GetTilePosition(tile);

        if (validMoves.Contains(tilePos))
        {
            GameManager.Instance.MovePiece(selectedPiece, tilePos);
        }

        TileHighlighter.Instance.ClearHighlights();

        selectedPiece.SetSelected(false);
        selectedPiece = null;
    }
}
