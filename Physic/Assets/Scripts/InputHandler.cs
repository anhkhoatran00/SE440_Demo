using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private Vector2 axisLook;

    private Vector2 axisMovement;

    private bool brakeInput;

    private bool lightInput;

    public Vector2 GetInputLook() => axisLook;
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
        if (context.started)
        {
            brakeInput = true;
        }
        if (context.canceled)
        {
            brakeInput = false;
        }
    } 
    public void OnLightInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lightInput = true;
        }
        if (context.canceled)
        {
            lightInput = false;
        }
    }
}
