using System;
using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ChapterDialogConfig", menuName = "Configs/ChapterDialogs", order = 0)]
    public class ChapterDialogConfig : ScriptableObject
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private DialogConfig[] dialogs;

        public int LevelNumber => levelNumber;
        public DialogConfig[] Dialogs => dialogs;
        
    }
    
    [Serializable]
    public class DialogConfig 
    {
        [SerializeField] private int dialogId;
        [SerializeField] private string characterName;
        [SerializeField] private Sprite characterSpr;
        [SerializeField] private string characterText;
        
        public int DialogId => dialogId;
        public string CharacterName => characterName;
        public string CharacterText => characterText;

        public Sprite CharacterSpr => characterSpr;
    }
}