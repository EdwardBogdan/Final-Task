using SystemID;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem.SceneDataManagment.Components
{
    public class SaveComponentBool : IDUser
    {
        [SerializeField] protected UnityEvent _onTrue;
        [SerializeField] protected UnityEvent _onFalse;

        private DataBool data;
        public void SetData(bool value)
        {
            data.SetData(value);
        }
        
        protected virtual void Awake()
        {
            SceneDataManager.GetSceneData(gameObject.scene.name, out SceneData scene);

            scene.GetBool(ID, out data);

            if (data.GetData())
            {
                _onTrue?.Invoke();
            }
            else
            {
                _onFalse?.Invoke();
            }
        }
    }
}
