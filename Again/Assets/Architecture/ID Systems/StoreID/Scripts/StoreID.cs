using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SystemID
{
    [CreateAssetMenu(menuName = "SystemID/StoreID", fileName = "StoreID")]
    public class StoreID : ScriptableObject
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private List<string> _ids = new();

        private const int length = 5;
        private const int maxAttempts = 10;
        private const string characters = "abcdefghijklmnopqrstuvwxyz1234567890";

        internal static string CreateID()
        {
            StoreID store = I;

            StringBuilder idBuilder = new();

            int charactersLenght = characters.Length;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                for (int x = 0; x < length; x++)
                {
                    int index = Random.Range(0, charactersLenght);
                    idBuilder.Append(characters[index]);
                }

                string id = idBuilder.ToString();

                if (!store._ids.Contains(id))
                {
                    store._ids.Add(id);
                    Debug.Log("ID Added: " + id);

                    EditorUtility.SetDirty(I);
                    return id;
                }
                else
                {
                    idBuilder.Clear();
                }
            }

            Debug.LogError("Failed to generate a unique ID after multiple attempts.");
            return null;
        }
        internal static void RemoveID(string id)
        {
            StoreID store = I;

            if (store._ids.Contains(id))
            {
                store._ids.Remove(id);
                Debug.Log("ID Removed: " + id);
                EditorUtility.SetDirty(I);
            }
        }

        internal static bool CheckID(string id)
        {
            bool foo = I._ids.Contains(id);

            return foo;
        }

        #region Mono
        private void OnValidate()
        {
            if (_ids.Contains(string.Empty))
            {
                _ids.Remove(string.Empty);
            }
        }
        #endregion

        #region Singleton
        private static StoreID _instance;
        private static StoreID I
        {
            get
            {
                if (_instance == null)
                {
                    string[] guids = AssetDatabase.FindAssets($"t:{typeof(StoreID)}", new[] { "Assets" });

                    if (guids.Length == 0)
                    {
                        Debug.LogError($"Object to be a singleton of: {typeof(StoreID)} is doesn`t exist");
                        return null;
                    }

                    if (guids.Length > 1)
                    {
                        Debug.LogError($"More than one object found to be a singleton of: {typeof(StoreID)}");
                        return null;
                    }

                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    _instance = AssetDatabase.LoadAssetAtPath<StoreID>(path);
                }
                return _instance;
            }
        }
        #endregion
    }
}
