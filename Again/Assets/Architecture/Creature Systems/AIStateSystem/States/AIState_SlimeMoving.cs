using System.Collections;
using UnityEngine;
using Property.TimeProperty;
using ColliderTouchSystem.Touch;
using PhysicModuleSystem2D;

namespace AIStateSystem.States
{
    public class AIState_SlimeMoving : AIState
    {
        [SerializeField] private Cooldown _waitTimeToChange;

        [SerializeField] private ColliderTouchKeeper _areaWall;
        [SerializeField] private PMController2D _physicController;
        
        private Vector3 direction;

        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            int x = Random.Range(0, 2) * 2 - 1; // result 1 or -1

            direction = new(x, 0, 0);

            _waitTimeToChange.Reset();

            while (true)
            {
                if (_areaWall.IsTouched && _waitTimeToChange.IsReady)
                {
                    direction *= -1;
                    _waitTimeToChange.Reset();
                }

                _physicController.SetDirection(direction);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}