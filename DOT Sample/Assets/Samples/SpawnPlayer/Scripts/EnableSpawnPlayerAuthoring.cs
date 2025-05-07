using Unity.Entities;
using UnityEngine;

namespace spawnPlayer.Samples.SpawnPlayer.Scripts
{
    public struct EnableSpawnPlayer : IComponentData { }

    [DisallowMultipleComponent]
    public class EnableSpawnPlayerAuthoring : MonoBehaviour
    {
        class EnableSpawnPlayerBaker : Baker<EnableSpawnPlayerAuthoring>
        {
            public override void Bake(EnableSpawnPlayerAuthoring authoring)
            {
                EnableSpawnPlayer component = default(EnableSpawnPlayer);
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, component);
            }
        }
    }
}