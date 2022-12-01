using System;
using System.Linq;
using UnityEngine;

namespace GameUi
{
    [CreateAssetMenu(fileName = "ChapterDialogConfig", menuName = "Configs/ChapterDialogs", order = 0)]
    public class ChapterDialogConfig : ScriptableObject
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private DialogConfig[] dialogs;

        public int LevelNumber => levelNumber;
        public DialogConfig[] Dialogs => dialogs;

        public DialogConfig GetById(int id)
        {
            return dialogs.First(dialogs => dialogs.DialogId == id);
        }
    }
    
    [Serializable]
    public class DialogConfig 
    {
        [SerializeField] private int dialogId;
        [SerializeField] private string characterName;
        [SerializeField] private GameObject character;
        [SerializeField] private string characterText;
        
        public int DialogId => dialogId;
        public string CharacterName => characterName;
        public GameObject Character => character;
        public string CharacterText => characterText;
    }
}