using PhysicModuleSystem2D;
using Property.TimeProperty;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_PatricAttack : AIState
    {
        [SerializeField, Min(0.1f)] private float _attakcDuration;

        [SerializeField, Min(0.1f)] private float _restDuration;

        [SerializeField] private Cooldown _attackCD;

        [SerializeField] private PMController2D _physic;

        [SerializeField] private UnityEvent _beforeAttack;
        [SerializeField] private UnityEvent _attack;
        [SerializeField] private UnityEvent _afterAttack;

        [Header("Exits")]
        [SerializeField] private AIState _attackFinished;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            float time = Time.time + _attakcDuration;

            _attackCD.Reset();

            _beforeAttack.Invoke();

            _physic.SetDirection(machine.DirectionToTarget);

            while (Time.time <= time)
            {
                if (_attackCD.IsReady)
                {
                    _attackCD.Reset();
                    _attack.Invoke();
                }

                yield return new WaitForFixedUpdate();
            }
            _afterAttack.Invoke();

            _physic.SetDirection(Vector2.zero);

            yield return new WaitForSeconds(_restDuration);

            machine.StartState(_attackFinished);
        }
    }
}
