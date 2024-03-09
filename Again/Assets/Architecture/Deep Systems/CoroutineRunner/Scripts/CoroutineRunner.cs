using GameSystem.General;
using System.Collections;
using UnityEngine;

namespace Coroutines
{
    [CreateAssetMenu(menuName = "Coroutines/Runner", fileName = "CoroutineRunner")]
    public class CoroutineRunner : SystemOrigin<CoroutineRunner>
    {
        #region Runner
        [SerializeField, EditorAttributes.EditorReadOnly] private Runner _runner;
        internal static Runner Runner { set => I._runner = value; }
        #endregion

        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return I._runner.StartCoroutine(coroutine);   
        }
        public static void StopCoroutine(IEnumerator coroutine)
        {
            I._runner.StopCoroutine(coroutine);
        }

        #region Init
        private const string AddressableKey = "CorourineRunner";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
