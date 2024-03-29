using ColliderTouchSystem.Touch;
using System;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_SeparatingFork : AIState
    {
        [SerializeField] private bool _moveToTarget;
        [SerializeField, Min(0.1f)] private float _checkInterval;

        [SerializeField] private ColliderTouchKeeper _chaseArea;

        [Header("Exits")]
        [SerializeField] private AIState _targetLost;
        [SerializeField] private SeparetingCheck[] forkExits;

        private delegate void logic(AIStateMachine machine);
        private logic stateLogic;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            stateLogic = Check;

            if (_moveToTarget) stateLogic += Move;

            while (_chaseArea.IsTouched && machine.VisualContact)
            {
                stateLogic.Invoke(machine);

                yield return new WaitForSeconds(_checkInterval);
            }
            machine.StartState(_targetLost);
        }

        private void Check(AIStateMachine machine)
        {
            foreach (SeparetingCheck check in forkExits)
            {
                if (check.area.IsTouched)
                {
                    machine.StartState(check.state);
                    break;
                }
            }
        }
        private void Move(AIStateMachine machine)
        {
            Vector2 _direction = machine.DirectionToTarget;
            machine.SetPhysicDirection(this, _direction);
        }

        [Serializable]
        private class SeparetingCheck
        {
            public ColliderTouchKeeper area;
            public AIState state;
        }
    }
}
