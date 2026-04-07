using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    T owner;

    public State<T> currentState;

    public StateMachine(T owner)
    {
        this.owner = owner;
    }

    public void Execute()
    {
        currentState?.Execute();
    }

    public void ChangeState(State<T> newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this.owner);
    }
}
