using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using static CarController;

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
    private TextMeshProUGUI speedTextKPH, speedTextMPH;

    [SerializeField]
    Material brakeMaterial, reverseMaterial;
    [SerializeField]
    private Color brakeColor, reverseColor;
    [SerializeField]
    private int brakeColorIntensity;
    [SerializeField]
    private int reverseColorIntensity;

    private Rigidbody rb;
    private LightController lightController;

    private const float milesConvert = 0.6213711922f;
    private const float kilometersConvert = 3.6f;
    private float speed;
    private float steerInput, gasInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking, isReverse;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lightController = GetComponent<LightController>();
    }

    // Update is called once per frame
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
        LightControl();
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
        // Steering Input
        steerInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        gasInput = Input.GetAxis("Vertical");

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
                wheel.collider.motorTorque = gasInput * motorForce;
            }
        }
        currentbreakForce = isBreaking ? breakForce : 0f;
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
        currentSteerAngle = maxSteerAngle * steerInput;
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

    private void LightControl()
    {

        if (gasInput < 0f)
        {
            isReverse = true;
            reverseMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            isReverse = false;
            reverseMaterial.DisableKeyword("_EMISSION");
        }
        brakeMaterial.SetColor("_EmissionColor", brakeColor * (isBreaking ? brakeColorIntensity : 1));
        reverseMaterial.SetColor("_EmissionColor", reverseColor * (gasInput < 0 ? reverseColorIntensity : 1));
        lightController.ReverseLightStatus(isReverse);
        lightController.BarkeLightStatus(isBreaking);
    }
}
[System.Serializable]
public class Wheel
{
    public WheelType type;
    public WheelCollider collider;
    public Transform transform;
}
