using System;
using UnityEngine;

public class DeadState : State
{
    public override void Enter()
    {
        m_enemy.getNavMeshAgent().isStopped = true;
    }
}