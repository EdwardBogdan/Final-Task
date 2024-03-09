using UnityEngine;

namespace GameSystem.SceneManagment
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private SceneConfig _config;

        public void ChangeScene()
        {
            GameSceneManager.LoadScene(_config);
        }
    }
}
