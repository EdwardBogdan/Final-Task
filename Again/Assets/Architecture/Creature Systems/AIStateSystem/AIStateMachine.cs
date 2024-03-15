using UnityEngine;
using AIStateSystem.States;
using PhysicModuleSystem2D;

namespace AIStateSystem
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private GameObject _target;
        [SerializeField, EditorAttributes.EditorReadOnly] private string _state;

        #region Visual Contact

        [SerializeField] private LayerMask _obstructionVisionLayer;
        public bool VisualContact => Target != null && SendVisionRay();
        private bool SendVisionRay()
        {
            float raycastDistance = Vector2.Distance(SelfPos, TargetPos);

            Vector2 vector = DirectionToTarget;

            bool touch = Physics2D.Raycast(SelfPos, vector, raycastDistance, _obstructionVisionLayer);
#if UNITY_EDITOR
            Color color = touch ? Color.red : Color.green;

            Debug.DrawRay(SelfPos, vector, color);
#endif
            return !touch;
        }
        #endregion

        #region Target

        internal GameObject Target { get => _target; private set { _target = value; } }
        public void SetTarget(GameObject obj)
        {
            Target = obj;
        }
        public void DropTarget()
        {
            Target = null;
        }
        #endregion

        #region Physic

        [SerializeField] private PMController2D _physicController;
        internal Rigidbody2D RB => _physicController.RB;

        #region Position
        public Vector3 DirectionToTarget => Target != null ? TargetPos - SelfPos : Vector3.zero;
        internal Vector3 TargetPos => Target != null ? Target.transform.position : Vector3.up * 1000;
        internal Vector3 SelfPos => transform.position;
        #endregion

        internal void SetPhysicDirection(AIState state, Vector2 direction)
        {
            _physicController.SetDirection(direction);
            //Debug.Log($"{state.gameObject.name} : {direction}");
        }
        #endregion

        #region AI Logic
        [SerializeField] private AIState _startState;

        private Coroutine currentStateLogic;

        public void StartState(AIState state)
        {
            StopLogic();

            currentStateLogic = StartCoroutine(state.StateLogic(this));

            _state = state.gameObject.name;

            //Debug.Log($"{state.gameObject.name}");
        }

        public void StopLogic()
        {
            if (currentStateLogic != null)
            {
                StopAllCoroutines();
                //StopCoroutine(currentStateLogic);

                currentStateLogic = null;
                _state = string.Empty;
            }
        }

        private void Start()
        {
            StartState(_startState);
        }
        #endregion

    }
}
