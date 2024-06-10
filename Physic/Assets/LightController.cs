using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    /*[SerializeField]
    GameObject LeftFront_Light, RightFront_Light, LeftRear_Light, RightRear_Light;*/
    [SerializeField]
    GameObject Light;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LightControl()
    {
        Light.SetActive(LightStatus() ? false : true);
    }
    private bool LightStatus() => Light.activeInHierarchy;
    
}
