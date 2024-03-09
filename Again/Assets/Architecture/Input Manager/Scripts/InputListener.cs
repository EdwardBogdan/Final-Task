using UnityEngine;
using UnityEngine.Events;

namespace InputControll
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private InputMap _mapTrigger;

        [SerializeField] private UnityEvent _onMapSelected;
        [SerializeField] private UnityEvent _onMapDeselected;
        private void Awake()
        {
            InputManager.ListenChangeMap(OnChange, true);
        }

        private void OnDestroy()
        {
            InputManager.ListenChangeMap(OnChange, false);
        }

        private void OnChange(InputMap map, InputMap oldMap)
        {
            if (map == _mapTrigger) 
                _onMapSelected?.Invoke();
            else if(oldMap == _mapTrigger) 
                _onMapDeselected?.Invoke();
        }
    }
}
