using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip bgmClip;
    public AudioClip buttonClickClip;
    public AudioClip placementClip;
    public AudioClip strikeClip;
    public AudioClip popupClip;

    private bool _bgmEnabled = true;
    private bool _sfxEnabled = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSettings();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    // ── Playback ──────────────────────────────────────────────────────────

    public void PlayBGM()
    {
        if (!_bgmEnabled || bgmSource.isPlaying) return;
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!_sfxEnabled || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // ── Toggles ───────────────────────────────────────────────────────────

    public void SetBGMEnabled(bool enabled)
    {
        _bgmEnabled = enabled;
        SaveSettings();

        if (enabled)
        {
            bgmSource.mute = false;
            if (!bgmSource.isPlaying)
            {
                bgmSource.clip = bgmClip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
        else
        {
            bgmSource.Stop();
            bgmSource.mute = true;
        }
    }

    public void SetSFXEnabled(bool enabled)
    {
        _sfxEnabled = enabled;
        sfxSource.mute = !enabled;
        SaveSettings();
    }

    public bool IsBGMEnabled() => _bgmEnabled;
    public bool IsSFXEnabled() => _sfxEnabled;

    // ── Volume ────────────────────────────────────────────────────────────

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveSettings();
    }

    public float GetBGMVolume() => bgmSource.volume;
    public float GetSFXVolume() => sfxSource.volume;

    // ── Persistence ───────────────────────────────────────────────────────

    private void LoadSettings()
    {
        _bgmEnabled = PlayerPrefs.GetInt("BGMEnabled", 1) == 1;
        _sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;
        bgmSource.mute = !_bgmEnabled;
        sfxSource.mute = !_sfxEnabled;
        bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("BGMEnabled", _bgmEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SFXEnabled", _sfxEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("BGMVolume", bgmSource.volume);
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
        PlayerPrefs.Save();
    }
}