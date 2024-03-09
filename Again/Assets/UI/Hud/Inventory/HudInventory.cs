using UnityEngine;
using Creature.Player.PlayerInit;
using GameSystem.Overlay;

namespace UI.Hud.Inventory
{
    public class HudInventory : UnitUI
    {
        #region Init
        private static HudInventory instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            const string AddressableKey = "Hud Inventory";

            PlayerInitManager.OnPlayerCreated += OnCreateHud;
            PlayerInitManager.OnPlayerDestroyed += OnDestroyHud;

            static void OnCreateHud()
            {
                UILoader.LoadUI<HudInventory>(AddressableKey, OnLoaded);

                static void OnLoaded(HudInventory value)
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
