using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    [SerializeField] CharacterController controller;
    [SerializeField] Animator a;
    [SerializeField] Transform Target;
    void Start()
    {
        
    }

    
    void Update()
    {
        float HorizontalMove = joystick.Horizontal;
        float VerticalMove = joystick.Vertical;

        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        

        if (Input.GetKey(KeyCode.S) || VerticalMove < 0)
        {
            a.SetBool("IsMoving", true);
            controller.Move(Move * SpeedMove * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) || VerticalMove > 0)
        {
            a.SetBool("IsMoving", true);
            controller.Move(Move * SpeedMove * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || HorizontalMove < 0)
        {
            a.SetBool("IsMoving", true);
            controller.Move(Move * SpeedMove * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || HorizontalMove > 0)
        {
            a.SetBool("IsMoving", true);
            controller.Move(Move * SpeedMove * Time.deltaTime);
        }
        else
        {
            a.SetBool("IsMoving", false);
        }

    }

    
}
