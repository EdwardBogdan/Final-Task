using System.IO;
using UnityEngine;

namespace GameSystem.SaveSystem
{
    public static class SaveManager
    {
        private static string _savePath;
        private const string SaveFolderName = "/SaveData";

        public static T GetSave<T>(string folderName, string fileName) where T : new()
        {
            string path = _savePath + "/" + folderName;

            InitFolder(path);

            path = Path.Combine(path, fileName);

            if (!File.Exists(path))
            {
                T newSet = new();
                SetSave(newSet, folderName, fileName);
                return newSet;
            }

            string json = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(json);
        }
        public static void SetSave(object data, string folderName, string fileName)
        {
            string path = _savePath + "/" + folderName;

            path = Path.Combine(path, fileName);

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitSaveFolder()
        {
            _savePath = Application.dataPath + SaveFolderName;

            InitFolder(_savePath);
        }

        private static void InitFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
