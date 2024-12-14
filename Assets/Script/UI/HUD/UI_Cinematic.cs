using DG.Tweening;
using UnityEngine;

public class UI_Cinematic : UI_HUD
{
    [Header("Reference")]
    [SerializeField] private RectTransform _topLetterBox;
    [SerializeField] private RectTransform _bottomLetterBox;

    public override void Active()
    {
        base.Active();

        _topLetterBox.anchoredPosition = new Vector2(0f, 130f);
        _bottomLetterBox.anchoredPosition = new Vector2(0f, -130f);

        _topLetterBox.DOAnchorPosY(0, 2f).SetEase(_activeEase);
        _bottomLetterBox.DOAnchorPosY(0, 2f).SetEase(_activeEase);
    }

    public override void Deactive()
    {
        _topLetterBox.DOAnchorPosY(130f, 2f);
        _bottomLetterBox.DOAnchorPosY(-130f, 2f);

        base.Deactive();
    }
}
