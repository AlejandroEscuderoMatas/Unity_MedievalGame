using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = true;
        m_animator.SetInteger("stateStage", 3);
    }

    public void checkHit()
    {
        if (HitsPlayer())
        {
            m_enemy.setHasHitted(true);
        }
    }

    public void finishIdle()
    {
        m_enemy.getNavMeshAgent().SetDestination(m_playerTransform.position);
        
        if (CanHitPlayer())
        {
            Enter();
        }
        else
        {
            m_stateMachine.ChangeState(m_enemy.getChase());
        }
    }

    public bool HitsPlayer()
    {
        float distanceFromPlayer = Vector3.Distance(m_enemy.transform.position, m_playerTransform.position);
        
        if (distanceFromPlayer <= 50) return true;
        else return false;
    }
}
