using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PauseUI : SingletonMonobehavior<PauseUI>
{
    [SerializeField] private Selectable _tab1;
    [SerializeField] private Selectable _tab2;
    [SerializeField] private Selectable _btn1;
    [SerializeField] private Selectable _btn2;
    [SerializeField] private Selectable _btn3;
    [SerializeField] private Selectable _btn4;

    [SerializeField] private GameObject _defaultSelectButton;
    [SerializeField] private PauseUI_Tab1 _tab1Controller;
    [SerializeField] private PauseUI_Tab2 _tab2Controller;
    public int _status;

    private void OnEnable()
    {
        InputManagerEx.RegisterUINavigateAction(OnNavigateAction);
        InputManagerEx.RegisterUISubmitAction(OnSubmitAction);
        InputManagerEx.RegisterUICancleAction(OnCancleAction);

        InputManagerEx.SetPlayerInput(false);
    }

    private void OnDisable()
    {
        InputManagerEx.DeregisterUINavigateAction(OnNavigateAction);
        InputManagerEx.DeregisterUISubmitAction(OnSubmitAction);
        InputManagerEx.DeregisterUICancleAction(OnCancleAction);

        InputManagerEx.SetPlayerInput(true);
    }

    private void OnNavigateAction(InputAction.CallbackContext context)
    {
        Vector2 navigateVector = context.ReadValue<Vector2>();

        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(!go)
        {
            EventSystem.current.SetSelectedGameObject(_defaultSelectButton);
        }

        Selectable selectable = go.GetComponent<Selectable>();
        Selectable next = null;
        if (navigateVector == Vector2.left)
        {
            next = selectable.FindSelectableOnLeft();
        }
        else if (navigateVector == Vector2.right)
        {
            next = selectable.FindSelectableOnRight();
        }
        else if (navigateVector == Vector2.up)
        {
            next = selectable.FindSelectableOnUp();
        }
        else if(navigateVector == Vector2.down)
        {
            next = selectable.FindSelectableOnDown();
        }

        if (next)
        {
            next.Select();
        }
        else
        {
            return;
        }

        AfterNavigateSelectable(next);
    }

    private void OnSubmitAction(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0) return;

        GameObject current = EventSystem.current.currentSelectedGameObject;
        Selectable selectable = current?.GetComponent<Selectable>();

        if (!selectable) return;

        Button button = selectable as Button;
        if (button)
        {
            button.onClick.Invoke();
        }
    }

    private void OnCancleAction(InputAction.CallbackContext context)
    {
        switch (_status)
        {
            case 0:
                gameObject.SetActive(false);
                break;
            case 1:
                _tab1.Select();
                _status = 0;
                break;
            case 2:
                _tab2.Select();
                _status = 0;
                break;
        }
    }

    private void AfterNavigateSelectable(Selectable next)
    {
        switch (_status)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                _tab2Controller.UpdateScroll(next);
                break;
        }
    }
}
