using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlayer2D : Character2DBase
{
    [SerializeField] private Detector _groundDetector;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void OnMove(InputValue value)
    {
        Horizontal = value.Get<Vector2>().x;
        //Debug.Log(Horizontal);
    }
}
