using Configs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game
{
    public class MapLevel : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LevelMapConfig, MapLevelPosition, MapLevel>
        {
        }

        [SerializeField] private TextMeshProUGUI levelNumberText;
        [SerializeField] private SpriteRenderer spriteRenderer; 
        
        private GameObject _parent;

        private bool _isDialog;
        private Vector2 _localPosition;

        private LevelMapConfig _levelMapConfig;

        public bool IsDialog => _isDialog;

        public Vector2 LocalPosition => _localPosition;

        [Inject]
        public void Construct(LevelMapConfig levelMapConfig, MapLevelPosition mapLevelPosition)
        {
            _levelMapConfig = levelMapConfig;
            _localPosition = mapLevelPosition.LocalPosition;
        }

        private void Start()
        {
            _parent = FindObjectOfType<MapLevelsParent>().gameObject;
            gameObject.transform.SetParent(_parent.transform);
        }

        public void Initialize()
        {
            transform.localPosition = _localPosition;
            levelNumberText.text = _levelMapConfig.LevelNumber.ToString();
            spriteRenderer.color = _levelMapConfig.GetLevelColor();
            _isDialog = _levelMapConfig.IsDialog;
        }
    }
}