using System.Collections.Generic;
using UnityEngine;



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
    public class Light
    {
        public LightType type;
        public GameObject lightObject;
        public Light light;
    }

    [SerializeField]
    private List<Light> lights = new List<Light>();

    [SerializeField]
    private float lightIntensityValue = 8f;

    public void FrontLightStatus()
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Front)
            {
                if (light.lightObject.activeInHierarchy)
                {
                    light.lightObject.SetActive(false);

                }
                else
                {
                    light.lightObject.SetActive(true);

                }
            }
        }
    }
    public void RearLightStatus()
    {
        foreach (var light in lights)
        {
            if (light.type == LightType.Rear)
            {
                if (light.lightObject.activeInHierarchy)
                {
                    light.lightObject.SetActive(false);
                }
                else
                {
                    light.lightObject.SetActive(true);
                }
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
                    LightIntensity.ChangeIntensity(lightIntensityValue*2);
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

