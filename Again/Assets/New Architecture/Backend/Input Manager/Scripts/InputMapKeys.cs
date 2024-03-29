using System;
using UnityEngine;

namespace InputControll
{
    [CreateAssetMenu(fileName = "InputMapKeys", menuName = "Player/Input/MapKeys")]
    public class InputMapKeys : ScriptableObject
    {
        [SerializeField] private KeyRef[] _keys;

        internal static string GetKey(InputMap map)
        {
            KeyRef[] keys = InputManager.Keys._keys;

            foreach (var item in keys)
            {
                if (item._map == map)
                {
                    return item._key;
                }
            }

            return "Empty";
        }

        [Serializable]
        private class KeyRef
        {
            [SerializeField] public InputMap _map;
            [SerializeField] public string _key;
        }
    }
}
