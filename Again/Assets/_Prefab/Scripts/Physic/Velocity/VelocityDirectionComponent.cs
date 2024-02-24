using UnityEngine;

namespace PhysicCustom.Components
{
    public class VelocityDirectionComponent : MonoBehaviour
    {
        [SerializeField] private bool _resetVelocity;
        [SerializeField] private bool _checkResistance;
        [SerializeField, Min(0)] private float _velocityValue = 0;

        [SerializeField] private Vector2 _direction;        
        public void OnAddVelocity(GameObject go)
        {
            if(!go.TryGetComponent(out Rigidbody2D body)) return;

            float velocity = _velocityValue;

            //Учитываем сопротивление инерции
            if (_checkResistance && go.TryGetComponent(out VelocityResistanceComponent resistence))
            {
                velocity = _velocityValue * (1 - resistence.VelocityResistance / 100);
            }

            if(_resetVelocity) body.velocity = new(0, 0);

            body.AddForce(new(_direction.x * velocity, _direction.y * velocity), ForceMode2D.Impulse);
        }

        #region Editor
#if UNITY_EDITOR
        [SerializeField] private bool _showGizmos = true;
        [SerializeField] private Color _gizmosColor = Color.yellow;
        public Vector2 StartPointEditor { get => transform.position;}
        public Vector2 FinishPointEditor { get => GetGlobalPos(_direction); set => _direction = GetLocalPos(value); }
        private Vector2 GetGlobalPos(Vector2 pos)
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;

            return new(currentPos.x + (pos.x * scale.x), currentPos.y + (pos.y * scale.y));
        }
        private Vector2 GetLocalPos(Vector2 globalPos)
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;

            if (scale.x == 0 || scale.y == 0)
            {
                return Vector2.zero;
            }

            return new Vector2(
                (globalPos.x - currentPos.x) / scale.x,
                (globalPos.y - currentPos.y) / scale.y
            );
        }

        private void OnDrawGizmosSelected()
        {
            if (!_showGizmos) return;

            Gizmos.color = _gizmosColor;

            Vector2 direction = FinishPointEditor - StartPointEditor;

            Gizmos.DrawLine(StartPointEditor, FinishPointEditor);

            // Нарисовать стрелку на конце вектора
            DrawArrowEnd(FinishPointEditor, direction);

            static void DrawArrowEnd(Vector2 position, Vector2 direction, float arrowHeadAngle = 20.0f)
            {
                // Вычисляем длину вектора
                float vectorLength = direction.magnitude;

                // Рассчитываем длину стрелки как 25% от длины вектора, но не меньше 0.5
                float arrowHeadLength = Mathf.Clamp(vectorLength * 0.25f, 0.5f, float.PositiveInfinity);

                // Нормализуем направление, чтобы получить единичный вектор
                Vector2 directionNormalized = direction.normalized;

                // Рассчитываем точку, где головка стрелки встречается с основной линией
                Vector2 arrowTip = position;

                // Рассчитываем поворот для правой и левой стороны головки стрелки
                Vector2 rightDirection = Quaternion.Euler(0, 0, -arrowHeadAngle) * directionNormalized;
                Vector2 leftDirection = Quaternion.Euler(0, 0, arrowHeadAngle) * directionNormalized;

                // Рисуем линии для головки стрелки
                Gizmos.DrawRay(arrowTip, -rightDirection * arrowHeadLength);
                Gizmos.DrawRay(arrowTip, -leftDirection * arrowHeadLength);
            }
        }
#endif
        #endregion
    }
}
