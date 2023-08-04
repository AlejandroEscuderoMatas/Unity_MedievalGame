using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Animator m_animator;
    protected Enemy m_enemy;
    protected StateMachine m_stateMachine;
    protected Transform m_playerTransform;
    
    public float m_timePassed = 2f;
    
    public void Init(Enemy enemy, StateMachine SM)
    {
        m_enemy = enemy;
        m_stateMachine = SM;
        m_playerTransform = m_enemy.getPlayerTransform();
        m_animator = m_enemy.m_animator;
        this.enabled = false;
    }

    //HACEMOS ESTOS METODOS VIRTUALES PARA NO ESTAR OBLIGADOS A IMPLEMENTARLOS 
    //EN LOS ESTADOS QUE HEREDEN DE ESTE, COMO PASARIA CON ABSTRACT
    public virtual void Enter() {}

    public virtual void Exit() {}
    
    protected bool checkTimer()
    {
        m_timePassed -= Time.deltaTime;
        if (m_timePassed <= 0)
        {
            m_timePassed = 2;
            return true;
        }

        return false;
    }
    
    protected bool CanSeePlayer()
    {
        float Angle = Mathf.Abs(Vector3.Angle(transform.forward,
            (m_playerTransform.position - transform.position).normalized));

        if (Angle < m_enemy.getFieldOfView() && CanHearPlayer()) return true;
        else return false;
    }


    protected bool CanHearPlayer()
    {
        float distanceFromPlayer = Vector3.Distance(m_enemy.transform.position, m_playerTransform.position);
        
        if (m_enemy.getHearingDistance() >= distanceFromPlayer) return true;
        else return false;
    }

    protected bool CanHitPlayer()
    {
        float distanceFromPlayer = Vector3.Distance(m_enemy.transform.position, m_playerTransform.position);
        
        if (distanceFromPlayer < 6f) return true;
        else return false;
    }
}
