using System;
using Loading;
using Newtonsoft.Json;
using Signals.Loading;
using UnityEngine;
using Zenject;

namespace Common
{
    public class SaveSystem : IInitializable, IDisposable
    {
        private const string DATA_KEY = "GameData";
        private const string TIMERS_DATA_KEY = "TimersData";
        private readonly SignalBus _signalBus;

        public SaveSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    
        public GameData Data { get; private set; }
        public TimersData TimersData { get; private set; }
        
        
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
            TimersData = new TimersData();
            SaveData();
        }

        public void LoadData()
        {
             var jsonData = PlayerPrefs.GetString(DATA_KEY);
             Data = JsonUtility.FromJson<GameData>(jsonData);

             var jsonTimersData = PlayerPrefs.GetString(TIMERS_DATA_KEY);
             TimersData = JsonConvert.DeserializeObject<TimersData>(jsonTimersData);
        }

        public void SaveData()
        {
             var jsonData = JsonUtility.ToJson(Data);
             PlayerPrefs.SetString(DATA_KEY, jsonData);

             var jsonTimerData = JsonConvert.SerializeObject(TimersData);
             PlayerPrefs.SetString(TIMERS_DATA_KEY, jsonTimerData);
        }

    }
}
