using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private float m_remainingDistance;
    
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = false;
        m_animator.SetInteger("stateStage", 1);
        m_enemy.NextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        m_remainingDistance = Vector3.Distance(m_enemy.transform.position, m_enemy.m_wayPoints[m_enemy.getNextWaypoint()].position);
        
        if (CanSeePlayer() || CanHearPlayer())
        {
            m_stateMachine.ChangeState(m_enemy.getChase());
        }
        else
        {
            //if(!m_enemy.getNavMeshAgent().pathPending)
            if(m_remainingDistance < 3f)
                m_stateMachine.ChangeState(m_enemy.getIdle());
        }
        
        /*if(!m_enemy.getNavMeshAgent().pathPending && m_enemy.getNavMeshAgent().remainingDistance < 2f)
            m_stateMachine.ChangeState(m_enemy.getIdle());*/
    }
}
