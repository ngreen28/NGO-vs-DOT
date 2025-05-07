using Unity.Netcode;
using UnityEngine;

namespace Samples.SpawnPlayer.Scripts
{
    public class PlayerControllerNGO : NetworkBehaviour
    {
        public float speed = 5f;
        private Renderer rend;

        public Color ownerColor = Color.red; // Color fijo para el jugador local

        void Start()
        {
            rend = GetComponent<Renderer>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (rend == null)
                rend = GetComponent<Renderer>();

            // Instancia el material para evitar que todos compartan el mismo
            rend.material = new Material(rend.material);

            // Asigna color según el tipo de jugador
            if (IsOwner)
            {
                rend.material.color = ownerColor;
            }
            else
            {
                rend.material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            }
        }

        void Update()
        {
            if (!IsOwner) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }
    }
}