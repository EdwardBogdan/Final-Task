using UnityEngine;

namespace Creature.Player.Ref
{
    public class PlayerRefs : MonoBehaviour
    {
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private Transform _spawnArea;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            if (PlayerRefManager.Player != null)
            {
                Destroy(_playerGO);
                return;
            }

            PlayerRefManager.Player = _playerGO;
            PlayerRefManager.SpawnArea = _spawnArea;
            PlayerRefManager.Animator = _animator;
        }
    }
}