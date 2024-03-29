using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace SystemID
{
    [ExecuteInEditMode]
    public class IDComponent : MonoBehaviour
    {
        [SerializeField, EditorAttributes.EditorReadOnly] protected string _id;
        public string ID => _id;

        #region Editor
#if UNITY_EDITOR

        private void OnEnable()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if (prefabStage != null) return;

            if (EditorApplication.isPlaying) return;


            if (_id == string.Empty)
            {
                _id = StoreID.CreateID();
                EditorUtility.SetDirty(this);
                return;
            }

            bool foo = StoreID.CheckID(_id);
            if (foo) return;

            StoreID.AddID(_id);
            EditorUtility.SetDirty(this);
        }

        //private void OnDestroy()
        //{
        //    if (_id == string.Empty) return;
        //
        //    var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        //
        //    if (prefabStage != null) return;
        //
        //    if (EditorApplication.isPlaying) return;
        //
        //    StoreID.RemoveID(_id);
        //    _id = string.Empty;
        //
        //    EditorUtility.SetDirty(this);
        //}

#endif
        #endregion
    }
}