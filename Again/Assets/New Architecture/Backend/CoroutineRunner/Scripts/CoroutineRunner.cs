using Coroutines.Components;
using GameSystem.Initialization;
using System.Collections;
using UnityEngine;

namespace Coroutines
{
    [CreateAssetMenu(menuName = "Backend/CoroutineRunner", fileName = "CoroutineRunner")]
    public class CoroutineRunner : SystemOrigin<CoroutineRunner>
    {
        #region Runner
        [SerializeField, EditorAttributes.EditorReadOnly] private Runner _runner;
        internal static Runner Runner { get => I._runner;  set => I._runner = value; }
        #endregion

        public void PlayCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return Runner.StartCoroutine(coroutine);   
        }
        public static void StopCoroutine(Coroutine coroutine)
        {
            Runner.StopCoroutine(coroutine);
        }

        public static void StopAllCoroutines()
        {
            Runner.StopAllCoroutines();
        }

        #region Init
        private const string Group = "Backend";
        private const string AddressableKey = "CorourineRunner";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
