using DialogSystem;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

namespace UI.Dialog
{
    public class DialogTextController : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _textInterval;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UnityEvent _typingEvent;

        private string _phraseText;
        private Coroutine _routine;
        private void OnChange(string value)
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
            }

            _phraseText = value;

            _routine = StartCoroutine(TypeDialogText());
        }

        private void Awake()
        {
            DialogManager.ListenText(OnChange, true);
        }
        private void OnDestroy()
        {
            DialogManager.ListenText(OnChange, false);
        }

        private IEnumerator TypeDialogText()
        {
            _text.text = string.Empty;
        
            foreach (var letter in _phraseText)
            {
                _text.text += letter;
                _typingEvent?.Invoke();
                yield return new WaitForSeconds(_textInterval);
            }

            _routine = null;
        }

        public void OnKeySkip()
        {
            if (_routine == null)
            {
                DialogManager.NextPhrase();
            }
            else
            {
                StopCoroutine(_routine);
                _routine = null;
                _text.text = _phraseText;
                _typingEvent?.Invoke();
            }
        }
    }
}
