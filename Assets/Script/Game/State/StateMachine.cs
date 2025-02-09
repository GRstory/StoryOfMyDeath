using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    private readonly StateBase2D[] _states;
    private readonly StateLink2D[] _stateLinks;

    public StateBase2D CurrentState { get; private set; }

    
    public StateMachine(StateMachineData data, ICharacterMovement chracter)
    {
        data.GenerateStateBase();
        data.GenerateStateLink();
        _states = data.States;
        _stateLinks = data.StateLinks;
        CurrentState = _states[0];
    }

    public void Update(ICharacterMovement character)
    {
        foreach (StateLink2D link in _stateLinks.Where<StateLink2D>((Func<StateLink2D, bool>)(a => a.OldState == CurrentState)))
        {
            if (link.CheckCondition(character))
            {
                CurrentState.End(character);
                CurrentState = link.NewState;
                CurrentState.Start(character);
                break;
            }
        }

        CurrentState?.Update(character);
    }
}

public class StateMachineData
{
    private List<StateBase2D> _states = new List<StateBase2D>();
    private List<StateLink2D> _linkList = new List<StateLink2D>();

    public StateBase2D[] States { get { return _states.ToArray(); } }
    public StateLink2D[] StateLinks { get { return _linkList.ToArray(); } }

    public void GenerateStateBase()
    {
        _states.Add(new StateMoving2D());
        _states.Add(new StateJump());
    }

    public void GenerateStateLink()
    {
        _linkList.Add(new StateLink2DGeneric<StateMoving2D, StateJump>((IEnumerable)_states, new Func<StateMoving2D, StateJump, ICharacterMovement, bool>((oldState, newState, condition) => false)));
        _linkList.Add(new StateLink2DGeneric<StateJump, StateMoving2D>((IEnumerable)_states, new Func<StateJump, StateMoving2D, ICharacterMovement, bool>((oldState, newState, condition) => false)));
    }

    public List<StateLink2D> GetStateLinkList()
    {
        return _linkList;
    }
}