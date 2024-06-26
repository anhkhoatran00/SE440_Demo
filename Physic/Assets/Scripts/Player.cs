using Cinemachine;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{

    private void Start()
    {
        if (!isLocalPlayer) return;
        var cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachine.Follow = transform;
        cinemachine.LookAt = transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Return(collision.gameObject);
        }
    }

    private void Return(GameObject obj)
    {
        ObjectPooling.Instance.ReturnOne(obj);
    }
}
