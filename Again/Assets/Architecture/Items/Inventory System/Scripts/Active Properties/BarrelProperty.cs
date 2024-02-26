using Creature.Player.Ref;
using SpawnSystem;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "BarrelProperty", menuName = "Inventory/Items/ActiveProperty/Barrel")]
    public class BarrelProperty : ItemProperty
    {
        [SerializeField] private GameObject _prefabBarrel;
        [SerializeField] private Vector2 _posThrow;
        public void OnDrop()
        {
            Transform spawnAreaTransform = PlayerRefManager.SpawnArea.transform;
            Transform playerTransform = PlayerRefManager.Player.transform;

            Vector3 pos = spawnAreaTransform.position;

            pos.x += _posThrow.x * playerTransform.localScale.x;
            pos.y += _posThrow.y * playerTransform.localScale.y;

            Spawning.Spawn(_prefabBarrel, pos);
        }
    }
}
