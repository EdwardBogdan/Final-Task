using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControll.KeySubscriber
{
    public class DialogKeyReader : MonoBehaviour
    {
        #region Skip
        public static event Action SkipCallback;
        public void OnKeySkip(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SkipCallback?.Invoke();
            }
        }
        #endregion
    }
}
