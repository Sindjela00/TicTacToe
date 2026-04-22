using UnityEngine;
using TMPro;

public class StatsPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text totalGamesText;
    [SerializeField] private TMP_Text player1WinsText;
    [SerializeField] private TMP_Text player2WinsText;
    [SerializeField] private TMP_Text drawsText;
    [SerializeField] private TMP_Text avgDurationText;
    [SerializeField] private PopupController popup;

    private void OnEnable()
    {
        GameStats s = StatisticsManager.Instance.Stats;
        totalGamesText.text  = $"Total Games: {s.totalGames}";
        player1WinsText.text = $"Player 1 Wins: {s.player1Wins}";
        player2WinsText.text = $"Player 2 Wins: {s.player2Wins}";
        drawsText.text       = $"Draws: {s.draws}";

        float avg = s.AverageDuration;
        avgDurationText.text = $"Avg Duration: {(int)(avg / 60):00}:{(int)(avg % 60):00}";
    }

    public void Close() => popup.Close();
}
