using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance { get; private set; }

    public GameStats Stats { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Stats = SaveSystem.LoadStats();
    }

    public void RecordResult(int winner, float durationSeconds)
    {
        Stats.totalGames++;
        Stats.totalDuration += durationSeconds;

        if (winner == 1)       Stats.player1Wins++;
        else if (winner == 2)  Stats.player2Wins++;
        else                   Stats.draws++;

        SaveSystem.SaveStats(Stats);
    }
}
