using UnityEngine;

namespace DeepSystem.SceneManagment
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private LoadingType _type = LoadingType.Room;
        [SerializeField] private SceneConfig _config;

        public void ChangeScene()
        {
            GameSceneManager.LoadScene(_config, _type);
        }
    }
}
