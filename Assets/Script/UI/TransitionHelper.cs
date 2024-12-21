using UnityEngine;

public class TransitionHelper
{
    private float _duration = 0f;
    private float _timeElapsed = 0f;
    private float _progress = 0f;

    private bool _inProgress = false;

    private Vector2 _posCurrent;
    private Vector2 _posFrom;
    private Vector2 _posTo;

    public bool InProgress { get => _inProgress; }

    public Vector2 PosCurrent { get => _posCurrent; }

    public void Update()
    {
        Tick();
        CalculatePosition();
    }

    private void Tick()
    {
        if (!_inProgress) return;

        _timeElapsed += Time.deltaTime;
        _progress += _timeElapsed / _duration;
        if (_progress > 1)
        {
            _progress = 1;
        }

        if (_progress >= 1)
        {
            TransitionComplete();
        }
    }

    public void Clear()
    {
        _duration = 0f;
        _timeElapsed = 0f;
        _progress = 0f;

        _inProgress = false;
    }

    public void TransitionPositionFromTo(Vector2 posFrom, Vector2 posTo, float duration)
    {
        Clear();

        _posFrom = posFrom;
        _posTo = posTo;
        _duration = duration;
        _inProgress = true;
    }

    private void CalculatePosition()
    {
        _posCurrent.x = Mathf.Lerp(_posFrom.x, _posTo.x, _progress);
        _posCurrent.y = Mathf.Lerp(_posFrom.y, _posTo.y, _progress);
    }

    private void TransitionComplete()
    {
        _inProgress = false;
    }
}
