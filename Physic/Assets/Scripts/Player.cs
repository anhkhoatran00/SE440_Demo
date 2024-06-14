using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

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
