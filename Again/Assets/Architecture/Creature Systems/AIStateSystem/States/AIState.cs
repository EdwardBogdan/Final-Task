using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public abstract class AIState : MonoBehaviour
    {
        public abstract IEnumerator StateLogic(AIStateMachine machine);
    }
}
