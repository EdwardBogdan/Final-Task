using UnityEditor;
using UnityEngine;

namespace GameSystem.General.Editors
{
    [CustomEditor(typeof(ScriptableObjectLink))]
    public class ScriptableObjectLinkEditor : Editor 
    {
        private ScriptableObjectLink obj;
        private SerializedProperty property;
        private Editor editor; // ������� ������ �� ���������� �������� ��� ���������� �������������

        public override void OnInspectorGUI()
        {
            // ��������� ��������� � ��������������� ���������
            serializedObject.Update();

            // ������ ��������������� �����
            EditorGUILayout.BeginHorizontal();

            // ����������� ���� Target ��� �����
            EditorGUILayout.PropertyField(property, GUIContent.none);

            // ������ ������ �� ���� Target
            if (GUILayout.Button("Remove SO", GUILayout.Width(80))) // ���������� �������� ������ ������
            {
                obj.ChangeTarget(null);
                // ����������� ��������, ���� �� ��� ����������
                if (editor != null) DestroyImmediate(editor);
                editor = null;
            }

            // ���������� ��������������� �����
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            // ��������� ��������� � ��������������� ���������
            serializedObject.ApplyModifiedProperties();

            if (obj.Target != null)
            {
                // ������� �������� ��� Target, ���� �� ��� �� ������ ��� ���� Target ���������
                if (editor == null || editor.target != obj.Target)
                {
                    if (editor != null) DestroyImmediate(editor);
                    editor = CreateEditor(obj.Target);
                }

                // ����������� ����������������� ���������� ��������� ��� Target
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
            // ����������� �������� ��� ������ ��� ������� ��������
            if (editor != null) DestroyImmediate(editor);
        }
    }
}
