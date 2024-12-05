using Cinemachine;
using UnityEngine;

public abstract class MapControllerBase : MonoBehaviour
{
    [SerializeField] private Collider2D _cameraCollider;
    [SerializeField] private CinemachineConfiner _cameraConfiner;

    protected virtual void Awake()
    {
        _cameraCollider = GetComponent<CompositeCollider2D>();
    }

    public virtual void Active()
    {
        _cameraConfiner.m_BoundingShape2D = _cameraCollider;
    }

    public virtual void Deactive()
    {

    }
}
