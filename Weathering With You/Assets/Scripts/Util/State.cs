using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : MonoBehaviour
{
    protected T owner;

    public virtual void Enter(T owner) { }

    public virtual void Execute() { }

    public virtual void Exit() { }
}
