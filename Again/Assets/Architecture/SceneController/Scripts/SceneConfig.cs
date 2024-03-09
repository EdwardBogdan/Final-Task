using UnityEngine;

namespace GameSystem.SceneManagment
{
    [CreateAssetMenu(menuName = "SceneManagment/Confing", fileName = "SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField, SceneKey] private string _name;
        [SerializeField] private string _textNameKey;
        [SerializeField] private Color _textColor;
        [SerializeField] private Color _panelColor;
        
        public string Name => _name;
        public string TextNameKey => _textNameKey;
        public Color TextColor => _textColor;
        public Color PanelColor => _panelColor;
    }
}
