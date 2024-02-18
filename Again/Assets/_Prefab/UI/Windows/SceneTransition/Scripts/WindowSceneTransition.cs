using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.SceneTransition
{
    public class WindowSceneTransition : WindowAnimated<WindowSceneTransition>
    {
        [SerializeField] private Image _panel1;
        [SerializeField] private Image _panel2;
        [SerializeField] private Text _text;

        const float WaitSec = 0.5f;

        public void ChangeColors(Color panel1, Color panel2, Color text)
        {
            _panel1.color = panel1;
            _panel2.color = panel2;
            _text.color = text;
        }
        public void WaitOperation(AsyncOperation operation)
        {        
            StartCoroutine(LoadWaiter());

            IEnumerator LoadWaiter()
            {
                while (!operation.isDone)
                {
                    yield return new WaitForSeconds(WaitSec);
                }
        
                TriggerHide();
                yield return null;
            }
        }
    }
}
