using UnityEngine;

namespace ScriptAnimation
{
    public class ScriptDirectionMoving : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector2 _direction;

        private float scaleX;
        private float scaleY;

        private void Start()
        {
            scaleX = (transform.localScale.x > 0 ? 1 : -1) * _direction.x;
            scaleY = (transform.localScale.y > 0 ? 1 : -1) * _direction.y;
        }
        private void FixedUpdate()
        {
            Vector2 vector = _transform.position;
            _transform.position = new(vector.x += scaleX, vector.y += scaleY);
        }

        #region Editor
#if UNITY_EDITOR

        [SerializeField] private Color _gizmosColor;
        public Vector2 StartPointEditor { get => GetGlobalPos(transform.position); }
        public Vector2 FinishPointEditor { get => GetGlobalPos(_direction); }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;

            Vector2 direction = FinishPointEditor - StartPointEditor;

            Gizmos.DrawLine(StartPointEditor, FinishPointEditor);

            // Ќарисовать стрелку на конце вектора
            DrawArrowEnd(FinishPointEditor, direction);

            void DrawArrowEnd(Vector2 position, Vector2 direction, float arrowHeadAngle = 20.0f)
            {
                // ¬ычисл€ем длину вектора
                float vectorLength = direction.magnitude;

                // –ассчитываем длину стрелки как 25% от длины вектора, но не меньше 0.5
                float arrowHeadLength = Mathf.Clamp(vectorLength * 0.25f, 0.5f, float.PositiveInfinity);

                // Ќормализуем направление, чтобы получить единичный вектор
                Vector2 directionNormalized = direction.normalized;

                // –ассчитываем точку, где головка стрелки встречаетс€ с основной линией
                Vector2 arrowTip = position;

                // –ассчитываем поворот дл€ правой и левой стороны головки стрелки
                Vector2 rightDirection = Quaternion.Euler(0, 0, -arrowHeadAngle) * directionNormalized;
                Vector2 leftDirection = Quaternion.Euler(0, 0, arrowHeadAngle) * directionNormalized;

                // –исуем линии дл€ головки стрелки
                Gizmos.DrawRay(arrowTip, -rightDirection * arrowHeadLength);
                Gizmos.DrawRay(arrowTip, -leftDirection * arrowHeadLength);
            }
        }

        private Vector2 GetGlobalPos(Vector2 pos)
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;

            return new(currentPos.x + (pos.x * scale.x), currentPos.y + (pos.y * scale.y));
        }
#endif
        #endregion
    }
}
