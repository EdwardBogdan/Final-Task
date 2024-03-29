using UnityEngine;
using UnityEngine.Events;

namespace Overlay.UIManagment.Windows
{
    public class WindowGameOver : UnitUI
    {
        [SerializeField] private UnityEvent _onMainMenu;
        [SerializeField] private UnityEvent _onLoad;

        public void OnLoad()
        {
            _onLoad?.Invoke();
        }

        public void OnMenu()
        {
            _onMainMenu?.Invoke();
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}
