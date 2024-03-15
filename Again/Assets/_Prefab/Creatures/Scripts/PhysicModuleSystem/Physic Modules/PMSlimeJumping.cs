using ColliderTouchSystem.Touch;
using CustomUnityUtils.TimeUtils;
using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMSlimeJumping : PhysicModule
    {
        [SerializeField] private float _jumpHeight;

        [SerializeField] private Cooldown _jumpFrequency;

        [SerializeField] private ColliderTouchKeeper _ground;

        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            if (_jumpFrequency.IsReady && _ground.IsTouched)
            {
                _jumpFrequency.Reset();

                body.velocity = new(body.velocity.x, _jumpHeight);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _jumpFrequency.Reset();
        }
    }
}
