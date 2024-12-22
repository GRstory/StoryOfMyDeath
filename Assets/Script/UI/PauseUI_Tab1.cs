using Febucci.UI.Core;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using I2.Loc;

public class PauseUI_Tab1 : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TypewriterCore _infoWriter;

    private string _defaultDisplayTitleText = "UI_Pause_Display_Title";
    private string _defaultDisplayInfoText = "UI_Pause_Display_Info";
    private Button[,] _buttonArray = new Button[4, 4];

    private void Awake()
    {
        Button[] buttonArray = GetComponentsInChildren<Button>();

        for (int i = 1; i < buttonArray.Length; i++)
        {
            _buttonArray[(i - 1) / 4, (i - 1) % 4] = buttonArray[i];
        }

        SetElementNavigation();
    }

    private void OnEnable()
    {
        
    }

    private void SetElementNavigation()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                Navigation navigation = new Navigation { mode = Navigation.Mode.Explicit };

                navigation.selectOnUp = y > 0 ? _buttonArray[y - 1, x] : null;
                navigation.selectOnDown = y < 3 ? _buttonArray[y + 1, x] : null;
                navigation.selectOnLeft = x > 0 ? _buttonArray[y, x - 1] : null;
                navigation.selectOnRight = x < 3 ? _buttonArray[y, x + 1] : null;

                _buttonArray[y, x].navigation = navigation;
            }
        }
    }

    public void OnButtonClick()
    {
        _buttonArray[0, 0]?.Select();
        PauseUI.Instance._status = 1;
    }

    public void SetDisplay()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject;

        _titleText.text = currentObject.name;
        _infoWriter.ShowText(_titleText.text);
    }

    public void ResetDisplay()
    {
        _titleText.text = LocalizationManager.GetTranslation(_defaultDisplayTitleText);
        _infoWriter.ShowText(LocalizationManager.GetTranslation(_defaultDisplayInfoText));
    }
}
