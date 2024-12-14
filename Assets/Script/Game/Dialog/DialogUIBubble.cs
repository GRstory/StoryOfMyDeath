using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DialogUIBubble : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogText;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _tailImage;
    [SerializeField] private Image _endImage;

    [SerializeField] private float _maxWidth;
    [SerializeField] private float _worldYMargin = 0.25f;
    [SerializeField] private Vector2 _textMargin = new Vector2(80, 50);
    [SerializeField] private Vector2 _windowMargin = new Vector2(80, 100);

    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private RectTransform _textTransform;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private GameObject _go;

    public void Active(BubbleData data)
    {
        _nameText.text = data.Name;
        _dialogText.text = data.Dialog;
        _nameText.color = data.Color;
        _endImage.color = new Color(data.Color.r, data.Color.g, data.Color.b, 0.5f);

        transform.SetAsLastSibling();
        gameObject.SetActive(true);

        Resize();
    }

    public void Deactivate()
    {
        _nameText.text = "";
        _dialogText.text = "";
        _nameText.color = Color.black;
        _endImage.color = new Color(0f, 0f, 0f, 0.5f);

        gameObject.SetActive(false);
    }

    public void Resize()
    {
        //텍스트 RectTransform 사이즈
        Vector2 vector = new Vector2(_maxWidth, _dialogText.preferredHeight);
        if(_dialogText.preferredWidth < _maxWidth)
        {
            vector.x = _dialogText.preferredWidth;
        }
        _textTransform.sizeDelta = vector;

        //Dialog 줄 수가 여러줄이면 제일 긴 텍스트 줄 기준으로 사이즈 조절
        _dialogText.ForceMeshUpdate();
        if (_dialogText.textInfo.lineCount > 1)
        {
            float num = _dialogText.textInfo.lineInfo
                .Take(_dialogText.textInfo.lineCount)
                .Select(line => line.lineExtents.max.x - line.lineExtents.min.x)
                .Max();

            vector.x = num;
            vector.y = _dialogText.preferredHeight;
            _textTransform.sizeDelta = vector;
        }

        _nameText.rectTransform.sizeDelta = new Vector2(vector.x, _nameText.rectTransform.sizeDelta.y);

        Vector2 vector2 = new Vector2(_textTransform.rect.width + _textMargin.x, _textTransform.sizeDelta.y + _textMargin.y);
        _rectTransform.sizeDelta = vector2;
    }

    public void RePosition()
    {
        Vector3 worldPos = _go.transform.position;
        if (_go.TryGetComponent<Collider2D>(out Collider2D collider))
        {
            float topY = collider.bounds.size.y;
            worldPos.y += topY;
            //Debug.Log($"TopY Padding : {topY}, WorldPos = {worldPos}");
        }
        worldPos.y += _worldYMargin;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPos);
        
        screenPosition.x = Mathf.Clamp(screenPosition.x, ((_rectTransform.rect.width / 2) + _windowMargin.x), Screen.width - _rectTransform.rect.width / 2 - _windowMargin.x);
        screenPosition.y = Mathf.Clamp(screenPosition.y, ((_rectTransform.rect.height / 2) + _windowMargin.y), Screen.height - _rectTransform.rect.height / 2 - _windowMargin.y);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            screenPosition,
            null,
            out Vector2 localPosition
        );

        _rectTransform.localPosition = localPosition;
    }
}
