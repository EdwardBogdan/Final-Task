using AIStateSystem.Patroling;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_PointPatrol : AIState
    {
        [SerializeField, Min(0.1f)] private float _checkInterval = 0.1f;

        [SerializeField] private float _treshold = 0.5f;

        [Header("Exits")]
        [SerializeField] private AIState _targetIsDetected;

        private int _pointIndex;
        private Transform[] _points;

        private delegate IEnumerator logic(AIStateMachine machine);
        private logic stateLogic;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            if (TryGetComponent(out PatrolPointsForAIState patrol))
            {
                _points = patrol._points;
            }

            stateLogic = (_points == null || _points.Length <= 0) ? JustWait : Patrol;

            while (true)
            {
                if (machine.VisualContact)
                {
                    machine.StartState(_targetIsDetected);
                    break;
                }
                yield return stateLogic.Invoke(machine);
            }
        }
        private IEnumerator JustWait(AIStateMachine machine)
        {
            machine.SetPhysicDirection(this, Vector2.zero);
            yield return new WaitForSeconds(_checkInterval);
        }
        private IEnumerator Patrol(AIStateMachine machine)
        {
            bool IsOnPoint = (_points[_pointIndex].position - machine.SelfPos).magnitude < _treshold;

            if (IsOnPoint)
            {
                _pointIndex = (int)Mathf.Repeat(_pointIndex + 1, _points.Length);
            }

            var direction = _points[_pointIndex].position - machine.SelfPos;

            direction.y = 0;
            direction.Normalize();
            machine.SetPhysicDirection(this, direction);

            yield return new WaitForSeconds(_checkInterval);
        }
    }
}
