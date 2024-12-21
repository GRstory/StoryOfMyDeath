using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PauseUI_Tab2 : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private RectTransform _viewportRect;

    private List<Button> _buttonList = new List<Button>();

    private TransitionHelper _transitionHelper = new TransitionHelper();

    private void OnEnable()
    {
        UpdateElement();
    }

    private void Update()
    {
        if(_transitionHelper.InProgress)
        {
            _transitionHelper.Update();
            _contentRect.transform.localPosition = _transitionHelper.PosCurrent;
        }
    }

    public void OnButtonClick()
    {
        _buttonList[0]?.Select();
        PauseUI.Instance._status = 2;
    }

    private void UpdateElement()
    {
        _buttonList = GetComponentsInChildren<Button>()
            .Where(b => b.gameObject != gameObject)
            .ToList();

        SetElementNavigation();
    }

    private void SetElementNavigation()
    {
        for(int i = 0; i < _buttonList.Count; i++)
        {
            Navigation nav = new Navigation
            {
                mode = Navigation.Mode.Explicit
            };

            nav.selectOnUp = i > 0 ? _buttonList[i - 1] : _buttonList[_buttonList.Count - 1];
            nav.selectOnDown = i < _buttonList.Count - 1 ? _buttonList[i + 1] : _buttonList[0];

            _buttonList[i].navigation = nav;
        }
    }

    public void UpdateScroll(Selectable next)
    {
        float viewportTopBorderY = GetBorderTopYLocal(_viewportRect.gameObject);
        float viewportBottomBorderY = GetBorderBottomYLocal(_viewportRect.gameObject);

        //top
        float targetTopBorderY = GetBorderTopYRelative(next.gameObject);
        float targetTopYWithViewportOffset = targetTopBorderY + viewportTopBorderY;

        //bottom
        float targetBottomBorderY = GetBorderBottomYRelative(next.gameObject);
        float targetBottomYWithViewportOffset = targetBottomBorderY - viewportBottomBorderY;

        //topDiff
        float topDiff = targetTopYWithViewportOffset - viewportTopBorderY;
        if (topDiff > 0)
        {
            MoveContentObjectByAmount(topDiff * 100f + GetVerticalLayoutGroup().padding.top);
        }

        //bottomDiff
        float bottomDiff = targetBottomYWithViewportOffset - viewportBottomBorderY;
        if (bottomDiff < 0)
        {
            MoveContentObjectByAmount(topDiff * 100f + GetVerticalLayoutGroup().padding.bottom);
        }
    }

    private float GetBorderTopYLocal(GameObject go)
    {
        Vector3 pos = go.transform.localPosition / 100f;
        return pos.y;
    }

    private float GetBorderBottomYLocal(GameObject go)
    {
        Vector2 rectSize = go.GetComponent<RectTransform>().rect.size * 0.01f;
        Vector3 pos = go.transform.localPosition / 100f;
        pos.y -= rectSize.y;
        return pos.y;
    }

    private float GetBorderTopYRelative(GameObject go)
    {
        float contentY = _contentRect.transform.localPosition.y / 100f;
        float targetBorderUpYLocal = GetBorderTopYLocal(go);
        float targetBorderUpYRelative = targetBorderUpYLocal + contentY;
        return targetBorderUpYRelative;
    }

    private float GetBorderBottomYRelative(GameObject go)
    {
        float contentY = _contentRect.transform.localPosition.y / 100f;
        float targetBorderBottomYLocal = GetBorderBottomYLocal(go);
        float targetBorderBottomYRelative = targetBorderBottomYLocal + contentY;
        return targetBorderBottomYRelative;
    }

    private void MoveContentObjectByAmount(float amount)
    {
        Vector2 posScrollFrom = _contentRect.transform.localPosition;
        Vector2 posScrollTo = posScrollFrom;
        posScrollTo.y -= amount;

        _transitionHelper.TransitionPositionFromTo(posScrollFrom, posScrollTo, 1f);
    }

    private VerticalLayoutGroup GetVerticalLayoutGroup()
    {
        VerticalLayoutGroup verticalLayoutGroup = _contentRect.GetComponent<VerticalLayoutGroup>();
        return verticalLayoutGroup;
    }
}
