using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem.Data
{
    [Serializable]
    internal class PhraseSet
    {
        [SerializeField] private PhraseUnit _phrase;
        [SerializeField] private UnityEvent _event;

        internal PhraseUnit Phrase => _phrase;
        internal UnityEvent Event => _event;
    }
}
