using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ChapterConfig", menuName = "Configs/ChapterConfig", order = 0)]
    public class ChapterConfig : ScriptableObject
    {
        [SerializeField] private ChapterDialogConfig[] dialogConfigs;

        public ChapterDialogConfig[] DialogConfigs => dialogConfigs;
    }
    
    [Serializable]
    public class ChapterDialogConfig 
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private DialogConfig[] dialogs;

        public int LevelNumber => levelNumber;
        public DialogConfig[] Dialogs => dialogs;
        
    }
    
    [Serializable]
    public class DialogConfig 
    {
        [SerializeField] private CharacterPosition characterPosition;
        [SerializeField] private string characterName;
        [SerializeField] private Sprite characterSpr;
        [SerializeField] private string characterText;

        private enum CharacterPosition
        {
            LEFT,
            RIGHT
        }

        public bool IsCharacterPositionLeft()
        {
            if (characterPosition == CharacterPosition.LEFT)
                return true;
            return false;
        }
        
        public string CharacterName => characterName;
        public string CharacterText => characterText;

        public Sprite CharacterSpr => characterSpr;
    }
}