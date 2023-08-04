using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State m_currentState;
    
    public void Init(State firstState)
    {
        m_currentState = firstState;
        m_currentState.enabled = true;
        m_currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        m_currentState.Exit();
        m_currentState.enabled = false;
        m_currentState = newState;
        m_currentState.enabled = true;
        m_currentState.Enter();
    }

    public State getCurrentState()
    {
        return m_currentState;
    }
}
