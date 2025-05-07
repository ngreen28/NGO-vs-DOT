using UnityEngine;
using Unity.Netcode;

namespace Samples.SpawnBalls.Scripts
{
    public class Spawner : NetworkBehaviour
    {
        public static Spawner Instance { get; private set; }

        [SerializeField] private GameObject prefabToSpawn;

        private int spawnCount = 1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void OnSpawnButtonClicked()
        {
            if (IsClient)
                RequestSpawnServerRpc(spawnCount);

            spawnCount *= 2; // Duplicar para la próxima vez
        }

        [ServerRpc]
        private void RequestSpawnServerRpc(int count, ServerRpcParams rpcParams = default)
        {
            if (prefabToSpawn == null)
            {
                Debug.LogWarning("No prefab assigned to Spawner.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-5f, 5f),
                    1f,
                    Random.Range(-5f, 5f)
                );

                GameObject go = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
            }

            Debug.Log($"Spawned {count} prefab(s)");
        }
    }
}