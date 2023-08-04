using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = true;
        m_animator.SetInteger("stateStage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() || CanHearPlayer())
        {
            m_stateMachine.ChangeState(m_enemy.getChase());
        }
        else
        {
            if (checkTimer())
            {
                m_stateMachine.ChangeState(m_enemy.getPatrol());
            }
        }
        
        
        //m_stateMachine.ChangeState(m_enemy.getPatrol());
    }

    public override void Exit()
    {
        m_enemy.getNavMeshAgent().isStopped = false;
    }
}
