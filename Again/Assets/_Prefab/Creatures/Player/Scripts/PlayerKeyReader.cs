using PlayerSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeyReader : MonoBehaviour
{
    public void OnKeyMovement(InputAction.CallbackContext context)
    {
        Vector2 vector = context.ReadValue<Vector2>();
        PlayerManager.PhysicController.SetDirection(vector);
    }

    public void OnKeyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
    }
    public void OnKeyUseEquipment(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
    }
    public void OnKeyChooseEquipment(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int index = (int)context.ReadValue<float>();

            //EquipmentManager.ChooseItem(index - 1);
        }
    }
    public void OnKeyInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerManager.CastInteract();
        }
    }

    public void OnKeyPause(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            
        }
    }
}
