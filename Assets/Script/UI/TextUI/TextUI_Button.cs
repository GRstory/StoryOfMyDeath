using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI_Button : TextUI_Base
{
    [Header("Reference")]
    [SerializeField] private Button[] _buttonList;
    [SerializeField] private TMP_Text[] _buttonTextList;
    [SerializeField] private Image[] _buttonImageList;
    [SerializeField] private RectTransform[] _buttonRectTransformList;

    [Header("Setting")]
    [SerializeField] private float _cardSequencetimer = 0.2f;

    private void Awake()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        Sequence sq = DOTween.Sequence();

        for (int i = 0; i < _buttonImageList.Length; i++)
        {
            sq.Insert(_cardSequencetimer * i, _buttonRectTransformList[i].DOShakeRotation(1f));
        }

        sq.Play();
    }
}
