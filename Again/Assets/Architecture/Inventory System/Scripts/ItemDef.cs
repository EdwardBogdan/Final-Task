using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDef", menuName = "Inventory/Items/Def")]
    public class ItemDef : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField, Min(0)] private int _prefSlot;

        public string ID => _id;
        public Sprite Icon => _icon;
        public int Slot => _prefSlot;
    }
} 
