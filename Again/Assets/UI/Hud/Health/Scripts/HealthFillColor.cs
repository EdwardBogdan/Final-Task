using HealthSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Health
{
    public class HealthFillColor : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private Color _general;
        [SerializeField] private Color _hit;
        [SerializeField] private Color _heal;
        [SerializeField, Min(0)] private float _time = 0.1f;

        private void Awake()
        {
            PlayerHealthManager.PlayerHealth.ListenHit(OnHit, true);
            PlayerHealthManager.PlayerHealth.ListenHeal(OnHeal, true);
        }
        private void OnDestroy()
        {
            PlayerHealthManager.PlayerHealth.ListenHit(OnHit, false);
            PlayerHealthManager.PlayerHealth.ListenHeal(OnHeal, false);
        }

        private void OnHit()
        {
            _sprite.color = _hit;

            StartCoroutine(ColorReturn());
        }
        private void OnHeal()
        {
            _sprite.color = _heal;

            StartCoroutine(ColorReturn());
        }

        private IEnumerator ColorReturn()
        {
            yield return new WaitForSecondsRealtime(_time);

            _sprite.color = _general;
        }
    }
}
