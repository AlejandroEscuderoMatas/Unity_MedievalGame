using System;
using UnityEngine;

public abstract class Item : MonoBehaviour , I_Item
{
    protected Player      m_player;
    protected AudioSource m_sfx;
    public AudioClip      m_pickupSound;
    public AudioClip      m_exitSound;

    protected bool        m_isPickedUp = false;
    protected bool        m_expiresImmediately = true;
    protected float       m_counterTime = 0;
    protected float       m_expiresWithTime = 0;

    protected virtual void Start()
    {
        m_sfx = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //SI NO ES UN ITEM DE ACCION IMMEDIATA
        if (m_isPickedUp && !m_expiresImmediately && m_expiresWithTime > 0)
        {
            m_counterTime += Time.deltaTime;

            if (m_counterTime > m_expiresWithTime)
            {
                ExitAction();
            }
        }
    }

    public void OnTriggerWithPlayer(Player player)
    {
        m_player = player;
        m_sfx.PlayOneShot(m_pickupSound);
        m_isPickedUp = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        m_counterTime = 0;
        
        ExecuteAction();
    }

    public virtual void ExecuteAction()
    {
        if (m_expiresImmediately)
        {
            ExitAction();
        }
    }
    
    public virtual void ExitAction()
    {
        m_sfx.PlayOneShot(m_exitSound);
        
        Destroy(gameObject);
    }
}