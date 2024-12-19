using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManagerEx
{
    public static @InputSystem_Actions contolAsset = new @InputSystem_Actions();

    static InputManagerEx()
    {
        contolAsset = new @InputSystem_Actions();
        contolAsset.Enable();
    }

    public static void SetPlayerInput(bool active)
    {
        if (active) contolAsset.Player.Enable();
        else contolAsset.Player.Disable();
    }

    public static void RegisterUINavigateAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Navigate.performed -= action;
        contolAsset.UI.Navigate.performed += action;
    }

    public static void DeregisterUINavigateAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Navigate.performed -= action;
    }

    public static void RegisterUISubmitAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Submit.performed -= action;
        contolAsset.UI.Submit.performed += action;
    }

    public static void DeregisterUISubmitAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Submit.performed -= action;
    }

    public static void RegisterUICancleAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Cancel.performed -= action;
        contolAsset.UI.Cancel.performed += action;
    }

    public static void DeregisterUICancleAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.UI.Cancel.performed -= action;
    }

    public static void RegisterMoveAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.Player.Move.performed -= action;
        contolAsset.Player.Move.performed += action;
        contolAsset.Player.Move.canceled -= action;
        contolAsset.Player.Move.canceled += action;
    }

    public static void DeregisterMoveAction(Action<InputAction.CallbackContext> action)
    {
        contolAsset.Player.Move.performed -= action;
        contolAsset.Player.Move.canceled -= action;
    }
}
