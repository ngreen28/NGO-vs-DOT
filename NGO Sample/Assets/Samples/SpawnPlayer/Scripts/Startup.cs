using Unity.Netcode;
using UnityEngine;

namespace Samples.SpawnPlayer.Scripts
{
    public class Startup : MonoBehaviour
    {
        void OnGUI()
        {
            if (NetworkManager.Singleton == null)
            {
                GUILayout.Label("NetworkManager not found.");
                return;
            }

            if (!NetworkManager.Singleton.IsListening)
            {
                if (GUILayout.Button("Host"))
                    NetworkManager.Singleton.StartHost();

                if (GUILayout.Button("Client"))
                    NetworkManager.Singleton.StartClient();
            }
            else
            {
                GUILayout.Label("Running as: " + 
                                (NetworkManager.Singleton.IsServer ? "Host/Server" : "Client"));
            }
        }
    }
}