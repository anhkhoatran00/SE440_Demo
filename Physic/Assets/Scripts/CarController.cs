using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using static CarController;


[System.Serializable]
public class Wheel
{
    public WheelType type;
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

    [SerializeField]
    private ParticleSystem smokeParticle;

    [SerializeField]
    private List<Wheel> wheels = new List<Wheel>();

    [SerializeField]
    private float motorForce, breakForce, maxSteerAngle;

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
        speed = rb.velocity.magnitude * kilometersConvert;
        speedTextKPH.text = Mathf.FloorToInt(speed).ToString();
        speed *= milesConvert;
        speedTextMPH.text = Mathf.FloorToInt(speed).ToString();
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

        /*  wheels[0].collider.motorTorque = verticalInput * motorForce;
          wheels[1].collider.motorTorque = verticalInput * motorForce;*/
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                wheel.collider.motorTorque = GasInput * motorForce;
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
        /* wheels[0].collider.brakeTorque = currentbreakForce;
         wheels[1].collider.brakeTorque = currentbreakForce;
         wheels[2].collider.brakeTorque = currentbreakForce;
         wheels[3].collider.brakeTorque = currentbreakForce;*/
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * SteerInput;
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                wheel.collider.steerAngle = currentSteerAngle;
            }
        }
        /*   wheels[0].collider.steerAngle = currentSteerAngle;
           wheels[1].collider.steerAngle = currentSteerAngle;*/
    }

    private void UpdateWheels()
    {
        foreach (var wheel in wheels)
        {
            UpdateSingleWheel(wheel.collider, wheel.transform);
        }
        /* UpdateSingleWheel(wheels[0].collider, wheels[0].transform);
         UpdateSingleWheel(wheels[1].collider, wheels[1].transform);
         UpdateSingleWheel(wheels[2].collider, wheels[2].transform);
         UpdateSingleWheel(wheels[3].collider, wheels[3].transform);*/
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

