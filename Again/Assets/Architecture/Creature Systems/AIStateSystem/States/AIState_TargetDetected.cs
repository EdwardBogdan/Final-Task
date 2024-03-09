using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_TargetDetected : AIState
    {
        [SerializeField, Min(0)] private float _roundTime;
        [SerializeField, Min(0)] private float _changeStateDelay;

        [SerializeField] private UnityEvent _detectedEvent;

        [Header("Exits")] 
        [SerializeField] private AIState _targetStillDetected;
        [SerializeField] private AIState _targetLost;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            Vector2 _direction = machine.DirectionToTarget;
            machine.SetPhysicDirection(this, _direction);

            yield return new WaitForSeconds(_roundTime);

            machine.SetPhysicDirection(this, Vector2.zero);

            _detectedEvent?.Invoke();

            yield return new WaitForSeconds(_changeStateDelay);

            if (machine.VisionRay)
            {
                machine.StartState(_targetStillDetected);
            }
            else
            {
                machine.StartState(_targetLost);
            }
        }
    }
}
