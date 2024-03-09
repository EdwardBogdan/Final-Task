using UnityEngine;
using AIStateSystem.States;
using PhysicModuleSystem2D;

namespace AIStateSystem
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private string _state;

        [SerializeField] private LayerMask _obstructionVisionLayer;

        [SerializeField] private PMController2D _physicController;

        [SerializeField] private AIState _startState;

        private Coroutine currentStateLogic;
        public GameObject Target { get; private set; }

        public bool VisionRay => Target != null && SendVisionRay();
        public Vector3 DirectionToTarget => Target != null ? TargetPos - SelfPos : Vector3.zero;
        public Vector3 TargetPos => Target.transform.position;
        public Vector3 SelfPos => transform.position;

        internal Rigidbody2D RB => _physicController.RB;

        public void SetTarget(GameObject obj)
        {
            Target = obj;
        }
        public void DropTarget()
        {
            Target = null;
        }

        internal void SetPhysicDirection(AIState state, Vector2 direction)
        {
            //Debug.Log($"{state.gameObject.name} : {direction}");
            _physicController.SetDirection(direction);
        }
        internal void StartState(AIState state)
        {
            if (currentStateLogic != null)
            {
                StopAllCoroutines();
                //StopCoroutine(currentStateLogic);
            }
            currentStateLogic = StartCoroutine(state.StateLogic(this));

            _state = state.gameObject.name;

            Debug.Log($"{state.gameObject.name}");
        }
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

        private void Start()
        {
            StartState(_startState);
        }
    }
}
