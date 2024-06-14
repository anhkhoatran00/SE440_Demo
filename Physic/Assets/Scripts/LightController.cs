using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LightController : MonoBehaviour
{
    public enum LightType
    {
        Front,
        Rear,
        Brake,
        Reverse
    }

    [System.Serializable]
    public class LightCar
    {
        public LightType type;
        public GameObject lightObject;
    }

    [SerializeField]
    private List<LightCar> lights = new List<LightCar>();

    [SerializeField]
    private float lightIntensityValue = 8f;

    [SerializeField]
    Material brakeMaterial, reverseMaterial;

    [SerializeField]
    private Color brakeColor, reverseColor;

    [SerializeField]
    private int brakeColorIntensity;

    [SerializeField]
    private int reverseColorIntensity;

    private CarController carController;

    private bool isReverse;
    private bool isLightOn;
    private bool isPressAgain;
    void Start()
    {
        carController = GetComponent<CarController>();
        //isPressAgain = false;
    }
    private void FixedUpdate()
    {
        if (carController.GasInput < 0f)
        {
            isReverse = true;
            reverseMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            isReverse = false;
            reverseMaterial.DisableKeyword("_EMISSION");
        }
        if (carController.IsBreaking)
        {
            brakeMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            brakeMaterial.DisableKeyword("_EMISSION");
        }
        brakeMaterial.SetColor("_EmissionColor", brakeColor * (carController.IsBreaking ? brakeColorIntensity : 1));
        reverseMaterial.SetColor("_EmissionColor", reverseColor * (carController.GasInput < 0 ? reverseColorIntensity : 1));
        isLightOn = carController.LightOn;
        /*  if (isLightOn && !isPressAgain)
          {
              isPressAgain = true;
          }
          else if(isPressAgain && isLightOn) 
          {
              isPressAgain = !isPressAgain;
          }*/
        if (isLightOn)
        {
            isPressAgain = !isPressAgain;
            FrontLightStatus(isPressAgain);
        }
        ReverseLightStatus(isReverse);
        BarkeLightStatus(carController.IsBreaking);
    }
    public void FrontLightStatus(bool isActive)
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Front)
            {
                light.lightObject.SetActive(isActive);
            }
        }
    }
    public void RearLightStatus(bool isActive)
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Rear)
            {
                light.lightObject.SetActive(isActive);
            }
        }
    }
    public void ReverseLightStatus(bool isActive)
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Reverse)
            {
                light.lightObject.SetActive(isActive);
            }
        }
    }
    public void BarkeLightStatus(bool isActive)
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Rear)
            {
                if (light.lightObject.TryGetComponent<LightIntensityControl>(out var LightIntensity) && isActive)
                {
                    // light.lightObject.GetComponent<LightIntensityControl>().ChangeIntensity(lightIntensity);
                    LightIntensity.ChangeIntensity(lightIntensityValue * 2);
                }
                else
                {
                    LightIntensity.ChangeIntensity(lightIntensityValue);
                }
                light.lightObject.SetActive(isActive);
            }
        }
    }
}


