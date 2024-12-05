using UnityEngine;

public interface ICharacter2D
{
    Rigidbody2D Rigidbody { get; }
    CapsuleCollider2D Collider { get; set; }
    Bounds InitBound { get; set; }
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    float VelocityX { get; set; }
    float VelocityY { get; set; }

    Character2DDirection Direction { get; set; }
    Vector2 GroundNormal { get; set; }
    Quaternion GroundZAngle { get; set; }
}

public enum Character2DDirection
{
    Left,
    Right,
    None
}
