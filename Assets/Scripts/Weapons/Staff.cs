using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    public  Transform  m_raycastSpot; //Para saber donde debe ir la bola
    public GameObject  m_magicSpell;
    private GameObject m_player;

    private float      m_accuracyDropPerShot = 25f;
    private float      m_accuracyRecoveryPerSecond = 50f;
    private float      m_currentAccuracy;
    
    private float      m_timeBetweenShots;
    private float      m_shotTimer = 0;
    private bool       m_canShot = true;
    
    private void Start()
    {
        //m_player = GameObject.FindGameObjectWithTag("Player");
        //Physics.IgnoreCollision(m_magicSpell.GetComponent<Collider>(), m_player.GetComponent<Collider>(), true);
        
        m_timeBetweenShots = 2f;
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_damage = 40;
    }

    private void Update()
    {
        m_currentAccuracy += Time.deltaTime * m_accuracyRecoveryPerSecond;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canAttack)
            {
                Attack();
            }
        }
    }

    private void CastSpell()
    {
        //SETEAMOS EL VECTOR DIRECTOR DEL RAYO EN FUNCION DE LA PRECISION DEL ARMA
        float   accuracyModifier  = (100 - m_currentAccuracy) / 1000;
        Vector3 directionForward  = m_raycastSpot.forward;
        directionForward.x       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
        directionForward.y       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
        directionForward.z       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
        m_currentAccuracy        -= m_accuracyDropPerShot;
        m_currentAccuracy         = Mathf.Clamp(m_currentAccuracy, 0, 100);

        //m_magicSpell.transform.position = m_raycastSpot.position;
        //m_magicSpell.transform.forward = m_raycastSpot.forward;
        //m_magicSpell.transform.rotation = Quaternion.Euler(m_raycastSpot.forward);
        
        Ray ray = new Ray(m_raycastSpot.position, directionForward);
        Debug.DrawRay(m_raycastSpot.position, directionForward, Color.red, 4);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(m_raycastSpot.position, directionForward, 100.0F);
        
        m_magicSpell.SetActive(true);
    }

    private void DisableSpell()
    {
        m_magicSpell.SetActive(false);
    }
}
