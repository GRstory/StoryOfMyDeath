using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlayer : CharacterBase
{
    [SerializeField] private Detector _groundDetector;

    private void OnEnable()
    {
        InputManagerEx.RegisterMoveAction(OnMove);
    }

    private void OnDisable()
    {
        InputManagerEx.DeregisterMoveAction(OnMove);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteraction>(out IInteraction interaction))
        {
            interaction.Interaction();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<Vector2>().x;
    }

    private void RegisterInput()
    {
        InputManagerEx.RegisterMoveAction(OnMove);
    }

    private void UnregisterInput()
    {
        InputManagerEx.DeregisterMoveAction(OnMove);
    }
}
