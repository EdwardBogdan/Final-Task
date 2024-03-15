using DeepSystem.SceneManagment;
using HealthSystem;
using SystemID;
using UnityEngine;

namespace SaveSystem.Components
{
    public class SaveComponentHealth : IDUser
    {
        [SerializeField] protected HealthComponent _health;

        private DataHealth data;
        public void SetData(int value)
        {
            data.SetData(value);
        }

        protected virtual void Awake()
        {
            SaveManager.GetSceneData(gameObject.scene.name, out SceneData scene);

            bool isExist = scene.GetHealth(ID, out data);

            if (isExist)
            {
                _health.SetHealth(data.GetData());
            }
            else
            {
                data.SetData(_health.Health.Value);
                data.SetSave(_health.Health.Value);
            }

            GameSceneManager.ListenStart(OnRememberValue, true);
        }

        private void OnRememberValue(SceneConfig conf, LoadingType type)
        {
            data.SetData(_health.Health.Value);

            GameSceneManager.ListenStart(OnRememberValue, false);
        }
    }
}