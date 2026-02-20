using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerBack : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
