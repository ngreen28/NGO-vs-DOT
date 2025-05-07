using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace spawnBall.Samples.SpawnBalls.Scripts
{
    [BurstCompile]
    [UpdateInGroup(typeof(BallSamplesSystemGroup))]
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    public partial struct HandleSpawnRpcSystem : ISystem
    {
        private Random _random;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnConfig>();
            _random = new Random((uint)DateTime.Now.Ticks);
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var config = SystemAPI.GetSingleton<SpawnConfig>();

            foreach (var (rpc, entity) in SystemAPI
                .Query<RequestSpawnRpc>()
                .WithEntityAccess()
                .WithAll<ReceiveRpcCommandRequest>())
            {
                for (int i = 0; i < rpc.Count; i++)
                {
                    Entity spawned = state.EntityManager.Instantiate(config.Prefab);

                    // Posición aleatoria con mayor rango y altura variable
                    float3 pos = new float3(
                        _random.NextFloat(-10f, 10f),  // Rango más amplio en X
                        _random.NextFloat(0.5f, 3f),   // Altura aleatoria entre 0.5 y 3
                        _random.NextFloat(-10f, 10f)   // Rango más amplio en Z
                    );

                    ecb.SetComponent(spawned, new LocalTransform
                    {
                        Position = pos,
                        Rotation = quaternion.identity,
                        Scale = 1f
                    });
                }

                ecb.DestroyEntity(entity);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}
