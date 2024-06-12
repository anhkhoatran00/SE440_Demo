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

    private float steeringInput, gasInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    [SerializeField]
    private Mybutton gasPedal, brakePedal, reversePedal;


    [SerializeField]
    private float motorForce, breakForce, maxSteerAngle;
    [SerializeField]
    Material brakeMaterial;
    [SerializeField]
    private Color brakeColor;
    InputHandler inputHandler;


    // Start is called before the first frame update
    void Start()
    {
        gasPedal = GameObject.FindWithTag("GasButton").GetComponent<Mybutton>();
        brakePedal = GameObject.FindWithTag("BrakeButton").GetComponent<Mybutton>();
        reversePedal = GameObject.FindWithTag("ReverseButton").GetComponent<Mybutton>();

        inputHandler = GetComponent<InputHandler>();
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
        /* horizontalInput = Input.GetAxis("Horizontal");*/

        Vector2 axisMovement = inputHandler.GetInputMovement();

        steeringInput = SimpleInput.GetAxis("Horizontal");

        if (!gasPedal.isPressed || !reversePedal)
        {
            gasInput = 0;
        }

        // Acceleration Input
        /*verticalInput = Input.GetAxis("Vertical");*/
        steeringInput = axisMovement.x;

        gasInput = axisMovement.y;
      
        gasInput += gasPedal.dampenPress;
       
        gasInput -= reversePedal.dampenPress;

        // Breaking Input
        // isBreaking = Input.GetKey(KeyCode.Space);
        isBreaking = brakePedal.isPressed;
    }
    private void HandleMotor()
    {

        /*  wheels[0].collider.motorTorque = verticalInput * motorForce;
          wheels[1].collider.motorTorque = verticalInput * motorForce;*/
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                wheel.collider.motorTorque = gasInput * motorForce;
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
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * steeringInput;
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
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
