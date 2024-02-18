using GameSystem.Initialization;
using UnityEditor;
using UnityEngine;

namespace GameSystem.General
{
    public abstract class SystemOrigin<T> : ScriptableObject where T : SystemOrigin<T>
    {
        [SerializeField] private ScriptableObjectLink linkPrefab;
#if !UNITY_EDITOR
        protected static T I;
#else
        protected static T I
        {
            get
            {
                if (_editorInstance == null && !EditorApplication.isPlaying)
                {
                    string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
                    T[] assets = new T[guids.Length];

                    for (int i = 0; i < guids.Length; i++)
                    {
                        string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                        assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    }

                    if (assets.Length == 0)
                    {
                        Debug.LogError($"{typeof(T).Name} not founded");
                        return null;
                    }
                    else if(assets.Length > 1)
                    {
                        Debug.LogError($"{typeof(T).Name} founded more than one");
                    }

                    I = assets[0];
                }

                return _editorInstance;
            }
            set { _editorInstance = value; }
        }

        private static T _editorInstance;

#endif
        protected static void InitializeSystem(string addressableKey)
        {
            SystemInitializer.InitNewSystem(addressableKey, OnInitCompleted);
        }

        private static void OnInitCompleted(ScriptableObject SO)
        {
            I = (T)SO;

            string name = I.linkPrefab.gameObject.name;

            GameObject obj = Instantiate(I.linkPrefab.gameObject);

            obj.name = name;

            DontDestroyOnLoad(obj);
        }
    }
}
