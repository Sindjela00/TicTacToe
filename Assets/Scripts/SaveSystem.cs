using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string StatsPath => Path.Combine(Application.persistentDataPath, "stats.json");

    public static void SaveStats(GameStats stats)
    {
        string json = JsonUtility.ToJson(stats, true);
        File.WriteAllText(StatsPath, json);
    }

    public static GameStats LoadStats()
    {
        if (!File.Exists(StatsPath))
            return new GameStats();

        string json = File.ReadAllText(StatsPath);
        return JsonUtility.FromJson<GameStats>(json);
    }
}
