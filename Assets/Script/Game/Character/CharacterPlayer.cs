using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlayer : CharacterBase
{
    [SerializeField] private Detector _groundDetector;

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

    public void OnMove(InputValue value)
    {
        Horizontal = value.Get<Vector2>().x;
        //Debug.Log(Horizontal);
    }
}
