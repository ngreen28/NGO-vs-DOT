using Unity.Entities;
using UnityEngine;

namespace spawnBall.Samples.SpawnBalls.Scripts
{
    public struct SpawnConfig : IComponentData
    {
        public Entity Prefab;
    }
    
    public class SpawnConfigAuthoring : MonoBehaviour
    {
        public GameObject prefab;

        class Baker : Baker<SpawnConfigAuthoring>
        {
            public override void Bake(SpawnConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SpawnConfig
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}