using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControll
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;

        private void Awake()
        {
            OnChangeMap();

            InputManager.ListenChangeMap(OnChangeMap, true);
        }
        private void OnDestroy()
        {
            InputManager.ListenChangeMap(OnChangeMap, false);
        }

        private void OnChangeMap()
        {
            var map = InputManager.Map;
            string key = InputMapKeys.GetKey(map);
            _input.SwitchCurrentActionMap(key);
        }
    }
}
