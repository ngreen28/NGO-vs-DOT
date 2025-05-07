using Unity.Entities;
using Unity.NetCode;

namespace spawnPlayer.Samples.SpawnPlayer.Scripts
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation |
                       WorldSystemFilterFlags.ServerSimulation)]
    public partial class PlayerSamplesSystemGroup : ComponentSystemGroup
    {
    }

    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation,
        WorldSystemFilterFlags.ClientSimulation)]
    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial class PlayerSamplesInputSystemGroup : ComponentSystemGroup
    {
    }

    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial class PlayerSamplesPredictedSystemGroup : ComponentSystemGroup
    {
    }
}