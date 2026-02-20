using UnityEngine;
using Photon.Pun;

public class BoardManager : MonoBehaviourPun
{
    public static BoardManager Instance;

    public GameObject tilePrefab;
    public float tileSize = 1f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateBoard();
    }

    // ========================================
    // ?? BOARD GENERATION (Local)
    // ========================================
    void GenerateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, y * tileSize);

                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.name = $"Tile {x},{y}";

                MeshRenderer rend = tile.GetComponent<MeshRenderer>();
                rend.material.color = (x + y) % 2 == 0 ? Color.white : Color.black;

                TileHighlighter.Instance.RegisterTile(x, y, tile);
            }
        }
    }

    // ========================================
    // ?? MULTIPLAYER PIECE SPAWN (Photon)
    // ========================================
    public void SpawnNetworkPieces()
    {
        // Only MasterClient spawns pieces (White)
        if (!PhotonNetwork.IsMasterClient)
            return;

        Debug.Log("Spawning network pieces...");

        // ==============================
        // WHITE SIDE (MasterClient)
        // ==============================

        // White Pawns
        for (int x = 0; x < 8; x++)
        {
            PhotonNetwork.Instantiate("GPVFX_Pawn_White",
                new Vector3(x, 0.5f, 1),
                Quaternion.identity);
        }

        // White Rooks
        PhotonNetwork.Instantiate("GPVFX_Rook_White", new Vector3(0, 0.5f, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Rook_White", new Vector3(7, 0.5f, 0), Quaternion.identity);

        // White Knights
        PhotonNetwork.Instantiate("GPVFX_Knight_White", new Vector3(1, 0.5f, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Knight_White", new Vector3(6, 0.5f, 0), Quaternion.identity);

        // White Bishops
        PhotonNetwork.Instantiate("GPVFX_Bishop_White", new Vector3(2, 0.5f, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Bishop_White", new Vector3(5, 0.5f, 0), Quaternion.identity);

        // White Queen
        PhotonNetwork.Instantiate("GPVFX_Queen_White", new Vector3(3, 0.5f, 0), Quaternion.identity);

        // White King
        PhotonNetwork.Instantiate("GPVFX_King_White", new Vector3(4, 0.5f, 0), Quaternion.identity);

        // ==============================
        // BLACK SIDE (remote Player)
        // ==============================

        // Black Pawns
        for (int x = 0; x < 8; x++)
        {
            PhotonNetwork.Instantiate("GPVFX_Pawn_Black",
                new Vector3(x, 0.5f, 6),
                Quaternion.identity);
        }

        // Black Rooks
        PhotonNetwork.Instantiate("GPVFX_Rook_Black", new Vector3(0, 0.5f, 7), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Rook_Black", new Vector3(7, 0.5f, 7), Quaternion.identity);

        // Black Knights
        PhotonNetwork.Instantiate("GPVFX_Knight_Black", new Vector3(1, 0.5f, 7), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Knight_Black", new Vector3(6, 0.5f, 7), Quaternion.identity);

        // Black Bishops
        PhotonNetwork.Instantiate("GPVFX_Bishop_Black", new Vector3(2, 0.5f, 7), Quaternion.identity);
        PhotonNetwork.Instantiate("GPVFX_Bishop_Black", new Vector3(5, 0.5f, 7), Quaternion.identity);

        // Black Queen
        PhotonNetwork.Instantiate("GPVFX_Queen_Black", new Vector3(3, 0.5f, 7), Quaternion.identity);

        // Black King
        PhotonNetwork.Instantiate("GPVFX_King_Black", new Vector3(4, 0.5f, 7), Quaternion.identity);

        Debug.Log("All network pieces spawned!");
    }
}
