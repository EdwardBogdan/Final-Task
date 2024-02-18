using PhysicModuleSystem2D;
using UnityEngine;
using CastSystem2D.Abstract;

namespace PlayerSystems
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private PMController2D _controller;
        [SerializeField] private CastComponent _interactCast;
        [SerializeField] private GameObject _spawnArea;

        public static GameObject PlayerGO { get; private set; }
        public static GameObject SpawnArea { get; private set; }
        public static PMController2D PhysicController { get; private set; }

        private static PlayerManager I;
        public static void CastInteract() => I._interactCast.Cast();

        private void Awake()
        {
            if (PlayerGO != null)
            {
                Destroy(_playerGO);
                return;
            }
            I = this;

            PlayerGO = _playerGO;

            PhysicController = _controller;
        }
    }
}