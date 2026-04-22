using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellView : MonoBehaviour
{
    [SerializeField] private Image markImage;

    private Button _button;
    private int    _index;
    private bool   _isEmpty = true;


    public bool IsEmpty => _isEmpty;

    public void Init(int index)
    {
        _index = index;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _index = transform.GetSiblingIndex();
    }

    public void SetMark(Sprite sprite)
    {
        _isEmpty = false;
        markImage.sprite  = sprite;
        markImage.enabled = true;
        _button.interactable = false;
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }

    public void ResetCell()
    {
        _isEmpty = true;
        markImage.sprite  = null;
        markImage.enabled = false;
        markImage.color   = Color.white;
        _button.interactable = true;
    }

    public void OnClick()
    {
        GameManager.Instance.OnCellClicked(_index);
    }
}
