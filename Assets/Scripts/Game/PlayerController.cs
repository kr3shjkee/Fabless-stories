using Common;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        private const float MOVE_TIME = 3f;
        
        private  SaveSystem _saveSystem;
        private  SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus, SaveSystem saveSystem)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(Vector2 pos)
        {
            transform.position = pos;
            gameObject.SetActive(true);
        }

        public void MoveToNextLevel(Vector2 pos)
        {
            transform.DOMove(pos, MOVE_TIME);
        }
    }
}