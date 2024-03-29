using SystemID;
using UnityEngine;
using SceneManagment;

namespace SaveSystem.SceneDataManagment.Components
{
    public class SaveComponentPosition : IDUser
    {
        [SerializeField] protected Transform _pos;

        private DataVector3 data;
        public void SetData(Vector3 value)
        {
            data.SetData(value);
        }

        protected virtual void Awake()
        {
            SceneDataManager.GetSceneData(gameObject.scene.name, out SceneData scene);

            bool isExist = scene.GetVector3(ID, out data);

            if (isExist)
            {
                _pos.position = data.GetData();
            }
            else
            {
                data.SetData(_pos.position);
                data.SetSave(_pos.position);
            }

            SceneLoader.ListenStart(OnRememberPos, true);
        }

        private void OnRememberPos(SceneConfig conf, LoadingType type)
        {
            data.SetData(_pos.position);

            SceneLoader.ListenStart(OnRememberPos, false);
        }
    }
}
