using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI_Tab1 : MonoBehaviour
{
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
}
