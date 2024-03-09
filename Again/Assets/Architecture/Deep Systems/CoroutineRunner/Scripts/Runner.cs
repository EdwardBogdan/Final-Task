using UnityEngine;

namespace Coroutines
{
    public class Runner : MonoBehaviour
    {
        private void Awake()
        {
            CoroutineRunner.Runner = this;
        }
    }
}
