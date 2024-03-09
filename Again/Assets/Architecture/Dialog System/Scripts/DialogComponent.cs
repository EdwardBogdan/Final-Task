using UnityEngine;
using DialogSystem.Data;

namespace DialogSystem
{
    public class DialogComponent : MonoBehaviour
    {
        [SerializeField] private DialogData[] _dialogs;
        public void StartDialog(string name)
        {
            foreach (var item in _dialogs)
            {
                if (item.DialogName == name)
                {
                    DialogManager.StartDialog(item);
                    return;
                }
            }

            Debug.Log($"Dialog {name} is not exist");
        }
    }
}
