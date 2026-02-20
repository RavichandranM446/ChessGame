using UnityEngine;
using TMPro;

public class TurnUI : MonoBehaviour
{
    public TextMeshProUGUI turnText;

    void Update()
    {
        if (GameManager.Instance.isWhiteTurn)
            turnText.text = "White Turn";
        else
            turnText.text = "Black Turn";
    }
}
