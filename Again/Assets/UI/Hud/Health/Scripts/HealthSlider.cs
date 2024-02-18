using HealthSystem;
using HealthSystem.Property;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Health
{
    public class HealthSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private HealthProperty health;
        private void Awake()
        {
            health = PlayerHealthManager.PlayerHealth;
            health.ListenCountChanged(OnChangeHealth, true);
            health.ListenMaxChanged(OnChangedMax, true);
            OnChangeHealth(health.Value, 0);
        }

        private void OnDestroy()
        {
            health.ListenCountChanged(OnChangeHealth, false);
            health.ListenMaxChanged(OnChangedMax, false);
        }

        private void OnChangeHealth(int value, int oldValue)
        { 
            _slider.value = (float)value / health.ValueMax;
        }

        private void OnChangedMax(int value, int oldValue)
        {
            _slider.value = health.Value / (float)value;
        }
    }
}
