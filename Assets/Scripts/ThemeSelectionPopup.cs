using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThemeSelectionPopup : MonoBehaviour
{
    [SerializeField] private PopupController popup;

    [Header("X Selector")]
    [SerializeField] private Button[] xSkinButtons = new Button[3];

    [Header("O Selector")]
    [SerializeField] private Button[] oSkinButtons = new Button[3];

    private int _xSelected = 0;
    private int _oSelected = 0;

    private void OnEnable()
    {
        RefreshX(_xSelected);
        RefreshO(_oSelected);
    }

    public void SelectXSkin(int index)
    {
        if (index < 0 || index >= xSkinButtons.Length) return;
        RefreshX(index);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
    }

    public void SelectOSkin(int index)
    {
        if (index < 0 || index >= oSkinButtons.Length) return;
        RefreshO(index);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
    }


    public void OnStartButton()
    {
        var xImg = xSkinButtons[_xSelected].GetComponent<Image>();
        var oImg = oSkinButtons[_oSelected].GetComponent<Image>();
        GameSession.SelectedXSprite = xImg.sprite;
        GameSession.SelectedOSprite = oImg.sprite;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickClip);
        if (GameSession.SelectedXSprite == null && GameSession.SelectedOSprite == null)
            return;
        SceneManager.LoadScene("GameScene");
    }

    public void Close() => popup.Close();


    private void RefreshX(int index)
    {
        _xSelected = index;
        ApplySelection(xSkinButtons, index);
    }

    private void RefreshO(int index)
    {
        _oSelected = index;
        ApplySelection(oSkinButtons, index);
    }

    private void ApplySelection(Button[] buttons, int selectedIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null) continue;

            var outline = buttons[i].GetComponent<Outline>();
            if (outline == null)
                outline = buttons[i].gameObject.AddComponent<Outline>();

            outline.effectColor    = Color.black;
            outline.effectDistance = new Vector2(3, 3);
            outline.enabled        = i == selectedIndex;
        }
    }
}
