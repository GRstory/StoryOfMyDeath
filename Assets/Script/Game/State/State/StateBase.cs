using System;
using UnityEngine;

[Serializable]
public class StateBase2D
{
    public virtual string AnimationId { get; }

    public virtual void Init(ICharacter2D character) { }

    public virtual void Start(ICharacter2D character) { }

    public virtual void Update(ICharacter2D character) { }

    public virtual void End(ICharacter2D character) { }
}