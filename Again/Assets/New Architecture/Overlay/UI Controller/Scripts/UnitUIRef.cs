using System;
using UnityEngine;

namespace Overlay.UIManagment.General
{
    [Serializable]
    internal class UnitUIRef
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private string _type;
        [SerializeField, EditorAttributes.EditorReadOnly] private UnitUI _unit;

        public string TypeName => _type;
        public UnitUI Unit => _unit;

        public UnitUIRef(string type, UnitUI unit)
        {
            _type = type;
            _unit = unit;
        }
    }
}
