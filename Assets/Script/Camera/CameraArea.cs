using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _firstCam;
    [SerializeField] private CinemachineVirtualCamera _secondCam;

    private Collider2D _collider;
    private static CinemachineVirtualCamera _currentCamera;

    private void Awake()
    {
        _collider = GetComponent<CompositeCollider2D>();
        if( _collider == null )
        {
            _collider = GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Vector2 exitDirection = (collision.transform.position - _collider.bounds.center).normalized;
        ChangeCamera(_firstCam, _secondCam, exitDirection);
    }

    private static void ChangeCamera(CinemachineVirtualCamera cam1, CinemachineVirtualCamera cam2, Vector2 exitPos)
    {
        if (exitPos.x > 0f)
        {
            cam1.enabled = false;
            cam2.enabled = true;

            _currentCamera = cam2;
        }
        else if (exitPos.x < 0f)
        {
            cam1.enabled = true;
            cam2.enabled = false;

            _currentCamera = cam1;
        }
    }
}
