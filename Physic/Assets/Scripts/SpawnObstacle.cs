using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 1f;

    private float _timer;

    [SerializeField]
    private float radius = 5f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer -= spawnInterval;
            Spawn();
        }

    }

    private void Spawn()
    {
        if (!ObjectPooling.Instance.CanSpawn()) return;

        var obj = ObjectPooling.Instance.PickOne(transform) as GameObject;

        var pos = Random.insideUnitSphere * radius;

        pos.y = Mathf.Abs(pos.y);

        obj.transform.position = pos;

    }

}
