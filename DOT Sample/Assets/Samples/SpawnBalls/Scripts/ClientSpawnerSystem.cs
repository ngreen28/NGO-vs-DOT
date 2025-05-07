using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace spawnBall.Samples.SpawnBalls.Scripts
{
    [UpdateInGroup(typeof(BallSamplesSystemGroup))]
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    public partial class ClientSpawnerSystem : SystemBase
    {
        private int spawnCount = 1;

        protected override void OnCreate()
        {
            RequireForUpdate<NetworkId>();
        }

        protected override void OnUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
                return;

            var rpcEntity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(rpcEntity, new RequestSpawnRpc { Count = spawnCount });
            EntityManager.AddComponentData(rpcEntity, new SendRpcCommandRequest
            {
                TargetConnection = SystemAPI.GetSingletonEntity<NetworkId>() // Updated here
            });

            Debug.Log($"Requesting spawn of {spawnCount} entities");

            spawnCount *= 2; // Duplicar para la próxima vez
        }
    }
}