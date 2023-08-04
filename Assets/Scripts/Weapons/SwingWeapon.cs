using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeapon : MonoBehaviour
{
    //GESTIONAR BALANCEO DEL ARMA
    private Quaternion m_startRotation;
    private float m_swingScale = 8;

    private void Start()
    {
        m_startRotation = GetComponent<Transform>().rotation;
    }

    private void Update()
    {
        Swing();
    }

    private void Swing()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion xRotation = Quaternion.AngleAxis(mouseX * -1.25f, Vector3.up);
        Quaternion yRotation = Quaternion.AngleAxis(mouseY * 1.25f, Vector3.left);
        Quaternion finalRotation = m_startRotation * xRotation * yRotation;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRotation, m_swingScale * Time.deltaTime);
    }
}
