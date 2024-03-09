using PhysicModuleSystem2D;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_WaitTheTarget : AIState
    {
        [SerializeField, Min(0.1f)] private float _checkInterval;
        [SerializeField] private PMController2D _physicController;
        [SerializeField] private AIState _targetIsDetected;
       
        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            machine.SetTarget(null);
            _physicController.SetDirection(Vector2.zero);

            while (true)
            {
                yield return new WaitForSeconds(_checkInterval);

                if (machine.VisionRay)
                {
                    machine.StartState(_targetIsDetected);
                    break;
                }
            }
        }
    }
}
