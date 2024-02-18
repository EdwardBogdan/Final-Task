using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMMoving2D : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deAcceleration;
        [SerializeField] private float _speedClamp;

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
    }
}
