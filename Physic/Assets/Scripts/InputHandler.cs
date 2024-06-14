using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private Vector2 axisMovement;

    private bool brakeInput;

    private bool lightInput;
    public Vector2 GetInputMovement() => axisMovement;
    public bool GetBrakeInput() => brakeInput;
    public bool GetLightInput() => lightInput;
   

    private void Awake()
    {

    }
    private void Start()
    {


    }
    private void Update()
    {

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        axisMovement = context.ReadValue<Vector2>();
    }
    public void OnBrakeInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                brakeInput = true;
                break;
            case InputActionPhase.Performed:
                brakeInput = true;
                break;
            case InputActionPhase.Canceled:
                brakeInput = false;
                break;
        }
    }
    public void OnLightInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                lightInput = true;
                break;
           /* case { phase: InputActionPhase.Performed }:
                lightInput = true;
                break;*/
            case InputActionPhase.Canceled:
                lightInput = false;
                break;
        }
    }
}
