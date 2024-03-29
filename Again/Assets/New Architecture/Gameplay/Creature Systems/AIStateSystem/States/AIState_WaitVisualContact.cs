using PhysicModuleSystem2D;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_WaitVisualContact : AIState
    {
        [SerializeField] private bool _stopMoving = true;
        [SerializeField, Min(0)] private float _restTime = 0;
        [SerializeField, Min(0.1f)] private float _checkInterval;
        [SerializeField] private PMController2D _physicController;
        [SerializeField] private UnityEvent _onBeforeWaiting;

        [Header("Exits")]
        [SerializeField] private AIState _targetIsDetected;
       
        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            if(_stopMoving) _physicController.SetDirection(Vector2.zero);

            _onBeforeWaiting?.Invoke();

            yield return new WaitForSeconds(_restTime);

            while (true)
            {
                if (machine.VisualContact)
                {
                    machine.StartState(_targetIsDetected);
                    break;
                }

                yield return new WaitForSeconds(_checkInterval);
            }
        }
    }
}
