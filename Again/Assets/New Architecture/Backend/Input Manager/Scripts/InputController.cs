using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControll
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;

        private void Awake()
        {
            OnChangeMap(InputManager.Map, default);

            InputManager.ListenChangeMap(OnChangeMap, true);
        }
        private void OnDestroy()
        {
            InputManager.ListenChangeMap(OnChangeMap, false);
        }

        private void OnChangeMap(InputMap map, InputMap oldMap)
        {
            string key = InputMapKeys.GetKey(map);
            _input.SwitchCurrentActionMap(key);
        }
    }
}
