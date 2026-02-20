using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    public static bool playMultiplayer = false;
    public static bool playWithAI = false;
    public static bool onlineMode = false;

    public void PlayOffline()
    {
        playWithAI = false;
        onlineMode = false;
        SceneManager.LoadScene("GameScene");
    }

    public void PlayAI()
    {
        playWithAI = true;
        onlineMode = false;
        SceneManager.LoadScene("GameScene");
    }

    public void PlayMultiplayer()
    {
        playWithAI = false;
        onlineMode = true;
        SceneManager.LoadScene("MultiplayerLobbyScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
