using UnityEngine;

namespace Overlay.UIManagment
{
    public abstract class UnitUI : MonoBehaviour
    {
        #region Controll
        public void OpenUI(UnitUI unit)
        {
            UIController.OpenUI(unit);
        }

        public void CloseUI(UnitUI unit)
        {
            UIController.CloseUI(unit);
        }

        public void CloseUISelf()
        {
            UIController.CloseUI(this);
        }
        #endregion
    }
}
