using Unity.NetCode;

namespace spawnBall.Samples.SpawnBalls.Scripts
{
    public struct RequestSpawnRpc : IRpcCommand
    {
        public int Count;
    }
}