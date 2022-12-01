using System;
using Loading;
using Signals.Loading;
using UnityEngine;
using Zenject;

namespace Common
{
    public class SaveSystem : IInitializable, IDisposable
    {
        private const string DATA_KEY = "GameData";
        private readonly SignalBus _signalBus;

        public SaveSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    
        public GameData Data { get; private set; }
        
        
        public void Initialize()
        {
            _signalBus.Subscribe<OnAgreeButtonClickSignal>(CreateNewData);
        }

        public void Dispose()
        {
            _signalBus.Subscribe<OnAgreeButtonClickSignal>(CreateNewData);
        }

        public bool CheckOnNewProfile()
        {
            if (PlayerPrefs.HasKey(DATA_KEY))
                return false;
            
            return true;
        }

        public void CreateNewData()
        {
            Data = new GameData();
            SaveData();
        }

        public void LoadData()
        {
            string jsonData = PlayerPrefs.GetString(DATA_KEY);
            Data = JsonUtility.FromJson<GameData>(jsonData);
        }

        public void SaveData()
        {
            string jsonData = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString(DATA_KEY, jsonData);
        }

    }
}
