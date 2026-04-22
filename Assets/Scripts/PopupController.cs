using System;
using System.Collections;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private float animDuration = 0.2f;

    public event Action OnClosed;

    private static readonly Vector3 ClosedScale = Vector3.zero;
    private static readonly Vector3 OpenScale = Vector3.one;

    public void Open()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ScaleTo(ClosedScale, OpenScale));
        AudioManager.Instance.PlaySFX(AudioManager.Instance.popupClip);
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleAndDeactivate(OpenScale, ClosedScale));
        AudioManager.Instance.PlaySFX(AudioManager.Instance.popupClip);
    }

    private IEnumerator ScaleTo(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        transform.localScale = from;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(from, to, elapsed / animDuration);
            yield return null;
        }
        transform.localScale = to;
    }

    private IEnumerator ScaleAndDeactivate(Vector3 from, Vector3 to)
    {
        yield return StartCoroutine(ScaleTo(from, to));
        gameObject.SetActive(false);
        OnClosed?.Invoke();
    }
}
