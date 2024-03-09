using UnityEngine;
using DialogSystem.Side;

namespace DialogSystem.Data
{
    [CreateAssetMenu(fileName = "PhraseUnit", menuName = "Dialog/PhraseUnit")]
    public class PhraseUnit : ScriptableObject
    {
        [SerializeField] private Sprite _face;
        [SerializeField] private SideType _side;
        [SerializeField] private string _phraseKey;

        internal string PhraseKey => _phraseKey;
        internal SideType Side => _side;
        internal Sprite Face => _face;
    }
}
