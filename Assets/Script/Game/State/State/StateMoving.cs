using System;
using UnityEngine;

[Serializable]
public class StateMoving2D : StateBase2D
{
    [SerializeField] private float speed = 5f;

    public override void Update(ICharacter2D character2D)
    {
        float y = Vector2.Dot(character2D.GroundNormal, character2D.Velocity);
        character2D.Velocity = (Vector2)(character2D.GroundZAngle * new Vector3(Input.GetAxisRaw("Horizon") * speed, y, 0f));
    }
}
