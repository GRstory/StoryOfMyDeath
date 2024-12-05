using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _subject;

    private Vector2 _startPosition;
    private float _startZ;

    private Vector2 Travel => (Vector2)_camera.transform.position - _startPosition;
    private float DistanceFromSubject => transform.position.z - _subject.position.z;
    private float ClippingPlane => (_camera.transform.position.z + (DistanceFromSubject > 0 ? _camera.farClipPlane : _camera.nearClipPlane));
    private float ParallaxFactor => Mathf.Abs(DistanceFromSubject) / ClippingPlane;

    private void Start()
    {
        _startPosition = transform.position;
        _startZ = transform.position.z;
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 newPos = _startPosition + Travel * ParallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, _startZ);
    }
}
