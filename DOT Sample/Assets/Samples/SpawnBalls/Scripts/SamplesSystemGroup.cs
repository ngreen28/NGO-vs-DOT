using Unity.Entities;

namespace spawnBall.Samples.SpawnBalls.Scripts
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation |
                       WorldSystemFilterFlags.ServerSimulation)]
    public partial class BallSamplesSystemGroup : ComponentSystemGroup
    {
    }
}
