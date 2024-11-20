using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool crouching;
    private bool sprinting;

    private float crouchTimer;
    private bool lerpCrouch;

    private float playerSpeed;
    public float sprintSpeed;

    public float walkingSpeed = 5f;
    public float crouchModifier = 0.7f;
    private float gravity = -9.8f;
    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerSpeed = walkingSpeed;
        sprintSpeed = walkingSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        //crouching functionality. this times the crouch so it is not immediate
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1 ;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);
        
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -1f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
    public void Crouch()
    {
        crouching = !crouching;
        if (crouching)
        {
            playerSpeed *= crouchModifier;
        }
        else
        {
            playerSpeed /= crouchModifier;
        }
        lerpCrouch = true;
        crouchTimer = 0;
    }
    public void Sprint()
    {
        
        sprinting = !sprinting;
        playerSpeed = sprinting ? sprintSpeed : walkingSpeed;
    }
}
