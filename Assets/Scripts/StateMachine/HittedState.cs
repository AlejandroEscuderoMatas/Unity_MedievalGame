using System;
using UnityEngine;

public class HittedState : State
{
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = true;
    }

    private void Update()
    {
        if (checkTimer())
        {
            m_stateMachine.ChangeState(m_stateMachine.getCurrentState());
        }
    }
}