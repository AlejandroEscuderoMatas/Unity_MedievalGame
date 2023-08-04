using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    //CARACTERISTICAS BASICAS DE TODAS LAS ARMAS
    protected float       m_damage;
    protected bool        m_canAttack = true;
    public float          m_attackCooldown = 1.0f;
    public float          m_reloadCooldown = 1.0f;
    protected Animator    m_animator;
    public AudioClip      m_attackSound;
    protected AudioSource m_audioSource;

    public float getDamage()
    {
        return m_damage;
    }

    public virtual void Attack()
    {
        m_canAttack = false;
        m_animator.SetTrigger("Attack");
        m_audioSource.PlayOneShot(m_attackSound);
        StartCoroutine(ResetAttackCooldown());
    }

    protected IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(m_attackCooldown);
        m_canAttack = true;
    }
    
    public virtual void setDamage(float dmgMultiplier)
    {
        m_damage += dmgMultiplier;
    }
}
