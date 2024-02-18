using UnityEngine;

namespace GameSystem.General
{
    public class ScriptableObjectLink : MonoBehaviour
    {
        [SerializeField] private ScriptableObject _target;

#if UNITY_EDITOR
        public ScriptableObject Target => _target;

        public void ChangeTarget(ScriptableObject obj)
        {
            _target = obj;
        }
#endif
    }
}
