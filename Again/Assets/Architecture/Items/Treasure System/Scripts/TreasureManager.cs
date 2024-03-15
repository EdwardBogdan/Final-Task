using DeepSystem.General;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureSystem
{
    [CreateAssetMenu(fileName = "TreasureManager", menuName = "Treasure/TreasureManager")]
    public class TreasureManager : SystemOrigin<TreasureManager>
    {
        [SerializeField] private TreasureUnit[] _units;

        public static IReadOnlyList<TreasureUnit> TreasureList => I._units;


        #region Init
        private const string AddressableKey = "TreasureManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
