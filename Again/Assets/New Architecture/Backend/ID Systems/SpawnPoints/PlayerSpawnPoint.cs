using Creature.Player.Ref;
using SystemID;
using UnityEngine;

namespace SpawnSystem
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private IDComponent _id;

        private void Start()
        {
            if (SpawnManager.SpawnID == _id.ID)
            {
                PlayerRefManager.Player.transform.position = transform.position;
            }
        }
    }
}
