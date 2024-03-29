using SceneManagment;
using HealthSystem;
using SystemID;
using UnityEngine;

namespace SaveSystem.SceneDataManagment.Components
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
            SceneDataManager.GetSceneData(gameObject.scene.name, out SceneData scene);

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

            SceneLoader.ListenStart(OnRememberValue, true);
        }

        public void OnChanged(int value, int oldValue)
        {
            data.SetData(value);
        }

        private void OnRememberValue(SceneConfig conf, LoadingType type)
        {
            data.SetData(_health.Health.Value);

            SceneLoader.ListenStart(OnRememberValue, false);
        }
    }
}