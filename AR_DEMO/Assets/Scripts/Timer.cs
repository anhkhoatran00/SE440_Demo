using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;
    private float elapsedTime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int secounds = Mathf.FloorToInt(elapsedTime % 60);
        float milliseconds = elapsedTime * 1000f % 1000f;
        textMeshProUGUI.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, secounds, (int)milliseconds);
    }
}
