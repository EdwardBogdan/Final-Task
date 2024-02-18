using PhysicCustom.Components;
using UnityEditor;
using UnityEngine;

namespace PhysicCustom.Editors
{
    [CustomEditor(typeof(VelocityDirectionComponent))]
    public class VelocityDirectionComponentEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            VelocityDirectionComponent myTarget = (VelocityDirectionComponent)target;
            
            // Рисуем Handle для конечной точки
            EditorGUI.BeginChangeCheck();
            Vector3 newEndPos = Handles.PositionHandle(myTarget.FinishPointEditor, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(myTarget, "Move End Point");
                myTarget.FinishPointEditor = newEndPos;
            }
        }
    }
}
