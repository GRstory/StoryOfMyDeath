using UnityEngine;

public abstract class Detector
{
    [SerializeField] private float _distance = 0.15f;

    public abstract void Update(DetectorData data);
}
