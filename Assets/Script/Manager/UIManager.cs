using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

//UI_HUD는 동시에 한개밖에 켤 수 없습니다.
//UI_Popup은 동시에 여러개를 킬 수 있지만, UI 계층 구조에 영향을 받아 보이지 않는 UI가 있을 수 있습니다. 수정은 가능함
public class UIManager : SingletonMonobehavior<UIManager>
{
    private Dictionary<System.Type, GameObject> _uiDict = new Dictionary<System.Type, GameObject>();
    private UI_HUD _currentHUD;

    /// <summary>
    /// UI_Base를 상속한 컴포넌트를 가진 GameObject는 이 함수를 자동으로 실행시켜 딕셔너리에 등록합니다.
    /// </summary>
    /// <param name="ui"></param>
    public void RegisterUI(UI_Base ui)
    {
        var type = ui.GetType();
        if(!_uiDict.ContainsKey(type))
        {
            _uiDict.Add(type, ui.gameObject);

            if (transform.childCount == _uiDict.Count)
            {
                EventHandler.CallAfterRegisterUIManager();
            }
        }
    }

    /// <summary>
    /// HUD UI를 변경합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ChangeHUDUI<T>() where T : UI_HUD
    {
        System.Type type = typeof(T);
        if (_uiDict.TryGetValue(type, out GameObject go))
        {
            if (_currentHUD != null)
            {
                _currentHUD.Deactive();
            }

            UI_HUD hud = go.GetComponent<UI_HUD>();
            _currentHUD = hud;
            hud.Active();
        }
    }

    /// <summary>
    /// Popup UI를 켭니다. 이미 켜져있다면, 무시합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popup"></param>
    public void ShowPopup<T>() where T : UI_Popup
    {
        var type = typeof(T);
        if(_uiDict.TryGetValue(type, out GameObject go))
        {
            if(go.TryGetComponent<UI_Popup>(out UI_Popup popup))
            {
                if(!popup.IsActive)
                {
                    go.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError($"타입 {type}이 등록되지 않았습니다.");
        }
    }

    /// <summary>
    /// Popup UI를 끕니다. 이미 꺼져있다면, 무시합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popup"></param>
    public void ClosePopup<T>() where T : UI_Popup
    {
        var type = typeof(T);

        if (_uiDict.TryGetValue(type, out GameObject go))
        {
            go.SetActive(false);
        }
        else
        {
            Debug.LogError($"타입 {type}이 등록되지 않았습니다.");
        }
    }

    public T GetRegisteredUI<T>() where T : UI_Base
    {
        var type = typeof(T);

        if (_uiDict.TryGetValue(type, out GameObject go))
        {
            if(go.TryGetComponent<T>(out T output))
            {
                return output;
            }
        }
        return null;
    }
}
