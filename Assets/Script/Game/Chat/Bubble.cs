using DG.Tweening;
using Febucci.UI;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    [SerializeField] private CanvasGroup _bubbleParent;
    [SerializeField] private RectTransform _bubbleTransform;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TypewriterByCharacter _textWriter;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float _stringPerSecond = 0.1f;

    private Coroutine _blinkCoroutine;

    public bool IsFinish = false;

    private void OnEnable()
    {
        _textWriter.onMessage?.AddListener(FinishAnimation);
    }

    private void OnDisable()
    {
        _textWriter.onMessage?.RemoveListener(FinishAnimation);
    }

    public void SetText(string text)
    {
        _textWriter.ShowText(text);
        //StartCoroutine(DoText(text));
    }

    public void SetElement(float alpha, float fontSize, float height)
    {
        //투명도 설정
        _bubbleParent.alpha = alpha;

        //폰트 크기 설정
        _text.fontSize = fontSize;

        //높이 설정
        _bubbleTransform.sizeDelta = new Vector2(_bubbleTransform.sizeDelta.x, height);
        LayoutRebuilder.MarkLayoutForRebuild(_bubbleTransform);
    }

    public void FinishAnimation(Febucci.UI.Core.Parsing.EventMarker eventMarker)
    {
        if(eventMarker.name == "finish")
        {
            IsFinish = true;
            _blinkCoroutine = StartCoroutine(DoTextEndBlink());
        }
    }

    public void StopBlink()
    {
        StopAllCoroutines();
        _endPoint.SetActive(false);
    }

    private IEnumerator DoText(string text)
    {
        StringBuilder sb = new StringBuilder();
        WaitForSeconds wfs = new WaitForSeconds(_stringPerSecond);

        for (int i = 0; i < text.Length; i++)
        {
            sb.Append(text[i]);
            _text.text = sb.ToString();
            yield return wfs;
        }

        IsFinish = true;
        _blinkCoroutine = StartCoroutine(DoTextEndBlink());
    }

    private IEnumerator DoTextEndBlink()
    {
        WaitForSecondsRealtime wfs = new WaitForSecondsRealtime(0.5f);
        while(true)
        {
            _endPoint.SetActive(!_endPoint.activeSelf);
            yield return wfs;
        }
    }
}