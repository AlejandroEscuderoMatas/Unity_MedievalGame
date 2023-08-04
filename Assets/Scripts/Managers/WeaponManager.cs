using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] m_weapons;
    private KeyCode[] m_weaponChange;
    private int currentWeapon = 0;

    private void Start()
    {
        for (int i = 1; i < m_weapons.Length; i++)
        {
            m_weapons[i].gameObject.SetActive(false);
        }

        m_weaponChange = new KeyCode[m_weapons.Length];
        for (int i = 0; i < m_weaponChange.Length; i++)
        {
            m_weaponChange.SetValue(KeyCode.Alpha1 + i, i);
        }
    }

    public void moreDamage(float dmgUp)
    {
        for (int i = 0; i < m_weapons.Length; i++)
        {
            m_weapons[i].setDamage(dmgUp);
        }
            
    }

    private void Update()
    {
        changeWeapon();
    }

    public void changeWeapon()
    {
        for (int i = 0; i < m_weapons.Length; i++)
        {
            if (Input.GetKeyDown(m_weaponChange[i]) && currentWeapon != i)
            {
                m_weapons[i].gameObject.SetActive(true);
                m_weapons[currentWeapon].gameObject.SetActive(false);
                currentWeapon = i;
            }
        }
    }
}