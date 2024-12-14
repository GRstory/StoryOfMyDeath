using System;
using UnityEngine;

[Serializable]
public class StateMoving2D : StateBase2D
{
    [SerializeField] private float speed = 2.5f;
    private float _xVelocity = 0f;
    private string _moveAnimationID = "move";
    private string _idleAnimationID = "idle";
    public override string AnimationId
    {
        get
        {
            if (Mathf.Abs(_xVelocity) > 0f) return _moveAnimationID;
            else return _idleAnimationID;
        }
    }


    public override void Update(ICharacter2D character2D)
    {
        float y = Vector2.Dot(character2D.GroundNormal, character2D.Velocity);
        character2D.Velocity = (Vector2)(character2D.GroundZAngle * new Vector3(character2D.Horizontal * speed, y, 0f));
        _xVelocity = character2D.Velocity.x;
    }
}
