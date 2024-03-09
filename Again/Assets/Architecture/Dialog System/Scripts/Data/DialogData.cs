using System;
using UnityEngine;

namespace DialogSystem.Data
{
    [Serializable]
    internal class DialogData
    {
        [SerializeField] private string _dialogName;
        [SerializeField] private PhraseSet[] _phrases;

        internal string DialogName => _dialogName;
        internal PhraseSet[] Phrases => _phrases;
    }
}
