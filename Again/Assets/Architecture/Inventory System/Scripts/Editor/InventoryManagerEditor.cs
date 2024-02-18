using UnityEditor;

namespace InventorySystem.Editors
{
    [CustomEditor(typeof(InventoryManager))]
    public class InventoryManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (InventoryManager.Items.Count > 0)
            {
                EditorGUILayout.LabelField("Items:", EditorStyles.boldLabel);
                foreach (ItemUnit item in InventoryManager.Items)
                {
                    if (item != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel(item.Def.ID);
                        EditorGUILayout.ObjectField(item, typeof(ItemUnit), false);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Inventory empty:", EditorStyles.boldLabel);
            }
        }
    }
}
