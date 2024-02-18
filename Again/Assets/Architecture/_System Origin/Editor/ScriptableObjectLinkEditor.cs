using UnityEditor;
using UnityEngine;

namespace GameSystem.General.Editors
{
    [CustomEditor(typeof(ScriptableObjectLink))]
    public class ScriptableObjectLinkEditor : Editor 
    {
        private ScriptableObjectLink obj;
        private SerializedProperty property;
        private Editor editor; // Держите ссылку на внутренний редактор для повторного использования

        public override void OnInspectorGUI()
        {
            // Сохраняем изменения в сериализованных свойствах
            serializedObject.Update();

            // Начало горизонтального блока
            EditorGUILayout.BeginHorizontal();

            // Отображение поля Target без метки
            EditorGUILayout.PropertyField(property, GUIContent.none);

            // Кнопка справа от поля Target
            if (GUILayout.Button("Remove SO", GUILayout.Width(80))) // Установите желаемую ширину кнопки
            {
                obj.ChangeTarget(null);
                // Освобождаем редактор, если он уже существует
                if (editor != null) DestroyImmediate(editor);
                editor = null;
            }

            // Завершение горизонтального блока
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            // Применяем изменения к сериализованным свойствам
            serializedObject.ApplyModifiedProperties();

            if (obj.Target != null)
            {
                // Создаем редактор для Target, если он еще не создан или если Target изменился
                if (editor == null || editor.target != obj.Target)
                {
                    if (editor != null) DestroyImmediate(editor);
                    editor = CreateEditor(obj.Target);
                }

                // Отображение пользовательского интерфейса редактора для Target
                editor.OnInspectorGUI();
            }
        }

        private void OnEnable()
        {
            obj = (ScriptableObjectLink)target;
            property = serializedObject.FindProperty("_target");
        }

        private void OnDisable()
        {
            // Освобождаем редактор при выходе для очистки ресурсов
            if (editor != null) DestroyImmediate(editor);
        }
    }
}
