using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [Header("Cell Views")]
    [SerializeField] private CellView[] cells;

    [Header("Popups")]
    [SerializeField] private GameResultPopup gameResultPopup;
    [SerializeField] private SettingsPopup   settingsPopup;

    [Header("HUD")]
    [SerializeField] private HUDController hud;

    [Header("Panels")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject boardPanel;
    [SerializeField] private GameObject footerPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject gameContent;

    [Header("Strike line")]
    [SerializeField] private StrikeAnimator strikeAnimator;


    public enum GameState { Idle, Playing, Paused, GameOver }

    public GameState State        { get; private set; } = GameState.Idle;
    public int       CurrentPlayer { get; private set; } = GameBoard.Player1;
    public float     MatchStartTime { get; private set; }

    private readonly GameBoard _board = new GameBoard();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        StartMatch();
    }

    public void StartMatch()
    {
        _board.Reset();
        CurrentPlayer = GameBoard.Player1;
        MatchStartTime = Time.time;
        State = GameState.Playing;

        foreach (CellView cell in cells)
        {
            cell.ResetCell();
        }

        hud.OnMatchStarted();
    }

    public void OnCellClicked(int index)
    {

        if (State != GameState.Playing)
        {
            return;
        }

        if (!_board.PlaceMark(index, CurrentPlayer))
        {
            return;
        }

        Sprite mark = CurrentPlayer == GameBoard.Player1
            ? GameSession.SelectedXSprite
            : GameSession.SelectedOSprite;

        cells[index].SetMark(mark);
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.placementClip);


        hud.UpdateCurrentPlayer(CurrentPlayer);
        var (winner, winLine) = _board.CheckResult();

        if (winner != 0)
        {
            EndMatch(winner, winLine);
            return;
        }

        // Switch turn
        CurrentPlayer = CurrentPlayer == GameBoard.Player1
            ? GameBoard.Player2
            : GameBoard.Player1;

        hud.UpdateCurrentPlayer(CurrentPlayer);
    }

    public void OnPauseButton()
    {
        if (State == GameState.Playing)
        {
            State = GameState.Paused;
            hud.SetPaused(true);
            SetCellsInteractable(false);
            boardPanel.SetActive(false);
        }
        else if (State == GameState.Paused)
        {
            State = GameState.Playing;
            hud.SetPaused(false);
            SetCellsInteractable(true);
            boardPanel.SetActive(true);
        }
    }

    public void OnSettingsButton()
    {
        if (State == GameState.Playing)
        {
            State = GameState.Paused;
            hud.SetPaused(true);
            SetCellsInteractable(false);
        }

        var popup = settingsPopup.GetComponent<PopupController>();
        popup.OnClosed += ResumeAfterSettings;
        HideContent();
        popup.Open();
    }

    public void OnExitToMenuButton()
    {
        ExitToMenu();
    }

    public void OpenSettings() => OnSettingsButton();

    private void EndMatch(int winner, int[] winLine)
    {
        State = GameState.GameOver;
        hud.StopTimer();
        float duration = hud.ElapsedSeconds;

        StatisticsManager.Instance.RecordResult(winner, duration);

        if (winner > 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.strikeClip);
            strikeAnimator.Play(winLine, cells);
        }

        hudPanel.SetActive(false);
        footerPanel.SetActive(false);
        gameResultPopup.SetData(winner, duration);
        resultPanel.SetActive(true);
    }

    private void ResumeAfterSettings()
    {
        settingsPopup.GetComponent<PopupController>().OnClosed -= ResumeAfterSettings;

        ShowContent();

        if (State == GameState.Paused)
        {
            State = GameState.Playing;
            hud.SetPaused(false);
            SetCellsInteractable(true);
        }
    }

    private void SetCellsInteractable(bool interactable)
    {
        foreach (CellView cell in cells)
        {
            if (!interactable || cell.IsEmpty)
                cell.SetInteractable(interactable);
        }
    }

    public void RetryMatch()
    {
        resultPanel.SetActive(false);
        hudPanel.SetActive(true);
        footerPanel.SetActive(true);
        strikeAnimator.Hide();
        StartMatch();
    }

    private void HideContent() => gameContent?.SetActive(false);
    private void ShowContent() => gameContent?.SetActive(true);

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
