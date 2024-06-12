using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace Game
{
    public class MyObjectSpawner : ObjectSpawner
    {
        [Header("Custom Spawn")]
        [SerializeField] private GameObject objectSpawn;

        private GameObject _spawned;
        protected override bool CustomSpawnGameObject(Vector3 spawnPoint, Vector3 spawnNormal)
        {
            if (_spawned != null)
            {
                return true;
            }

            _spawned = Instantiate(objectSpawn);
            _spawned.transform.position = spawnPoint;
            return true;
        }
    }
}