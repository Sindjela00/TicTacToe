using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StrikeAnimator : MonoBehaviour
{
    [SerializeField] private Image  lineImage;
    [SerializeField] private float  lineThickness = 14f;
    [SerializeField] private float  overshoot     = 30f;
    [SerializeField] private float  animDuration  = 0.35f;

    private RectTransform _lineRt;
    private RectTransform _parentRt;

    private void EnsureRefs()
    {
        if (_lineRt != null) return;
        lineImage.gameObject.SetActive(true);
        _lineRt   = lineImage.rectTransform;
        _parentRt = _lineRt.parent as RectTransform;
        lineImage.enabled          = false;
        lineImage.gameObject.SetActive(false);
    }

    public void Play(int[] winLine, CellView[] cells)
    {
        if (lineImage == null || winLine == null || winLine.Length < 3) return;
        EnsureRefs();
        StopAllCoroutines();
        StartCoroutine(Animate(winLine, cells));
    }

    public void Hide()
    {
        StopAllCoroutines();
        if (lineImage != null)
        {
            lineImage.enabled = false;
            lineImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator Animate(int[] winLine, CellView[] cells)
    {
        Vector2 a = _parentRt.InverseTransformPoint(
            cells[winLine[0]].GetComponent<RectTransform>().position);
        Vector2 b = _parentRt.InverseTransformPoint(
            cells[winLine[2]].GetComponent<RectTransform>().position);

        Vector2 center = (a + b) / 2f;
        Vector2 delta  = b - a;
        float   length = delta.magnitude + overshoot * 2f;
        float   angle  = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        _lineRt.anchoredPosition = center;
        _lineRt.localRotation    = Quaternion.Euler(0f, 0f, angle);
        _lineRt.sizeDelta        = new Vector2(0f, lineThickness);
        lineImage.gameObject.SetActive(true);
        lineImage.enabled        = true;

        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animDuration);
            _lineRt.sizeDelta = new Vector2(Mathf.Lerp(0f, length, t), lineThickness);
            yield return null;
        }

        _lineRt.sizeDelta = new Vector2(length, lineThickness);
    }
}
