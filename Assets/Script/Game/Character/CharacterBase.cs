using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class Character2DBase : MonoBehaviour, ICharacter2D, IHealth, IDetectorController
{
    public Rigidbody2D Rigidbody { get; set; }
    public CapsuleCollider2D Collider { get; set; }
    public Bounds InitBound { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 GroundNormal { get; set; }
    public Quaternion GroundZAngle { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public Character2DDirection Direction { get; set; }
    public int Health { get; private set; }
    [SerializeField] public int MaxHealth { get; } = 100;
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
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        InitBound = new Bounds((Vector3)Collider.offset, (Vector3)Collider.size);

        _detectorData = new DetectorData();
        _prevData = _detectorData.DeepCopy(_detectorData);
        SetCollider();

        _detectorData.Bound = InitBound;
        _detectorData.IsGround = true;
        _detectorData.CanStand = true;
        _detectorData.IsObstacleForward = true;

        _stateMachineData = new StateMachineData();
        _stateMachine = new StateMachine(_stateMachineData, this);

        UpdateAnimation();
        RecoverHealth();

        Detector groundDetector = new DetectorGround();
        DetectorList.Add(groundDetector);
    }

    protected virtual void FixedUpdate()
    {
         if (Health == 0) return;
        Velocity = Rigidbody.linearVelocity;
        if (_detectorData.GroundRigidbody != null)
        {
            Velocity -= _detectorData.GroundVelocity;
        }

        UpdateDetector();
        UpdateStateMachine();
        UpdateAnimation();
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
        _prevData = _detectorData.DeepCopy(_detectorData);
        _detectorData = new DetectorData();

        _detectorData.Bound = new Bounds(transform.TransformPoint(Collider.offset), Collider.size);
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

    public void UpdateAnimation()
    {
        //_animator?.SetTrigger(_stateMachine?.CurrentState.AnimationId);
    }
}
