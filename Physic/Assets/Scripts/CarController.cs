using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using static CarController;


[System.Serializable]
public class Wheel
{
    public WheelType wheelType;
    public WheelCollider collider;
    public Transform transform;
}

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public enum WheelType
    {
        Front,
        Rear
    }
    public enum CarDriveType
    {
        FrontWheelDrive,
        RearWheelDrive,
        FourWheelDrive
    }
    internal enum SpeedType
    {
        KPH,
        MPH
    }


    [SerializeField]
    private CarDriveType carDriveType;

    [SerializeField]
    private List<Wheel> wheels = new List<Wheel>();

    [SerializeField]
    private float motorForce, breakForce, maxSteerAngle;

    [SerializeField]
    private SpeedType speedType;

    [SerializeField]
    private Vector3 centerOfMass;

    [SerializeField]
    private TextMeshProUGUI speedTextKPH, speedTextMPH;

    private Rigidbody rb;
    private InputHandler inputHandler;

    private const float milesConvert = 0.6213711922f;
    private const float kilometersConvert = 3.6f;
    private float speed;
    public float SteerInput { get; private set; }
    public float GasInput { get; private set; }
    public bool IsBreaking { get; private set; }
    public bool LightOn { get; private set; }

    private float currentSteerAngle, currentbreakForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();

        rb.centerOfMass = centerOfMass;
    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        CaculateSpeed();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void CaculateSpeed()
    {

        float speed = rb.velocity.magnitude;
        switch (speedType)
        {
            case SpeedType.MPH:

                speed *= milesConvert;
                speedTextMPH.text = Mathf.FloorToInt(speed).ToString();
                break;

            case SpeedType.KPH:
                speed *= kilometersConvert;
                speedTextKPH.text = Mathf.FloorToInt(speed).ToString();
                break;
        }
    }
    private void GetInput()
    {
        Vector2 axisMovement = inputHandler.GetInputMovement();
        // Steering Input
        SteerInput = axisMovement.x;
        // Acceleration Input
        GasInput = axisMovement.y;

        // Breaking Input
        IsBreaking = inputHandler.GetBrakeInput();

        LightOn = inputHandler.GetLightInput();
    }
    private void HandleMotor()
    {
        foreach (var wheel in wheels)
        {

            switch (carDriveType)
            {
                case CarDriveType.FrontWheelDrive:
                    if (wheel.wheelType == WheelType.Front)
                    {
                        wheel.collider.motorTorque = GasInput * motorForce;
                    }
                    break;
                case CarDriveType.RearWheelDrive:
                    if (wheel.wheelType == WheelType.Rear)
                    {
                        wheel.collider.motorTorque = GasInput * motorForce;
                    }
                    break;
                case CarDriveType.FourWheelDrive:
                    wheel.collider.motorTorque = GasInput * motorForce;
                    break;
            }
        }
        currentbreakForce = IsBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = currentbreakForce;
        }
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * SteerInput;
        foreach (var wheel in wheels)
        {
            if (wheel.wheelType == WheelType.Front)
            {
                wheel.collider.steerAngle = currentSteerAngle;
            }
        }
    }

    private void UpdateWheels()
    {
        foreach (var wheel in wheels)
        {
            UpdateSingleWheel(wheel.collider, wheel.transform);
        }
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}

