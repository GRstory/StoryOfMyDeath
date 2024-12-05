using System;
using UnityEngine;

[Serializable]
public class DetectorData
{
    public Bounds Bound;
    public bool IsGround;
    public bool CanStand;
    public bool IsObstacleForward;
    public Collider2D GroundCollider;
    public Rigidbody2D GroundRigidbody;
    public PlatformEffector2D PlatformEffector2D;
    public Vector2 GroundVelocity;
    public int CharacterDirection;

    public static DetectorData DeepCopy(DetectorData prevData)
    {
        return new DetectorData()
        {
            Bound = prevData.Bound,
            IsGround = prevData.IsGround,
            CanStand = prevData.CanStand,
            IsObstacleForward = prevData.IsObstacleForward,
            GroundCollider = prevData.GroundCollider,
            GroundRigidbody = prevData.GroundRigidbody,
            PlatformEffector2D = prevData.PlatformEffector2D,
            GroundVelocity = prevData.GroundVelocity,
            CharacterDirection = prevData.CharacterDirection,
        };
    }
}
