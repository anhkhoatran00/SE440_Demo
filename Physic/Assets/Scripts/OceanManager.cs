using DefauleNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : SingleTon<OceanManager>
{
    [SerializeField]
    private GameObject ocean;
    [SerializeField]
    private float wavePower = 2f;

    private Material oceanMat;
    private Texture2D oceanTex;
    private void Start()
    {
        SetValue();
        Debug.Log("Ocean material: " + oceanMat);
    }
    private void Update()
    {
        OnChangeWavePower();
    }
    private void SetValue()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        oceanTex = (Texture2D)oceanMat.GetTexture("_mainTex");
    }
    private void OnChangeWavePower()
    {
        if (!Application.isPlaying) return;

        if (oceanMat != null)
        {
            SetValue();
        }
        oceanMat.SetFloat("_wavePower", wavePower);
    }
    public float GetWaveHeight(Vector3 point)
    {
        float waveHeight = oceanTex.GetPixelBilinear(point.x * Time.deltaTime, point.z * Time.deltaTime).g * wavePower * transform.position.y;
        return waveHeight;
    }
}
