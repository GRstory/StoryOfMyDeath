using UnityEngine;

public class UI_Debug : MonoBehaviour
{
    public void Button0()
    {
        UIManager.Instance.ChangeHUDUI<UI_Dialog>();
    }

    public void Button1()
    {
        UIManager.Instance.ChangeHUDUI<UI_Cinematic>();
    }

    public void Button2()
    {
        UIManager.Instance.ChangeHUDUI<UI_MainScene>();
    }

    public void Button3()
    {
        UIManager.Instance.ShowPopup<UI_Pause>();
    }
}
