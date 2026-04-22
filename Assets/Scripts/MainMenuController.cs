using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PopupController themePopup;
    [SerializeField] private PopupController statsPopup;
    [SerializeField] private PopupController settingsPopup;
    [SerializeField] private PopupController exitPopup;

    [Header("Main Menu UI")]
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private TMP_Text titleText;

    private const string MainTitle = "Tic Tac Toe";

    private void Start()
    {
        AudioManager.Instance?.PlayBGM();

        themePopup.OnClosed    += ShowMenu;
        statsPopup.OnClosed    += ShowMenu;
        settingsPopup.OnClosed += ShowMenu;
        exitPopup.OnClosed     += ShowMenu;
    }

    private void OnDestroy()
    {
        themePopup.OnClosed    -= ShowMenu;
        statsPopup.OnClosed    -= ShowMenu;
        settingsPopup.OnClosed -= ShowMenu;
        exitPopup.OnClosed     -= ShowMenu;
    }

    public void OnPlayButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
        OpenPopup(themePopup, "Select Skins");
    }

    public void OnStatsButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
        OpenPopup(statsPopup, "Statistics");
    }

    public void OnSettingsButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
        OpenPopup(settingsPopup, "Settings");
    }

    public void OnExitButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
        OpenPopup(exitPopup, "Exit Game");
    }

    public void OnExitConfirm()
    {
        Application.Quit();
    }

    public void OnExitCancel()
    {
        exitPopup.Close();
    }

    private void OpenPopup(PopupController popup, string screenTitle)
    {
        buttonGroup.SetActive(false);
        titleText.text = screenTitle;
        popup.Open();
    }

    private void ShowMenu()
    {
        buttonGroup.SetActive(true);
        titleText.text = MainTitle;
    }
}
