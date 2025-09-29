using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Joystick Reference")]
    public Joystick joystick;  // Drag your Joystick object here in Inspector

    [Header("Player Animator")]
    public Animator anim;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get joystick input instead of WASD
        float x = joystick.Horizontal; // left/right
        float z = joystick.Vertical;   // forward/back

        // Convert input to world space movement
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);

        // Animation control
        if (move.magnitude > 0.01f) // if moving
        {
            if (anim.GetBool("PickingCup"))
            {
                anim.SetBool("WalkingWithCup", true);
            }
            anim.SetBool("IsWalking", true);
        }
        else
        {
            if (anim.GetBool("PickingCup"))
            {
                anim.SetBool("WalkingWithCup", false);
            }
            anim.SetBool("IsWalking", false);
        }
    }
}