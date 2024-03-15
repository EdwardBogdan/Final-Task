using UnityEditor;
using UnityEngine;

namespace DeepSystem.General
{
    public abstract class SingletonSystem<T> : MonoBehaviour where T : SingletonSystem<T>
    {
#if !UNITY_EDITOR
        protected static T I;
#else
        protected static T I
        {
            get
            {
                if (_editorInstance == null && !EditorApplication.isPlaying)
                {
                    EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
                    _editorInstance = FindObjectOfType<T>();
                } 

                return _editorInstance;
            }
            set { _editorInstance = value; }
        }

        private static T _editorInstance;

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            }
            _editorInstance = null;
        }
#endif
        protected virtual void Awake()
        {
            if (I == null)
            {
                DontDestroyOnLoad(gameObject);
                I = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
