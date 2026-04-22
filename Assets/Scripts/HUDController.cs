using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    [Header("Player Names")]
    [SerializeField] private TMP_Text p1NameText;
    [SerializeField] private TMP_Text p2NameText;

    private bool  _running;
    private float _elapsed;

    public float ElapsedSeconds => _elapsed;

    private void Update()
    {
        if (!_running)
            return;

        _elapsed += Time.deltaTime;
        timerText.text = FormatTime(_elapsed);
    }


    public void OnMatchStarted()
    {
        _elapsed       = 0f;
        _running       = true;
        timerText.text = "00:00";
        UpdateCurrentPlayer(GameBoard.Player1);
    }

    public void UpdateCurrentPlayer(int player)
    {
        bool p1Active = player == GameBoard.Player1;
        SetBold(p1NameText, p1Active);
        SetBold(p2NameText, !p1Active);
    }

    public void StopTimer()
    {
        _running = false;
    }

    public void SetPaused(bool paused)
    {
        _running = !paused;
    }

    private static void SetBold(TMP_Text label, bool bold)
    {
        label.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
    }

    private static string FormatTime(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        return $"{m:00}:{s:00}";
    }
}
