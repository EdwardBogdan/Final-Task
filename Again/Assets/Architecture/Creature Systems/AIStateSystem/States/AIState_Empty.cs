using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_Empty : AIState
    {
        public override IEnumerator StateLogic(AIStateMachine machine)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
