using UnityEngine;
using UnityEngine.UI;
using Property.ObservableProperty;

namespace DeepSystem.Overlay
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Canvas _overlayCanvas;
        [SerializeField] private CanvasScaler _scaler;

        [SerializeField] private Vector2ObsProperty _resolution;

        private static ScreenManager I;
        public static Canvas OverlayCanvas => I._overlayCanvas;

        private void Awake()
        {
            if (I != null)
            {
                Destroy(gameObject);
                return;
            }

            I = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
