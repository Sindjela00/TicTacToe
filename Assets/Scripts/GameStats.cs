[System.Serializable]
public class GameStats
{
    public int totalGames;
    public int player1Wins;
    public int player2Wins;
    public int draws;
    public float totalDuration;

    public float AverageDuration => totalGames > 0 ? totalDuration / totalGames : 0f;
}
