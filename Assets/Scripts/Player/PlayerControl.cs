using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Player m_player;
    private CharacterController m_controller;
    public Camera m_camera;

    public KeyCode m_sprint;

    private Vector3 m_playerCamRotation;
    private float m_limitFOV;

    private float m_camRotXLimit;

    private float m_gravity = -9.81f;
    private Vector3 m_gravForce;

    // Start is called before the first frame update
    void Start()
    {
        //m_player = GetComponent<Player>();
        m_controller = GetComponent<CharacterController>();
        m_camRotXLimit = 60;
        m_limitFOV = 55;
        //m_controller.GetComponent<Collider>().enabled = true;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && moveAndRotate())
        {
            if (CameraManager.Instance.getFOVcontrol() == 0)
            {
                if (Input.GetKey(m_sprint))
                    m_camera.fieldOfView -= Time.deltaTime * 20f;
                else
                    m_camera.fieldOfView += Time.deltaTime * 40f;
                
                m_camera.fieldOfView = Mathf.Clamp(m_camera.fieldOfView, m_limitFOV, 60);
            }

            if (Input.GetKeyDown(m_sprint))
            {
                m_player.m_walkSpeed *= 1.7f;
            }
            else if (Input.GetKeyUp(m_sprint))
            {
                m_player.m_walkSpeed /= 1.7f;
                
            }
        }
        
        //LE APLICAMOS GRAVEDAD AL PERSONAJE
        m_gravForce.y += m_gravity * Time.deltaTime;
        m_controller.Move(m_gravForce * Time.deltaTime);
    }

    public bool moveAndRotate()
    {
        //*************************************CONTROLAR MOVIMIENTO Y ROTACION************************************
        //Controlar movimiento
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        m_controller.Move(move * (Time.deltaTime * m_player.m_walkSpeed));
        
        //Controlar rotacion
        m_playerCamRotation.y += Input.GetAxis("Mouse X") * m_player.m_rotationSensitivity;
        transform.eulerAngles = new Vector3(0, m_playerCamRotation.y, 0);

        m_playerCamRotation.x += -Input.GetAxis("Mouse Y") * m_player.m_rotationSensitivity;
        m_playerCamRotation.x = Mathf.Clamp(m_playerCamRotation.x, -m_camRotXLimit, m_camRotXLimit);
        
        m_camera.transform.localRotation = Quaternion.Euler(m_playerCamRotation.x, 0, 0);

        if (move != Vector3.zero) return true;
        else return false;
    }
}
