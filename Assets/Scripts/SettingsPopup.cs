using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private PopupController popup;

    private void OnEnable()
    {
        if (AudioManager.Instance == null) return;

        bgmToggle.SetIsOnWithoutNotify(AudioManager.Instance.IsBGMEnabled());
        sfxToggle.SetIsOnWithoutNotify(AudioManager.Instance.IsSFXEnabled());
        bgmSlider.SetValueWithoutNotify(AudioManager.Instance.GetBGMVolume());
        sfxSlider.SetValueWithoutNotify(AudioManager.Instance.GetSFXVolume());
    }

    public void OnBGMToggleChanged(bool value) => AudioManager.Instance.SetBGMEnabled(value);
    public void OnSFXToggleChanged(bool value) => AudioManager.Instance.SetSFXEnabled(value);
    public void OnBGMSliderChanged(float value) => AudioManager.Instance.SetBGMVolume(value);
    public void OnSFXSliderChanged(float value) => AudioManager.Instance.SetSFXVolume(value);

    public void Close() => popup.Close();
}
