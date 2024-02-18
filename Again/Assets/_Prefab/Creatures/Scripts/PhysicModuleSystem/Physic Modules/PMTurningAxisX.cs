using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMTurningAxisX : PhysicModule
    {
        private Vector3 _scale;
        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            float inputX = direction.x;

            if (inputX > 0)
            {
                body.transform.localScale = _scale;
            }
            else if (inputX < 0)
            {
                body.transform.localScale = new(-1 * _scale.x, _scale.y, _scale.z);
            }
        }
        protected override void Awake()
        {
            base.Awake();

            _scale = GetComponent<PMController2D>().RB.transform.localScale;

            if (_scale.x < 0) _scale.x *= -1;
            if (_scale.y < 0) _scale.y *= -1;
            if (_scale.z < 0) _scale.z *= -1;
        }
    }
}
