using PhysicModuleSystem2D.Modules;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_PatricAttack : AIState
    {
        [SerializeField, Min(0)] private float _baseAcceleration;
        [SerializeField, Min(0)] private float _lowZeroMod;
        [SerializeField, Min(0.0001f)] private float _increaseInterval;
        [SerializeField] private PMMoving2D _pM;
        [SerializeField] private UnityEvent _beforeAttack;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            _beforeAttack.Invoke();

            _pM.SetAcceleration(_baseAcceleration);

            machine.SetPhysicDirection(this, machine.DirectionToTarget);

            while (true)
            {
                float x = _pM.Acceleration;

                if (x > 1)
                {
                    _pM.SetAcceleration(x * x);
                }
                else
                {
                    _pM.SetAcceleration(x * _lowZeroMod);
                }

                yield return new WaitForSeconds(_increaseInterval);
            }
        }
    }
}
