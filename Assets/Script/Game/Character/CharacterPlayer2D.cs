using UnityEngine;

public class CharacterPlayer2D : Character2DBase
{
    [SerializeField] private Detector _groundDetector;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
