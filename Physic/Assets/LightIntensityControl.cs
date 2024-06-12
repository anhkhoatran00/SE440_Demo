using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityControl : MonoBehaviour
{
    [SerializeField]
    private Light light;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeIntensity(float intensityValue)
    {
        light.intensity = intensityValue;
    }
}
