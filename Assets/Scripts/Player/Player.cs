using UnityEngine;

public class Player : MonoBehaviour
{
    private float               m_strength = 120;
    private float               m_dmgMultiplier = 1;
    private bool                m_invencibility = false;
    
    public PlayerControl        m_controller;
    public WeaponManager        m_weaponManager;
    public KeyCode              m_interactKey;

    [Range(5, 10)] public float m_walkSpeed;
    [Range(5, 20)] public float m_rotationSensitivity;
    
    //public Weapon[] m_weapons;

    private void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<I_Item>() != null)
            other.GetComponent<I_Item>().OnTriggerWithPlayer(this);
    }

    public void setStrength(float m_stregthUp)
    {
        if (m_stregthUp < 0 && !m_invencibility)
        {
            //QUE SE PONGA LA PANTALLA ROJA
            m_strength += m_stregthUp;
            
            if (m_strength <= 0)
            {
                GameManager.Instance.setRoundLost();
            }
        }
        else if(m_stregthUp > 0)
        {
            //QUE SE PONGA LA PANTALLA VERDE
            m_strength += m_stregthUp;
        }
        
        GameManager.Instance.UpdateHealth(m_strength);
    }

    public void setDmgMultiplier(float dmgUp)
    {
        m_dmgMultiplier += dmgUp;
        m_weaponManager.moreDamage(dmgUp);
        GameManager.Instance.m_UIcontroller.setPowerText((int)m_dmgMultiplier);
    }
    public int getDmgMultiplier()
    {
        return (int)Mathf.Ceil(m_dmgMultiplier);
    }
    
    public void setInvencibility(bool active)
    {
        m_invencibility = active;
    }

    public float getStrength()
    {
        return m_strength;
    }
}