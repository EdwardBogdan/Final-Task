using HealthSystem;
using UnityEngine;

namespace UI.Hud.Health
{
    public class PlayerHealthBorder : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private float _minBorderSize = 20f;
        [SerializeField] private float _pixelPerOneHealth = 1f;

        private void Awake()
        {
            var health = PlayerHealthManager.PlayerHealth;
            health.ListenMaxChanged(OnSetSize, true);
            OnSetSize(health.ValueMax, 0);
        }
        private void OnDestroy()
        {
            PlayerHealthManager.PlayerHealth.ListenMaxChanged(OnSetSize, false);
        }

        private void OnSetSize(int value, int oldValue)
        {
            float x = _minBorderSize + _pixelPerOneHealth * value;
            _transform.sizeDelta = new(x, _transform.sizeDelta.y);
        }
    }
}
