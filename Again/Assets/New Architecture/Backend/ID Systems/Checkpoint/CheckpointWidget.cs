using System;
using SystemID;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem.CheckpointSystem
{
    public class CheckpointWidget : MonoBehaviour
    {
        [SerializeField] private IDComponent _id;

        [SerializeField] private UnityEvent _onActive;
        [SerializeField] private UnityEvent _onDeactive;

        private bool active = false;

        private static event Action OnNewCheckpoint;
        

        private void Awake()
        {
            if (CheckpointManager.CheckpointID == _id.ID)
            {
                Active();
            }
        }

        public void SetCheckpoint()
        {
            CheckpointManager.CheckpointID = _id.ID;

            Active();

            OnNewCheckpoint?.Invoke();
        }

        private void Active()
        {
            _onActive?.Invoke();
            OnNewCheckpoint += Deactive;
            active = true;
        }

        private void Deactive()
        {
            _onDeactive?.Invoke();
            OnNewCheckpoint -= Deactive;
            active = false;
        }

        private void OnDestroy()
        {
            if(active) OnNewCheckpoint -= Deactive;
        }
    }
}
