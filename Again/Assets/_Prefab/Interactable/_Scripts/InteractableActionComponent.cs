using UnityEngine;

namespace Interactable
{
    public class InteractableActionComponent : MonoBehaviour
    {
        public void InteractAction(GameObject _object)
        {
            if (!_object.TryGetComponent(out InteractableComponent component)) return;

            component.Interact();
        }
    }
}
