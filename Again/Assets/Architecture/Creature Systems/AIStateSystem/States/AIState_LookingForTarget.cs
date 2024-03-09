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

        [SerializeField] private UnityEvent _lostEvent;

        [Header("Exits")]
        [SerializeField] private AIState _targetIsDetectedAgain;
        [SerializeField] private AIState _targetLost;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            if (machine.Target != null)
            {
                machine.SetPhysicDirection(this, Vector2.zero);

                if (_lostTargetDelay.IsReady)
                {
                    _lostEvent?.Invoke();
                } 

                _lostTargetDelay.Reset();

                while (!_lostTargetDelay.IsReady)
                {
                    if (_chaseArea.IsTouched && machine.VisionRay)
                    {
                        machine.StartState(_targetIsDetectedAgain);
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            machine.SetTarget(null);
            machine.StartState(_targetLost);
        }
    }
}
