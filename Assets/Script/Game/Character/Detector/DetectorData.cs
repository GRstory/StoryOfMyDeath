using System;
using UnityEngine;

[Serializable]
public class DetectorData
{
    public Bounds Bound;
    public bool IsGround;
    public bool CanStand;
    public bool IsBlockedForward;
    public Collider2D GroundCollider;
    public Rigidbody2D GroundRigidbody;
    public Vector2 GroundNoraml;
    public Vector2 GroundVelocity;
    public PlatformEffector2D PlatformEffector2D;
    public int CharacterDirection;

    public static DetectorData DeepCopy(DetectorData prevData)
    {
        return new DetectorData()
        {
            Bound = prevData.Bound,
            IsGround = prevData.IsGround,
            CanStand = prevData.CanStand,
            IsBlockedForward = prevData.IsBlockedForward,
            GroundCollider = prevData.GroundCollider,
            GroundRigidbody = prevData.GroundRigidbody,
            GroundNoraml = prevData.GroundNoraml,
            GroundVelocity = prevData.GroundVelocity,
            PlatformEffector2D = prevData.PlatformEffector2D,
            CharacterDirection = prevData.CharacterDirection,
        };
    }
}
