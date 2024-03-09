using Creature.Player.PlayerInit;
using GameSystem.Overlay;
using UnityEngine;

namespace UI.Hud.Health
{
    public class HudHealth : UnitUI
    {
        #region Init
        private static HudHealth instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            const string AddressableKey = "Hud Health";

            PlayerInitManager.OnPlayerCreated += OnCreateHud;
            PlayerInitManager.OnPlayerDestroyed += OnDestroyHud;

            static void OnCreateHud()
            {
                UILoader.LoadUI<HudHealth>(AddressableKey, OnLoaded);

                static void OnLoaded(HudHealth value)
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
