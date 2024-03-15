using ColliderTouchSystem.Touch;
using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMSlimeMoving : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;

        [SerializeField] private ColliderTouchKeeper _ground;

        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            if (_ground.IsTouched) return;

            float InputX = direction.x;

            if (InputX != 0)
            {
                float _currentHorizontalSpeed = body.velocity.x;

                _currentHorizontalSpeed += InputX * _acceleration * Time.deltaTime;
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);

                body.velocity = new(_currentHorizontalSpeed, body.velocity.y);
            }
        }
    }
}
