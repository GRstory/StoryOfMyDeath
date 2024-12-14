using System;
using UnityEngine;

[Serializable]
public class StateJump : StateBase2D
{
    [SerializeField] private float _jumpForce = 4f;

    public override void Start(ICharacter2D character)
    {
        character.VelocityY = _jumpForce;
    }
}
