using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UI_Dialog : UI_HUD
{
    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private Transform _dialogParent;
    [SerializeField] private RectTransform _content;
    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] private GameObject _textObjectPrefap;
    [SerializeField] private GameObject _cardObjectPrefap;

    public TMP_InputField _ifDialog;
    public TMP_InputField _ifCharacterId;

    private string _tempDialog = "";
    private int _tempCharacterId = 0;
    private LinkedList<TextUI_Base> _textUILink = new LinkedList<TextUI_Base>();


    #region UI_Base 공통
    public override void Active()
    {
        base.Active();

        OpenDialog(true);
    }

    public override void Deactive()
    {
        _canvasTransform.anchoredPosition = new Vector2(700f, 0f);

        base.Deactive();
    }
    #endregion

    #region Open & Close

    public void OpenDialog(bool resetStartPosition)
    {
        if(resetStartPosition) _canvasTransform.anchoredPosition = new Vector2(700f, 0f);
        _canvasTransform.DOAnchorPosX(0f, 1.5f).SetEase(_activeEase);
    }

    public void CloseDialog()
    {
        _canvasTransform.anchoredPosition = new Vector2(0f, 0f);
        _canvasTransform.DOAnchorPosX(600f, 1.5f).SetEase(_activeEase);
    }

    #endregion
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _tableName = scene.name;
    }

    private string GetLocalizeString(string tableName, string id)
    {
        /*var stringHandle = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tableName, id);

        if (stringHandle.IsDone && stringHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            return stringHandle.Result;
        }*/
        return null;
    }

    #region 임시
    public void ChangeDialog()
    {
        _tempDialog = _ifDialog.text;
        Debug.Log($"SetDialog : {_tempDialog}");
    }

    public void ChangeCharacterId()
    {
        _tempCharacterId = int.Parse(_ifCharacterId.text);
        Debug.Log($"SetId : {_tempCharacterId}");
    }
    #endregion

    #region Dialog 관리
    public void AddTextUI(int index)
    {
        GameObject go = null;
        TextUI_Base textUI = null;

        switch (index)
        {
            case (int)TextUIType.Base:
                go = Instantiate(_textObjectPrefap, _dialogParent);
                textUI = go.GetComponent<TextUI_Base>();
                break;
            case (int)TextUIType.Button:
                go = Instantiate(_cardObjectPrefap, _dialogParent);
                textUI = go.GetComponent<TextUI_Base>();
                break;
        }

        if (go == null || textUI == null) return;

        textUI.SetDialogText(_tempDialog);
        textUI.CharacterId = _tempCharacterId;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_dialogParent.GetComponent<RectTransform>());

        if (_textUILink.Count > 0 && _textUILink.Last.Value.CharacterId == _tempCharacterId)
        {
            textUI.ActiveImage(active: false);
        }
        _textUILink.AddLast(textUI);

        CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, 1f).OnStart(() => { canvasGroup.blocksRaycasts = false; }).OnComplete(() => { canvasGroup.blocksRaycasts = true; });

        ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        _scrollRect.content.anchoredPosition = new Vector3(0, -1060);
    }
    #endregion
}

public enum TextUIType
{
    Base,
    Button,
}