using UnityEngine;

namespace SceneManagment
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private LoadingType _type = LoadingType.Room;
        [SerializeField] private SceneConfig _config;

        public void ChangeScene()
        {
            SceneLoader.LoadScene(_config, _type);
        }
    }
}
