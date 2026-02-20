using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
