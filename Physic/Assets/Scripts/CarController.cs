using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CarController;

public class CarController : MonoBehaviour
{
    public enum WheelType
    {
        Front,
        Rear
    }
    [System.Serializable]
    public struct Wheel
    {
        public WheelType type;
        public WheelCollider collider;
        public Transform transform;
    }

    [SerializeField]
    private List<Wheel> wheels = new List<Wheel>();

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    [SerializeField]
    private float motorForce, breakForce, maxSteerAngle;
    [SerializeField]
    Material brakeMaterial;
    [SerializeField]
    private Color brakeColor;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {

      /*  wheels[0].collider.motorTorque = verticalInput * motorForce;
        wheels[1].collider.motorTorque = verticalInput * motorForce;*/
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                wheel.collider.motorTorque = verticalInput * motorForce;
            }
        }
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        if (isBreaking)
        {
            brakeMaterial.SetColor("_EmissionColor", brakeColor * 3);
        }
        else
        {
            brakeMaterial.SetColor("_EmissionColor", brakeColor * 1);
        }
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
        currentSteerAngle = maxSteerAngle * horizontalInput;
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
