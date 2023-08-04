using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private FixedJoint m_joint;
    private float m_damage = 20;

    private void Start()
    {
        m_joint = GetComponent<FixedJoint>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.GetComponent<Rigidbody>() != null && collision.gameObject.tag.Equals("Environment")){
            //m_joint.connectedBody = collision.rigidbody;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
            //GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll;
        }
    }

    public void setDamage(float dmgUp)
    {
        m_damage += dmgUp;
    }

    public float getDamage()
    {
        return m_damage;
    }
}