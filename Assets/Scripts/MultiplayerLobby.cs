using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MultiplayerLobby : MonoBehaviourPunCallbacks
{
    public TMP_Text statusText;
    public TMP_InputField roomInput;

    void Start()
    {
        statusText.text = "Connecting to Photon...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Connected ?";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        statusText.text = "In Lobby ?";
    }

    public void CreateRoom()
    {
        if (roomInput.text.Length < 1) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomInput.text, options);
        statusText.text = "Creating Room...";
    }

    public void JoinRoom()
    {
        if (roomInput.text.Length < 1) return;

        PhotonNetwork.JoinRoom(roomInput.text);
        statusText.text = "Joining Room...";
    }

    public override void OnCreatedRoom()
    {
        statusText.text = "Room Created ? Waiting for players...";
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined Room ?";

        // Load only if both players are present
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        statusText.text = "Player Joined ? Starting game...";

        // Again check if 2 players now
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
