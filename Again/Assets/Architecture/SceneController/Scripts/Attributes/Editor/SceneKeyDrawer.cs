using UnityEditor;
using UnityEngine;

namespace DeepSystem.SceneManagment.Editors
{
    [CustomPropertyDrawer(typeof(SceneKeyAttribute))]
    public class SceneKeyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                // Получение списка сцен
                var scenes = EditorBuildSettings.scenes;
                var sceneNames = new string[scenes.Length];
                for (int i = 0; i < scenes.Length; i++)
                {
                    sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
                }

                // Текущий индекс выбранной сцены
                int currentIndex = Mathf.Max(0, System.Array.IndexOf(sceneNames, property.stringValue));

                // Отображение выпадающего списка сцен
                currentIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);

                // Обновление значения свойства
                if (currentIndex >= 0 && currentIndex < sceneNames.Length)
                {
                    property.stringValue = sceneNames[currentIndex];
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
