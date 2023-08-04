using System;
using System.Security.Cryptography;
using UnityEngine;

public class I_LifeUp : Item
{
    private float m_lifeUp = 20f;

    public override void ExecuteAction()
    {
        m_player.setStrength(m_lifeUp);
        base.ExecuteAction();
    }
}