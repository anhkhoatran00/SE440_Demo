using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float jumpHeight = 5f;
    [SerializeField]
    private float jumpTime = 1f;  
    [SerializeField]
    private Vector3 jumpStep;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            transform.DOJump(transform.position + jumpStep, jumpHeight, 1, jumpTime);
        }
    }
}
