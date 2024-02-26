using System.Collections;
using TMPro;
using TreasureSystem;
using UnityEngine;

namespace UI.Hud.Treasure
{
    public class TreasureCountWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField, Min(0.01f)] private float _changeInterval;

        [SerializeField] private Color _normal;
        [SerializeField] private Color _add;
        [SerializeField] private Color _remove;

        private int current = 0;

        private Coroutine changeCoroutine;

        private TreasureUnit storedUnit;
        public void OnSetItem(TreasureUnit item)
        {
            Subscribe(false);

            storedUnit = item;

            current = storedUnit.Count;

            SetCount(current);

            Subscribe(true);
        }
        private void Subscribe(bool value)
        {
            if (storedUnit == null) return;

            storedUnit.ListenCount(OnChange, value);
        }

        private void OnDestroy()
        {
            Subscribe(false);
        }

        private void OnChange(int value, int oldValue)
        {
            if (changeCoroutine != null)
            {
                StopCoroutine(changeCoroutine);
            }
            changeCoroutine = StartCoroutine(ChangeTextSmoothly(value));
        }

        private IEnumerator ChangeTextSmoothly(int value)
        {
            bool add = value >= current;

            _text.color = add ? _add : _remove;

            int change = add ? 1 : -1;

            while (current != value)
            {
                yield return new WaitForSeconds(_changeInterval);

                current += change;

                SetCount(current);
            }

            SetCount(current);

            _text.color = _normal;
        }

        private void SetCount(int value)
        {
            _text.text = value.ToString();
        }
    }
}
