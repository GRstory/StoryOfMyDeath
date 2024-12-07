using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character2DBase : MonoBehaviour, ICharacter2D, IHealth, IDetectorController
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _capsuleCollider;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _velocity;
    [SerializeField] private Character2DDirection _direction;

    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public CapsuleCollider2D Collider { get => _capsuleCollider; set => _capsuleCollider = value; }
    public Bounds InitBound { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity
    {
        get => _velocity;
        set
        {
            _velocity = value;
            if (Mathf.Approximately(value.x, 0)) return;
            if (_velocity.x > 0.1) Direction = Character2DDirection.Right;
            else if(_velocity.x < -0.1) Direction = Character2DDirection.Left;
        }
    }
    public float Horizontal { get; set; }
    public Vector2 GroundNormal { get; set; }
    public Quaternion GroundZAngle { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public Character2DDirection Direction
    {
        get => _direction;
        set
        {
            switch (value)
            {
                case Character2DDirection.Left:
                    _spriteRenderer.flipX = true;
                    break;
                case Character2DDirection.Right:
                    _spriteRenderer.flipX = false;
                    break;
                case Character2DDirection.None:
                    break;
            }
            _direction = value;
        }
    }
    public int Health { get; private set; }
    [SerializeField] public int MaxHealth { get; } = 100;
    [SerializeField] private ClipSet _clipSet;
    public List<Detector> DetectorList { get; set; } = new List<Detector>();

    public event Action OnDead;

    private Animator _animator;
    private StateMachine _stateMachine;
    private StateMachineData _stateMachineData;

    [SerializeField] private DetectorData _detectorData;
    [SerializeField] private DetectorData _prevData;

    [SerializeField] private float _colliderYSize = 1.8f;
    [SerializeField] private float _colliderXSize = 0.5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InitBound = new Bounds((Vector3)Collider.offset, (Vector3)Collider.size);

        _detectorData = new DetectorData();
        _prevData = DetectorData.DeepCopy(_detectorData);
        SetCollider();

        _detectorData.Bound = InitBound;
        _detectorData.IsGround = true;
        _detectorData.CanStand = true;
        _detectorData.IsBlockedForward = true;
        Direction = Character2DDirection.Right;

        _stateMachineData = new StateMachineData();
        _stateMachine = new StateMachine(_stateMachineData, this);

        UpdateAnimation();
        RecoverHealth();

        Detector groundDetector = new DetectorGround();
        Detector forwardDetector = new DetectorForward();
        DetectorList.Add(groundDetector);
        DetectorList.Add(forwardDetector);
    }

    protected virtual void FixedUpdate()
    {
         if (Health == 0) return;
        _velocity = Rigidbody.linearVelocity;
        if (_detectorData.GroundRigidbody != null)
        {
            _velocity -= _detectorData.GroundVelocity;
        }

        UpdateDetector();
        UpdateStateMachine();
        UpdateAnimation();
        SetRigidbodyVelocity();
    }

    public void Die(Death deathType)
    {
        if (Health != 0) return;

        OnDead?.Invoke();
        string key = deathType.DeathTextKey;

    }

    public int GetDamage(int amount)
    {
        Health = Mathf.Clamp(Health - amount, 0, MaxHealth);
        return Health;
    }

    public void RecoverHealth()
    {
        Health = MaxHealth;
    }

    public void UpdateDetector()
    {
        _prevData = DetectorData.DeepCopy(_detectorData);
        _detectorData = new DetectorData();

        _detectorData.Bound = new Bounds(transform.TransformPoint(Collider.offset), Collider.size);
        _detectorData.CharacterDirection = _direction == Character2DDirection.Left ? -1 : 1;
        foreach (Detector detector in DetectorList)
        {
            detector.Update(_detectorData);
        }
    }

    public void UpdateStateMachine()
    {
        _stateMachine?.Update(this);
    }

    public void SetCollider()
    {
        Collider.size = new Vector2(_colliderXSize, _colliderYSize);
        Collider.offset = new Vector2(Collider.offset.x, _colliderYSize / 2);
    }

    public void UpdateDirection()
    {
        switch (Direction)
        {
            case Character2DDirection.Left:
                _spriteRenderer.flipX = true;
                break;
            case Character2DDirection.Right:
                _spriteRenderer.flipX = false;
                break;
            case Character2DDirection.None:
                break;
        }

    }

    public void UpdateAnimation()
    {
        string id = _stateMachine?.CurrentState.AnimationId;
        _animator?.SetTrigger(id);
    }

    private void SetRigidbodyVelocity()
    {
        Vector2 vector = Velocity;

        if (_detectorData.GroundRigidbody != null)
        {
            Vector2 lhs = (Vector2)Vector3.Cross(_detectorData.GroundNoraml, Vector3.forward);
            vector += lhs * Vector2.Dot(lhs, Velocity);
            //vector += _detectorData.GroundNoraml * Vector2.Dot(_detectorData.GroundNoraml, Velocity);
            //Debug.Log("??:" + _detectorData.GroundNoraml * Vector2.Dot(_detectorData.GroundNoraml, Velocity));
        }
        if (_detectorData.GroundRigidbody != null)
        {
            vector += _detectorData.GroundVelocity;
        }
        _rigidbody.linearVelocity = vector;
    }
}
