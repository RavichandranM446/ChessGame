using UnityEngine;
using Photon.Pun;

public class GameNetworkManager : MonoBehaviourPun
{
    public static GameNetworkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Not connected? Go back to menu
        if (HomeMenu.playMultiplayer)
        {
            if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
                return;
            }

            Debug.Log("GameScene Loaded — Player = " +
                (PhotonNetwork.IsMasterClient ? "WHITE" : "BLACK"));

            // Only master client spawns pieces
            if (PhotonNetwork.IsMasterClient)
            {
                SpawnPieces();
            }
        }
    }

    // ==========================================================
    // ?? SPAWN ALL CHESS PIECES USING PHOTON
    // ==========================================================
    public void SpawnPieces()
    {
        Debug.Log("MASTER CLIENT: Spawning Chess Pieces...");

        SpawnWhite();
        SpawnBlack();

        Debug.Log("All pieces spawned successfully.");
    }

    // ==========================================================
    // WHITE TEAM (MasterClient)
    // ==========================================================
    void SpawnWhite()
    {
        for (int x = 0; x < 8; x++)
            Spawn("GPVFX_Pawn", x, 1);

        Spawn("GPVFX_Rook", 0, 0);
        Spawn("GPVFX_Rook", 7, 0);

        Spawn("GPVFX_Knight", 1, 0);
        Spawn("GPVFX_Knight", 6, 0);

        Spawn("GPVFX_Bishop", 2, 0);
        Spawn("GPVFX_Bishop", 5, 0);

        Spawn("GPVFX_Queen", 3, 0);
        Spawn("GPVFX_King", 4, 0);
    }

    // ==========================================================
    // BLACK TEAM (Remote Player)
    // ==========================================================
    void SpawnBlack()
    {
        for (int x = 0; x < 8; x++)
            Spawn("GPVFX_Pawn 1", x, 6);

        Spawn("GPVFX_Rook 1", 0, 7);
        Spawn("GPVFX_Rook 1", 7, 7);

        Spawn("GPVFX_Knight 1", 1, 7);
        Spawn("GPVFX_Knight 1", 6, 7);

        Spawn("GPVFX_Bishop 1", 2, 7);
        Spawn("GPVFX_Bishop 1", 5, 7);

        Spawn("GPVFX_Queen 1", 3, 7);
        Spawn("GPVFX_King 1", 4, 7);
    }

    // ==========================================================
    // Helper Function
    // ==========================================================
    void Spawn(string prefabName, int x, int y)
    {
        PhotonNetwork.Instantiate(prefabName, new Vector3(x, 0.5f, y), Quaternion.identity);
    }
}
