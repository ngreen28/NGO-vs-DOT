using Unity.NetCode;

namespace bootstrap.Bootstrap
{
    // This is a setup for dealing with a frontend menu, where the user wants control over client and server world creation.
    // We support:
    // - Starting a game into the Frontend scene, allowing the user to choose:
    //      - A 'client hosted' setup.
    //      - A 'connect to existing server via IP' setup.
    //      - A 'auto-load scene via `-scene XXX` commandline arg.
    // - While starting from any other scene will preserve the existing 'auto-connect' quick-start flow.
    
    [UnityEngine.Scripting.Preserve]
    public class NetCodeBootstrap : ClientServerBootstrap
    {
        public override bool Initialize(string defaultWorldName)
        {
            AutoConnectPort = 7979; // Enable auto connect
            return base.Initialize(defaultWorldName); // Use the regular bootstrap
        }
    }
}
