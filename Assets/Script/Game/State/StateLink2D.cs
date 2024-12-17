using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public abstract class StateLink2D
{
    public abstract StateBase2D OldState { get; }
    public abstract StateBase2D NewState { get; }
    public abstract bool CheckCondition(ICharacterMovement character);
    public bool IsValid
    {
        get => OldState != null && NewState != null && OldState != NewState;
    }
}


public class StateLink2DGeneric<Told, Tnew> : StateLink2D where Told : StateBase2D where Tnew : StateBase2D
{
    private readonly Told _oldState;
    private readonly Tnew _newState;
    private readonly Func<Told, Tnew, ICharacterMovement, bool> _checkContition;

    public override StateBase2D OldState => _oldState;
    public override StateBase2D NewState => _newState;

    public StateLink2DGeneric(IEnumerable states, Func<Told, Tnew, ICharacterMovement, bool> condition)
    {
        _oldState = states.OfType<Told>().FirstOrDefault<Told>();
        _newState = states.OfType<Tnew>().FirstOrDefault<Tnew>();
        _checkContition = condition;
    }

    public override bool CheckCondition(ICharacterMovement character)
    {
        return this._checkContition(_oldState, _newState, character);
    }
}
