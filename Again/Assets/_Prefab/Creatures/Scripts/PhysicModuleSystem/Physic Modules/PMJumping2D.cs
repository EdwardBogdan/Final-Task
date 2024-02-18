using ColliderTouchSystem.Touch;
using Property.TimeProperty;
using UnityEngine;
using UnityEngine.Events;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMJumping2D : PhysicModule
    {
        [SerializeField, Min(0)] private float _jumpHeight;
        [SerializeField, Min(0)] private float _extraJumpHeight;
        [SerializeField, Min(0)] private int _countExtraJumpMax;
        [SerializeField, Min(0)] private float _coyoteTime;
        [SerializeField, Min(0)] private float _fallMultiplier;
        [SerializeField, Min(0)] private float _lowJumpMultiplier;
        [SerializeField] private Cooldown _jumpCooldown;
        [SerializeField] private ColliderTouchKeeper _ground;
        [SerializeField] private UnityEvent _jumpEvent;
        [SerializeField] private UnityEvent _extraJumpEvent;

        private int _countExtraJump;
        private bool _coyoteUsable;

        private bool Grounded => _ground.IsTouched;
        private bool CanUseCoyote => _coyoteUsable && !Grounded && LastGroundedTime + _coyoteTime > Time.time;
        private float LastGroundedTime => _ground.LastTouchTime;

        protected override void Awake()
        {
            base.Awake();
            _jumpCooldown.Reset();
        }

        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            float velocityY = body.velocity.y;
            bool isJumpPressed = direction.y > 0;

            if (isJumpPressed && _jumpCooldown.IsReady && velocityY <= 0)
            {
                CalculateJump(ref velocityY);
            }
            else if (velocityY > 0 && !isJumpPressed)
            {
                velocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (velocityY < 0)
            {
                velocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            }
            else return;

            body.velocity = new Vector2(body.velocity.x, velocityY);

            void CalculateJump(ref float velocityY)
            {
                if (Grounded || CanUseCoyote)
                {
                    velocityY = _jumpHeight;
                    _coyoteUsable = false;
                    _jumpEvent?.Invoke();
                }
                else if (_countExtraJump > 0)
                {
                    velocityY = _extraJumpHeight;
                    _countExtraJump--;
                    _extraJumpEvent?.Invoke();
                }

                _jumpCooldown.Reset();
            }
        }

        public void OnGrounded()
        {
            _countExtraJump = _countExtraJumpMax;
            _coyoteUsable = true;
        }
    }
}
