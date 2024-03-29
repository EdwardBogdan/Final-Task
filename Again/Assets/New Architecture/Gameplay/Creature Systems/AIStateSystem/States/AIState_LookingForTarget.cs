using System.Collections;
using UnityEngine;
using Property.TimeProperty;
using ColliderTouchSystem.Touch;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_LookingForTarget : AIState
    {
        [SerializeField] private Cooldown _lostTargetDelay;

        [SerializeField] private ColliderTouchKeeper _chaseArea;

        [SerializeField] private UnityEvent _firstLostEvent;
        [SerializeField] private UnityEvent _secondLostEvent;

        [Header("Exits")]
        [SerializeField] private AIState _targetIsDetectedAgain;
        [SerializeField] private AIState _targetLost;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            machine.SetPhysicDirection(this, Vector2.zero);

            if (_lostTargetDelay.IsReady)
            {
                _firstLostEvent?.Invoke();
            }

            _lostTargetDelay.Reset();

            while (!_lostTargetDelay.IsReady)
            {
                if (_chaseArea.IsTouched && machine.VisualContact)
                {
                    machine.StartState(_targetIsDetectedAgain);
                }
                yield return new WaitForEndOfFrame();
            }

            _secondLostEvent?.Invoke();

            machine.DropTarget();
            machine.StartState(_targetLost);
        }
    }
}
