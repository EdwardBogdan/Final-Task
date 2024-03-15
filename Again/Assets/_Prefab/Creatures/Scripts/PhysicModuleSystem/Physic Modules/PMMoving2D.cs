using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMMoving2D : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deAcceleration;
        [SerializeField,Min(0)] private float _speedClamp;

        public float Acceleration => _acceleration;

        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            float directionX = direction.x;
            float currentHorizontalSpeed = body.velocity.x;

            currentHorizontalSpeed = directionX != 0
                ? currentHorizontalSpeed + directionX * _acceleration * Time.deltaTime
                : Mathf.MoveTowards(currentHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);


            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -_speedClamp, _speedClamp);
            body.velocity = new(currentHorizontalSpeed, body.velocity.y);
        }

        public void SetAcceleration(float value)
        {
            _acceleration = value;
        }
        public void SetDeacceleration(float value)
        {
            _deAcceleration = value;
        }
        public void SetSpeedMax(float value)
        {
            _speedClamp = value;
            _speedClamp = Mathf.Clamp(_speedClamp, 0, float.PositiveInfinity);
        }

        
    }
}
