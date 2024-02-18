using UnityEngine;

namespace PhysicModuleSystem2D
{
    [RequireComponent(typeof(PMController2D))]
    internal abstract class PhysicModule : MonoBehaviour
    {
        protected abstract void Modification(Rigidbody2D body, Vector2 direction);
        protected virtual void Awake()
        {
            PMController2D controller = GetComponent<PMController2D>();

            controller.ModificationAction += Modification;
        }
    }
}
