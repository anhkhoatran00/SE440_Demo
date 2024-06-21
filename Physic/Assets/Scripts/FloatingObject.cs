using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{
    [SerializeField]
    private float underWaterDrag;
    [SerializeField]
    private float underWaterAngularDrag;
    [SerializeField]
    private float airDrag;
    [SerializeField]
    private float airAngularDrag;
    [SerializeField]
    private float waterPower;
    [SerializeField]
    private Transform[] floatPoints;

    private Rigidbody rb;
    private bool isUnderWater;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        int pointUnderWaterCount = 0;
        foreach (var point in floatPoints)
        {
            var diff = point.position.y - OceanManager.Instance.GetWaveHeight(point.position);
            if (diff < 0)
            {
                rb.AddForceAtPosition(Vector3.up * waterPower * Mathf.Abs(diff), point.position, ForceMode.Acceleration);
                pointUnderWaterCount++;
                if (!isUnderWater)
                {
                    isUnderWater = true;
                    SetStage(true);
                }
            }
        }


        if (isUnderWater && pointUnderWaterCount == 0)
        {
            isUnderWater = false;
            SetStage(false);
        }
    }
    private void SetStage(bool underWater)
    {
        if (underWater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }

}
