using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControll.KeyReaders
{
    public class PlayerKeyReader : MonoBehaviour
    {
        #region Movement
        public static event Action<Vector2> MovementCallback;
        public void OnKeyMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            MovementCallback?.Invoke(vector);
        }
        #endregion

        #region Select Item
        public static event Action<int> SelectItemCallback;
        public void OnKeySelectItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                int index = (int)context.ReadValue<float>() - 1;
                SelectItemCallback?.Invoke(index);
            }
        }
        #endregion

        #region Use item
        public static event Action UseItemCallback;
        public void OnKeyUseEquipment(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                UseItemCallback?.Invoke();
            }
        }
        #endregion

        #region Attack
        public static event Action AttackCallback;
        public void OnKeyAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (Creature.Player.Arming.WeaponManager.Armed) 
                    AttackCallback?.Invoke();
            }
        }
        #endregion

        #region Interact
        public static event Action InteractCallback;
        public void OnKeyInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InteractCallback?.Invoke();
            }
        }
        #endregion

        public void OnKeyPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {

            }
        }
    }
}
