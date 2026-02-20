using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image turnImage;
    public Sprite whiteTurnSprite;
    public Sprite blackTurnSprite;

    public GameObject restartButton;
    public GameObject gameOverPanel;

    public TextMeshProUGUI winnerText; // <-- NEW

    private void Awake()
    {
        Instance = this;

        if (restartButton != null) restartButton.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void UpdateTurn(bool isWhite)
    {
        if (turnImage != null)
            turnImage.sprite = isWhite ? whiteTurnSprite : blackTurnSprite;
    }

    public void ShowWinner(string winner)
    {
        // Set Winner Text in the panel
        if (winnerText != null)
            winnerText.text = winner + " Wins!";

        // Update turn image
        if (turnImage != null)
            turnImage.sprite = (winner == "White") ? whiteTurnSprite : blackTurnSprite;

        // Show Restart Button + Winner Panel
        restartButton?.SetActive(true);
        gameOverPanel?.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
