using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2 axisMovement;
    public bool BrakeInput { get; private set; }
    public Vector2 GetInputMovement() => axisMovement;
    public void OnMove(InputAction.CallbackContext context)
    {
        axisMovement = context.ReadValue<Vector2>();
    }
    public void OnBrake(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            BrakeInput = true;
        }
        if (context.canceled)
        {
            BrakeInput = false;
        }
    }
}
