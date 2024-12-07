using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonMonobehavior<InputManager>
{
    private PlayerInput _playerInput;

    private InputAction _menuOpenAction;

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
        _menuOpenAction = _playerInput?.actions["MenuOpen"];
    }

    private void Update()
    {
        
    }
}
