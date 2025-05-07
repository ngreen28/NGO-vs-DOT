using Unity.Entities;
using UnityEngine;

namespace bootstrap.Bootstrap.GoInGame
{
    public struct EnableGoInGame : IComponentData { }

    [DisallowMultipleComponent]
    public class EnableGoInGameAuthoring : MonoBehaviour
    {
        class Baker : Baker<EnableGoInGameAuthoring>
        {
            public override void Bake(EnableGoInGameAuthoring authoring)
            {
                EnableGoInGame component = default;
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, component);
            }
        }
    }
}