using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Collider m_collider;
    
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
        m_damage = 20;
        
        //Desactivamos el collider nada mas instanciar la espada
        ToggleCollider();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canAttack)
            {
                Attack();
            }
        }
    }

    public void ToggleCollider()
    { 
        m_collider.enabled = !m_collider.enabled;
    }
}
