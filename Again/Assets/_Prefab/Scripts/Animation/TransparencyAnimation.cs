using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace ScriptAnimation
{
    public class TransparencyAnimation : MonoBehaviour
    {
        [SerializeField, Range(MinAlpha, MaxAlpha)] private float _currentAlpha;

        [SerializeField, Range(MinFrameRate, MaxFrameRate)] private float _frameRate = 30;
        [SerializeField, Min(MinDuration)] private float _duration = 5f;
        [SerializeField] private SpriteRenderer[] _renderers;

        [SerializeField] private UnityEvent _onShowFinish;
        [SerializeField] private UnityEvent _onHideFinish;

        private Coroutine _currentProcess;
        
        private const int MinFrameRate = 1;
        private const int MaxFrameRate = 60;

        private const float MinDuration = 0.05f;

        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        #region Controll
        public void StartTransparency(float targetAlpha)
        {
            if (_currentProcess != null)
            {
                StopCoroutine(_currentProcess);
            }

            targetAlpha = Mathf.Clamp(targetAlpha, MinAlpha, MaxAlpha);

            // Вычисляем адаптивную длительность на основе текущего и целевого значения альфа
            float adaptiveDuration = Mathf.Abs(targetAlpha - _currentAlpha) * _duration;

            _currentProcess = StartCoroutine(Process(targetAlpha, adaptiveDuration));
        }
        public void SetDuration(float value) => _duration = Mathf.Clamp(value, MinDuration, float.PositiveInfinity);
        public void SetFrameRate(float value) => _frameRate = Mathf.Clamp(value, MinFrameRate, MaxFrameRate);
        #endregion

        private IEnumerator Process(float targetAlpha, float adaptiveDuration)
        {
            float startTime = Time.time;
            float startAlpha = _currentAlpha;

            float secondsPerFrame = 1 / _frameRate;

            while (Time.time < startTime + adaptiveDuration)
            {
                _currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, (Time.time - startTime) / adaptiveDuration);

                foreach (var renderer in _renderers)
                {
                    Color color = renderer.color;
                    color.a = _currentAlpha;
                    renderer.color = color;
                }

                yield return new WaitForSeconds(secondsPerFrame);
            }

            // Установить точное значение альфа после анимации
            _currentAlpha = targetAlpha;
            foreach (var renderer in _renderers)
            {
                Color color = renderer.color;
                color.a = _currentAlpha;
                renderer.color = color;
            }

            if (_currentAlpha == MaxAlpha) _onShowFinish?.Invoke();
            else if (_currentAlpha == MinAlpha) _onHideFinish?.Invoke();
        }
    }
}
