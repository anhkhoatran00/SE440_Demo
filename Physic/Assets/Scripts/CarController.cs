using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
