using Creature.Player.PlayerInit;
using DeepSystem.Overlay;
using UnityEngine;

namespace UI.Hud.Treasure
{
    public class HudTreasure : UnitUI
    {
        #region Init
        private static HudTreasure instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            const string AddressableKey = "Hud Treasure";

            PlayerInitManager.OnPlayerCreated += OnCreateHud;
            PlayerInitManager.OnPlayerDestroyed += OnDestroyHud;

            static void OnCreateHud()
            {
                UILoader.LoadUI<HudTreasure>(AddressableKey, OnLoaded);

                static void OnLoaded(HudTreasure value)
                {
                    instance = value;
                }
            }

            static void OnDestroyHud()
            {
                if (instance == null) return;

                Destroy(instance.gameObject);
            }
        }
        #endregion
    }
}
