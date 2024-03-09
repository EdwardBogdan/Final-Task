using UI.Dialog;
using UnityEngine;

namespace InputControll.KeySubscriber
{
    public class DialogKeySubscriber : MonoBehaviour
    {
        [SerializeField] private DialogTextController _controller;
        public void OnSub()
        {
            DialogKeyReader.SkipCallback += _controller.OnKeySkip;
        }

        public void OnUnsub()
        {
            DialogKeyReader.SkipCallback -= _controller.OnKeySkip;
        }
    }
}
