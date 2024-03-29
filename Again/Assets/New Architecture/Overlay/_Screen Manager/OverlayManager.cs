using UnityEngine;

namespace Overlay
{
    public class OverlayManager : MonoBehaviour
    {
        [SerializeField] private Canvas _overlayCanvas;

        private static OverlayManager I;
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
