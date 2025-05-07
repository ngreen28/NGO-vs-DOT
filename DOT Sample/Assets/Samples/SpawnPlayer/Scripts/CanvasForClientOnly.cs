using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace spawnPlayer.Samples.SpawnPlayer.Scripts
{
    public class CanvasForClientOnly : MonoBehaviour
    {
        public GameObject CanvasUI;

        void Update()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;

            var entityManager = world.EntityManager;
            var query = entityManager.CreateEntityQuery(typeof(NetworkStreamInGame));

            if (query.CalculateEntityCount() > 0)
            {
                SetAllChildrenActive(CanvasUI, true);
                enabled = false; // No need to check again
            }
        }

        private void SetAllChildrenActive(GameObject parent, bool active)
        {
            parent.SetActive(active);
            foreach (Transform child in parent.transform)
            {
                SetAllChildrenActive(child.gameObject, active);
            }
        }
    }
}