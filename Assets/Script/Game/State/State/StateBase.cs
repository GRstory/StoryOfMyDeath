using System;
using UnityEngine;

[Serializable]
public class StateBase2D
{
    public virtual string AnimationId { get; }

    public virtual void Init(ICharacterMovement character) { }

    public virtual void Start(ICharacterMovement character) { }

    public virtual void Update(ICharacterMovement character) { }

    public virtual void End(ICharacterMovement character) { }
}