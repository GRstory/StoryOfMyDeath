using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

//UI_HUD�� ���ÿ� �Ѱ��ۿ� �� �� �����ϴ�.
//UI_Popup�� ���ÿ� �������� ų �� ������, UI ���� ������ ������ �޾� ������ �ʴ� UI�� ���� �� �ֽ��ϴ�. ������ ������
public class UIManager : SingletonMonobehavior<UIManager>
{
    private Dictionary<System.Type, GameObject> _uiDict = new Dictionary<System.Type, GameObject>();
    private UI_HUD _currentHUD;

    /// <summary>
    /// UI_Base�� ����� ������Ʈ�� ���� GameObject�� �� �Լ��� �ڵ����� ������� ��ųʸ��� ����մϴ�.
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
    /// HUD UI�� �����մϴ�.
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
    /// Popup UI�� �մϴ�. �̹� �����ִٸ�, �����մϴ�.
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
            Debug.LogError($"Ÿ�� {type}�� ��ϵ��� �ʾҽ��ϴ�.");
        }
    }

    /// <summary>
    /// Popup UI�� ���ϴ�. �̹� �����ִٸ�, �����մϴ�.
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
            Debug.LogError($"Ÿ�� {type}�� ��ϵ��� �ʾҽ��ϴ�.");
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
