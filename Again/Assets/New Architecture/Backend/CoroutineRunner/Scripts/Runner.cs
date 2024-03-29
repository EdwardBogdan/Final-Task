using UnityEngine;

namespace Coroutines.Components
{
    internal class Runner : MonoBehaviour
    {
        private void Awake()
        {
            CoroutineRunner.Runner = this;
        }
    }
}
