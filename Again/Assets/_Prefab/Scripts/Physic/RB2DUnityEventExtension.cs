using UnityEngine;

namespace PhysicCustom.Components
{
    public class RB2DUnityEventExtension : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        public void ResetVelocity()
        {
            if (_rb != null)
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }
}