using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action OnSceneChangeEvent;
    public static event Action AfterRegisterUIManager;

    public static void CallAfterRegisterUIManager()
    {
        AfterRegisterUIManager?.Invoke();
    }
}
