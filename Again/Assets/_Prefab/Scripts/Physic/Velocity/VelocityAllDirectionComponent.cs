using UnityEngine;

namespace PhysicCustom.Components
{
    public class VelocityAllDirectionComponent : MonoBehaviour
    {
        [SerializeField] private bool _resetVelocity;
        [SerializeField] private bool _checkResistance;
        [SerializeField, Min(0)] private float _velocityValue = 0;

        public void OnAddVelocity(GameObject go)
        {
            if (!go.TryGetComponent(out Rigidbody2D body)) return;

            float velocity = _velocityValue;

            //Учитываем сопротивление инерции
            if (_checkResistance && go.TryGetComponent(out VelocityResistanceComponent resistence))
            {
                velocity = _velocityValue * (1 - resistence.VelocityResistance / 100);
            }

            if (_resetVelocity) body.velocity = new(0, 0);

            Vector2 direction = go.transform.position - transform.position;

            body.AddForce(new(direction.x * velocity, direction.y * velocity), ForceMode2D.Impulse);
        }
    }
}
