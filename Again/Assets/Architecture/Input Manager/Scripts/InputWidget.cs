using UnityEngine;

namespace InputControll
{
    public class InputWidget : MonoBehaviour
    {
        [SerializeField] private InputMap _map;
        [SerializeField, EditorAttributes.EditorReadOnly] private InputMap _oldMap;
        public void ChangeInputMap()
        {
            _oldMap = InputManager.Map;
            InputManager.Map = _map;
        }
        public void ReturnInputMap()
        {
            InputManager.Map = _oldMap;
        }
        public void DropInputMap()
        {
            InputManager.Map = InputMap.Empty;
        }
    }
}
