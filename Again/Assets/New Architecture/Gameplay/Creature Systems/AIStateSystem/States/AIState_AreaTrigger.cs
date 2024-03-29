using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ColliderTouchSystem.Touch;

namespace AIStateSystem.States
{
    public class AIState_AreaTrigger : AIState
    {
        [SerializeField, Min(0)] private float _startDelay;
        [SerializeField, Min(0.1f)] private float _eventInterval;
        
        [SerializeField] private ColliderTouchKeeper _area;

        [SerializeField] private UnityEvent _event;

        [Header("Exits")]
        [SerializeField] private AIState _targetOutArea;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            yield return new WaitForSeconds(_startDelay);

            while (_area.IsTouched)
            {
                machine.SetPhysicDirection(this, Vector2.zero);
                machine.RB.velocity = Vector2.zero;

                _event.Invoke();

                yield return new WaitForSeconds(_eventInterval);
            }

            machine.StartState(_targetOutArea);
        }
    }
}
