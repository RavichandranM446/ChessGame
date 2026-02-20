using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject whitePawn;
    public GameObject whiteRook;
    public GameObject whiteKnight;
    public GameObject whiteBishop;
    public GameObject whiteQueen;
    public GameObject whiteKing;

    public GameObject blackPawn;
    public GameObject blackRook;
    public GameObject blackKnight;
    public GameObject blackBishop;
    public GameObject blackQueen;
    public GameObject blackKing;

    public float pieceYOffset = 0.5f;

    void Start()
    {
        SpawnAllPieces();
    }

    void SpawnPiece(GameObject prefab, int x, int y, bool isWhite)
    {
        Vector3 pos = new Vector3(x, pieceYOffset, y);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        Piece p = obj.GetComponent<Piece>();
        p.isWhite = isWhite;
        p.boardPos = new Vector2Int(x, y);

        GameManager.Instance.board[x, y] = p;
    }

    void SpawnAllPieces()
    {
        // White pawns
        for (int x = 0; x < 8; x++)
            SpawnPiece(whitePawn, x, 1, true);

        // Black pawns
        for (int x = 0; x < 8; x++)
            SpawnPiece(blackPawn, x, 6, false);

        // Rooks
        SpawnPiece(whiteRook, 0, 0, true);
        SpawnPiece(whiteRook, 7, 0, true);
        SpawnPiece(blackRook, 0, 7, false);
        SpawnPiece(blackRook, 7, 7, false);

        // Knights
        SpawnPiece(whiteKnight, 1, 0, true);
        SpawnPiece(whiteKnight, 6, 0, true);
        SpawnPiece(blackKnight, 1, 7, false);
        SpawnPiece(blackKnight, 6, 7, false);

        // Bishops
        SpawnPiece(whiteBishop, 2, 0, true);
        SpawnPiece(whiteBishop, 5, 0, true);
        SpawnPiece(blackBishop, 2, 7, false);
        SpawnPiece(blackBishop, 5, 7, false);

        // Queens
        SpawnPiece(whiteQueen, 3, 0, true);
        SpawnPiece(blackQueen, 3, 7, false);

        // Kings
        SpawnPiece(whiteKing, 4, 0, true);
        SpawnPiece(blackKing, 4, 7, false);
    }
}
