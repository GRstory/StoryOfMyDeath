using Febucci.UI;
using Febucci.UI.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogUIBubble : SingletonMonobehavior<DialogUIBubble>
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogText;
    [SerializeField] private TextAnimator_TMP _dialogTextAnimator;
    [SerializeField] public TypewriterCore _dialogTextWriter;

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

    private bool _isFinish = true;
    private Coroutine _finishTypingCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _dialogTextWriter.onTextShowed.AddListener(OnTypingFinish);
    }

    public void Active(DialogData data, string id)
    {
        StopAllCoroutines();
        _isFinish = false;
        _backgroundImage.gameObject.SetActive(true);
        _endImage.gameObject.SetActive(false);

        _dialogTextWriter.ShowText(data.Dialog);
        _nameText.text = data.Name;
        _nameText.color = data.Color;

        int cnt = 0;
        if (data.Color.r < 0.35f) cnt++;
        if (data.Color.g < 0.35f) cnt++;
        if (data.Color.b < 0.35f) cnt++;

        if(cnt > 1)
        {
            _endImage.color = Color.white;
        }
        else _endImage.color = new Color(data.Color.r, data.Color.g, data.Color.b);

        Resize();
        RePosition(id);
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        _isFinish = false;

        _nameText.text = "";
        _dialogText.text = "";
        _nameText.color = Color.black;
        _endImage.color = new Color(0f, 0f, 0f, 0.5f);

        _backgroundImage.gameObject.SetActive(false);
        _tailImage.gameObject.SetActive(false);
        _endImage.gameObject.SetActive(false);
    }

    private void Resize()
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

    private void RePosition(string id)
    {
        GameObject character = NPCManager.GetNPCObject(id).gameObject;
        Vector3 worldPos = Vector3.zero;
        if (character) worldPos = character.transform.position;

        if (character.TryGetComponent<Collider2D>(out Collider2D collider))
        {
            float topY = collider.bounds.size.y;
            worldPos.y += topY;
            //Debug.Log($"TopY Padding : {topY}, WorldPos = {worldPos}");
        }
        worldPos.y += _worldYMargin;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPos);
        Vector3 rawScreenPsition = screenPosition;

        screenPosition.x = Mathf.Clamp(screenPosition.x, ((_rectTransform.rect.width / 2) + _windowMargin.x), Screen.width - _rectTransform.rect.width / 2 - _windowMargin.x);
        screenPosition.y = Mathf.Clamp(screenPosition.y, ((_rectTransform.rect.height / 2) + _windowMargin.y), Screen.height - _rectTransform.rect.height / 2 - _windowMargin.y);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            screenPosition,
            null,
            out Vector2 localPosition
        );

        _rectTransform.localPosition = localPosition;

        SetTail(rawScreenPsition);
    }

    private void SetTail(Vector3 screenPosition)
    {
        _tailImage.gameObject.SetActive(true);

        float minX = _backgroundImage.rectTransform.anchoredPosition.x - (_backgroundImage.rectTransform.sizeDelta.x * 0.5f) + _textMargin.x * 0.5f;
        float maxX = _backgroundImage.rectTransform.anchoredPosition.x + (_backgroundImage.rectTransform.sizeDelta.x * 0.5f) - _textMargin.x * 0.5f;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            screenPosition,
            null,
            out Vector2 localPosition
        );

        if (localPosition.x < minX || localPosition.x > maxX)
        {
            _tailImage.gameObject.SetActive(false);
            return;
        }

        float positionX = Mathf.Clamp(localPosition.x, minX, maxX);

        _tailImage.rectTransform.localPosition = new Vector3(positionX, _backgroundImage.rectTransform.localPosition.y - 166.66666f, 0);
    }

    public bool IsFinish()
    {
        return _isFinish;
    }

    public void OnTypingFinish()
    {
        _isFinish = true;
        _finishTypingCoroutine = StartCoroutine(FinishTypingCoroutine());
    }

    private IEnumerator FinishTypingCoroutine()
    {
        while (true)
        {
            _endImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _endImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
