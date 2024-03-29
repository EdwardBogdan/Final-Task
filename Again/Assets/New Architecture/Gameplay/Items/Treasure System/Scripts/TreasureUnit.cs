using UnityEngine;
using UnityEngine.Events;

namespace TreasureSystem
{
    [CreateAssetMenu(menuName = "Treasure/Unit", fileName = "Treasure Unit")]
    public class TreasureUnit : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private TreasureProperty _count;

        public Sprite Icon => _icon;
        public int Count => _count.Value;

        public void ListenCount(UnityAction<int, int> func, bool mode)
        {
            if (mode) _count.OnCountChanged.AddListener(func);
            else _count.OnCountChanged.RemoveListener(func);
        }

        internal bool TryAdd(int value)
        {
            _count.Value = Count + value;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
            return true;
        }
        internal bool TryRemove(int count)
        {
            int oldCount = Count;

            if (count > oldCount) return false;

            _count.Value = oldCount - count;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return true;
        }

        #region Mono
#if UNITY_EDITOR
        private void OnValidate()
        {
            _count.Validate();
        }
#endif
        #endregion
    }
}
