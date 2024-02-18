using PlayerSystems;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    public abstract class WeaponProperty : ItemProperty
    {
        [SerializeField] protected CastSystem2D.Abstract.CastComponent _attackCastGOPrefab;

        private GameObject _castGO;
        public void OnEquiped()
        {
            _castGO = Instantiate(_attackCastGOPrefab.gameObject, PlayerManager.SpawnArea.transform);
        }
        public void OnUnequiped()
        {
            Destroy(_castGO);
        }
    }
}
