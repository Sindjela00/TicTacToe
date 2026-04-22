using UnityEngine;
using TMPro;

public class GameResultPopup : MonoBehaviour
{
    [Header("Labels")]
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text durationText;

    public void SetData(int winner, float durationSeconds)
    {
        resultText.text = winner switch
        {
            GameBoard.Player1 => "Player 1 Wins!",
            GameBoard.Player2 => "Player 2 Wins!",
            _                 => "It's a Draw!",
        };

        int m = Mathf.FloorToInt(durationSeconds / 60f);
        int s = Mathf.FloorToInt(durationSeconds % 60f);
        durationText.text = $"Duration: {m:00}:{s:00}";
    }

    public void OnRetryButton()
    {
        GameManager.Instance.RetryMatch();
    }

    public void OnExitButton()
    {
        GameManager.Instance.ExitToMenu();
    }
}
