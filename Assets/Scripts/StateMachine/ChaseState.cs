using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = false;
        m_animator.SetInteger("stateStage", 2);
        m_enemy.getNavMeshAgent().speed *= 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanSeePlayer() && !CanHearPlayer() && checkTimer())
        {
            m_stateMachine.ChangeState(m_enemy.getPatrol());
        }
        else if (CanHitPlayer())
        {
            m_stateMachine.ChangeState(m_enemy.getAttack());
        }
        else
        {
            m_enemy.getNavMeshAgent().SetDestination(m_playerTransform.position);
        }
    }

    public override void Exit()
    {
        m_enemy.getNavMeshAgent().speed *= 0.625f;
    }
}
