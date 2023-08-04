using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeaponAnim : StateMachineBehaviour
{
    //GESTIONAR BALANCEO DEL ARMA
    private Weapon m_weapon;
    private Quaternion m_startRotation;
    private float m_swingScale = 8;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_weapon = animator.GetComponent<Weapon>();
        m_startRotation = m_weapon.transform.rotation;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

        m_weapon.transform.localRotation = Quaternion.Lerp(m_weapon.transform.localRotation, finalRotation, m_swingScale * Time.deltaTime);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
