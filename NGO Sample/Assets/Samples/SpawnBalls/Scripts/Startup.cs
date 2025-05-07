using UnityEngine;
using Unity.Netcode;

namespace Samples.SpawnBalls.Scripts
{
    public class Startup : MonoBehaviour
    {
        private const int ButtonWidth = 200;
        private const int ButtonHeight = 40;

        private void OnGUI()
        {
            int y = 10;

            if (NetworkManager.Singleton == null)
            {
                GUI.Label(new Rect(10, y, ButtonWidth, ButtonHeight), "NetworkManager not found.");
                return;
            }

            if (!NetworkManager.Singleton.IsListening)
            {
                if (GUI.Button(new Rect(10, y, ButtonWidth, ButtonHeight), "Start Host"))
                    NetworkManager.Singleton.StartHost();

                y += ButtonHeight + 10;

                if (GUI.Button(new Rect(10, y, ButtonWidth, ButtonHeight), "Start Client"))
                    NetworkManager.Singleton.StartClient();
            }
            else
            {
                string mode = NetworkManager.Singleton.IsServer ? "Host/Server" : "Client";
                GUI.Label(new Rect(10, y, ButtonWidth, ButtonHeight), $"Running as: {mode}");

                y += ButtonHeight + 10;

                if (GUI.Button(new Rect(10, y, ButtonWidth, ButtonHeight), "Spawn"))
                {
                    if (Spawner.Instance != null)
                        Spawner.Instance.OnSpawnButtonClicked();
                    else
                        Debug.LogWarning("Spawner instance not found.");
                }
            }
        }
    }
}