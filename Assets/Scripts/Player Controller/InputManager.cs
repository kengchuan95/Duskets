using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject behavior;

    
    private PlayerInput playerInput;
    private PlayerInput.ActionsActions actions;
    private PlayerMotor motor;
    private PauseBehavior pause;
    private PlayerLook look;
    private PlayerInteractScript interact;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        actions = playerInput.Actions;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        interact = GetComponent<PlayerInteractScript>();

        pause = behavior.GetComponent<PauseBehavior>();

        actions.Jump.performed += ctx => motor.Jump();
        actions.Crouch.performed += ctx => motor.Crouch();
        
        actions.Sprint.started += ctx => motor.Sprint();
        actions.Sprint.canceled += ctx => motor.Sprint();
        actions.Pause.performed += ctx => pause.Pause();

        actions.Shoot.performed += ctx => interact.Shoot();
        actions.Reload.performed += ctx => interact.Reload();
        actions.ToggleWeapon.performed += ctx => interact.ToggleWeapon();
        pause.Resume();
    }
    void FixedUpdate()
    {
        //tell the motor to move based on move action value
        Vector2 movement = actions.Movement.ReadValue<Vector2>();
        motor.ProcessMove(movement);
    }
    void LateUpdate()
    {
        look.ProcessLook(actions.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
