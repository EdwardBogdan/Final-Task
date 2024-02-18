using UnityEngine;
using UnityEngine.Events;

namespace ScriptAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptSimpleAnimator : MonoBehaviour
    {
        [SerializeField, Range(1, 60)] private int _frameRate = 10;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private int currentSprite;
        private float secondsPerFrame;
        private float nextFrameTime;

        private SpriteRenderer _renderer;

        public void SetPlay(bool value)
        {
            enabled = value;
            currentSprite = 0;
            nextFrameTime = Time.time + secondsPerFrame;
        }
        public void SetLoop(bool value)
        {
            _loop = value;
        }

        #region Mono
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            secondsPerFrame = 1f / _frameRate;
            nextFrameTime = Time.time;
        }
        private void Update()
        {
            if (nextFrameTime > Time.time) return;

            if (currentSprite >= _sprites.Length)
            {
                if (!_loop)
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
                currentSprite = 0;
            }

            _renderer.sprite = _sprites[currentSprite];
            nextFrameTime += secondsPerFrame;
            currentSprite++;
        }
        #endregion
    }
}
