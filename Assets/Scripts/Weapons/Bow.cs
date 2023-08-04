using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public Camera           m_camera;
    
    public GameObject       m_visualArrow;
    public GameObject       m_arrowToShot;
    public List<GameObject> m_quiver = new List<GameObject>();
    private Transform       m_shotSpot;
    
    public int              m_totalArrows;
    private bool            m_canShot = true;
    private int             m_currentAmmo;
    
    private float           m_timeBetweenShots;
    private float           m_shotTimer = 0;
    private float           m_currentAccuracy;
    public float            m_shotForce = 1000f;

    // Start is called before the first frame update
    private void Start()
    {
        m_timeBetweenShots = 1f;
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_shotSpot = m_visualArrow.transform;
        
        m_totalArrows = 10;
        m_currentAmmo = m_totalArrows;
        m_arrowToShot.SetActive(false);
        
        m_damage = 0;
        
        createArrows();
    }
    
    public void createArrows()
    {
        for (int i = 0; i < m_totalArrows; i++)
        {
            GameObject arrowAux = Instantiate(m_arrowToShot, Vector3.zero, Quaternion.identity);
            //arrowAux.gameObject.layer = LayerMask.NameToLayer("Default");
            arrowAux.SetActive(false);
            m_quiver.Add(arrowAux);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canShot)
            {
                Attack();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (m_currentAmmo > 0 && !m_canShot)
            {
                reload();
            }
        }
    }

    public override void setDamage(float dmgMultiplier)
    {
        for (int i = 0; i < m_quiver.Count; i++)
        {
            m_quiver[i].GetComponent<Arrow>().setDamage(dmgMultiplier);
        }
    }

    //DISPARAR UNA FLECHA
    public override void Attack()
    {
        CameraManager.Instance.setFOVcontrol(1);
        Time.timeScale = .5f;

        StartCoroutine(FOVDown());
        
        m_canShot = false;
        m_animator.SetTrigger("Attack");
        m_audioSource.PlayOneShot(m_attackSound);
        StartCoroutine(ResetAttackCooldown());
    }

    private IEnumerator FOVDown()
    {
        while (m_camera.fieldOfView > 30)
        {
            m_camera.fieldOfView -= Time.deltaTime * 60;
            m_camera.fieldOfView = Mathf.Clamp(m_camera.fieldOfView, 30, 60);
            yield return null;
        }
    }
    private IEnumerator FOVUp()
    {
        while (m_camera.fieldOfView < 60)
        {
            m_camera.fieldOfView += Time.deltaTime * 20;
            m_camera.fieldOfView = Mathf.Clamp(m_camera.fieldOfView, 30, 60);
            yield return null;
        }
    }

    //RECARGAR EL ARCO
    void reload()
    {
        m_canShot = true;
        m_animator.SetTrigger("Recharge");
        StartCoroutine(ResetReloadCooldown());
    }
    
    public void throwArrow()
    {
        m_visualArrow.SetActive(false);
        Time.timeScale = 1f;
        
        m_quiver[m_currentAmmo-1].transform.position = m_visualArrow.transform.position;
        m_quiver[m_currentAmmo-1].transform.rotation = m_visualArrow.transform.rotation;
        m_quiver[m_currentAmmo-1].SetActive(true);
        //Debug.DrawRay(m_visualArrow.transform.position, m_visualArrow.transform.forward * (-1) * m_shotForce, Color.red, 4);

        m_quiver[m_currentAmmo - 1].GetComponent<Rigidbody>().useGravity = true;
        m_quiver[m_currentAmmo-1].GetComponent<Rigidbody>().AddForce(m_visualArrow.transform.forward * (-1) * m_shotForce);
        m_currentAmmo--;

        StartCoroutine(FOVUp());
        CameraManager.Instance.setFOVcontrol(0);
    }

    private new IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(m_attackCooldown);
        m_canAttack = true;
    }

    protected IEnumerator ResetReloadCooldown()
    {
        yield return new WaitForSeconds(m_reloadCooldown);
    }

    public void drawArrow()
    {
        m_visualArrow.SetActive(true);
    }
}
