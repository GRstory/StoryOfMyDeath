using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Color[] _backgroundColor;
    [SerializeField] private CanvasGroup[] _tabGroup;

    private int _mode = 1;

    public override void Active()
    {
        base.Active();

        _backgroundImage.DOColor(_backgroundColor[1], 1f);
    }

    public override void Deactive()
    {
        base.Deactive();
        _backgroundImage.color = _backgroundColor[1];
    }

    public void SetModeDown()
    {
        if (_mode == 0) return;

        if (_mode == 1) _mode = 0;
        else if (_mode == 2) _mode = 1;

        SetMode();
    }

    public void SetModeUp()
    {
        if (_mode == 2) return;

        if (_mode == 0) _mode = 1;
        else if (_mode == 1) _mode = 2;
        SetMode();
    }

    private void SetMode()
    {
        SetColor();
        SetTitleText();
        SetTab();
    }

    private void SetColor()
    {
        _backgroundImage.DOColor(_backgroundColor[_mode], 0.5f).SetEase(Ease.InOutQuad);
    }

    private void SetTitleText()
    {
        if (_mode == 0) _titleText.text = "Pause";
        else if (_mode == 1) _titleText.text = "Status";
        else if (_mode == 2) _titleText.text = "Archive";
    }

    private void SetTab()
    {
        for(int i = 0; i < _tabGroup.Count(); i++)
        {
            _tabGroup[i].DOFade(0, 0.5f).OnComplete(() => { _tabGroup[_mode].DOFade(1f, 0.25f); }).SetEase(Ease.InOutQuad);
        }
    }
}
