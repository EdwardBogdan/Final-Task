using UnityEngine;

namespace PhysicCustom.Components
{
    public class RB2DConstraintsController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;

        private RigidbodyConstraints2D Constraints { get => _rb.constraints; set => _rb.constraints = value; }

        public void FreezePositiontX(bool freeze)
        {
            if (freeze) Constraints |= RigidbodyConstraints2D.FreezePositionX;

            else Constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        public void FreezePositionY(bool freeze)
        {
            if (freeze) Constraints |= RigidbodyConstraints2D.FreezePositionY;

            else Constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }

        public void FreezeRotationZ(bool freeze)
        {
            if (freeze) Constraints |= RigidbodyConstraints2D.FreezeRotation;

            else Constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
