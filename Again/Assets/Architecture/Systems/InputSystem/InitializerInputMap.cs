using UnityEngine;

namespace GameSystem.Input
{
    public class InitializerInputMap : MonoBehaviour
    {
        public void ChangeInputMap(string mapName)
        {
            InputSystem.DefaultMap.Value = mapName;
        }
    }
}
