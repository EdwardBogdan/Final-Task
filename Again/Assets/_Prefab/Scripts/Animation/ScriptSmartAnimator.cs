using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptSmartAnimator : MonoBehaviour
    {
        [SerializeField, Range(1, 60)] private int _frameRate = 10;
        [SerializeField] private Clip[] _clips;

        private float secondsPerFrame;
        private float nextFrameTime;
        private int currentFrame;
        private int clipLenght;

        private SpriteRenderer _renderer;
        private Clip _currentClip;

        public void SetClip(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == name)
                {
                    _currentClip = clip;
                    clipLenght = clip.Sprites.Length;
                    currentFrame = 0;

                    nextFrameTime = Time.time + secondsPerFrame;
                    enabled = true;
                    break;
                }
            }
        }

        #region Mono
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();

            SetClip(_clips[0].Name);
        }
        private void Start()
        {
            secondsPerFrame = 1f / _frameRate;
            nextFrameTime = Time.time + secondsPerFrame;

            if (_clips.Length <= 0)
            {
                enabled = false;
                return;
            }
        }
        private void FixedUpdate()
        {
            if (nextFrameTime > Time.fixedTime) return;

            if (currentFrame >= clipLenght)
            {
                if (!_currentClip.Loop)
                {
                    enabled = false;
                    _currentClip.ONComplete?.Invoke();
                }

                currentFrame = 0;
                return;
            }

            nextFrameTime += secondsPerFrame;
            _renderer.sprite = _currentClip.Sprites[currentFrame];
            currentFrame++;
        }
        #endregion

        [Serializable]
        private class Clip
        {
            [SerializeField] private string _name;
            [SerializeField] private bool _loop;
            [SerializeField] private Sprite[] sprites;
            [SerializeField] private UnityEvent _onComplete;

            internal string Name => _name;
            internal bool Loop => _loop;
            internal Sprite[] Sprites => sprites;
            internal UnityEvent ONComplete => _onComplete;
        }
    }
}