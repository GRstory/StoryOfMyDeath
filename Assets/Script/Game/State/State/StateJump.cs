using System;
using UnityEngine;

[Serializable]
public class StateJump : StateBase2D
{
    [SerializeField] private float _jumpForce = 4f;

    public override void Start(ICharacterMovement character)
    {
        character.VelocityY = _jumpForce;
    }
}
