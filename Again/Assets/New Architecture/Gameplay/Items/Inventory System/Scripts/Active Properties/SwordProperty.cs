using Creature.Player.Ref;
using SpawnSystem;
using UnityEngine;
using Projectiles;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "SwordProperty", menuName = "Inventory/Items/Property/Sword")]
    public class SwordProperty : ItemProperty
    {
        [SerializeField] private ProjectileX _prefabThrow;
        [SerializeField] private Vector2 _posThrow;

        public void OnThrow()
        {
            Transform spawnAreaTransform = PlayerRefManager.SpawnArea.transform;
            Transform playerTransform = PlayerRefManager.Player.transform;

            Vector3 pos = spawnAreaTransform.position;

            pos.x += _posThrow.x * playerTransform.localScale.x;
            pos.y += _posThrow.y * playerTransform.localScale.y;

            ProjectileX projectile = Spawning.Spawn(_prefabThrow, pos);
            
            bool invert = playerTransform.localScale.x < 0;

            projectile.SetInvert(invert);
        }
    }
}