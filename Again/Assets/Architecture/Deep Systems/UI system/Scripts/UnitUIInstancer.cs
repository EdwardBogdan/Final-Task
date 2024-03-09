using UnityEngine;

namespace GameSystem.Overlay
{
    public class UnitUIInstancer : MonoBehaviour
    {
        [SerializeField] private UnitUI _unit;

        public void Inst()
        {
            Instantiate(_unit, ScreenManager.OverlayCanvas.transform);
        }
    }
}
