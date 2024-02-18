using UnityEngine;

namespace PhysicCustom.Components
{
    public class VelocityResistanceComponent : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private float _velocityResistance;

        public float VelocityResistance => _velocityResistance;
    }
}
