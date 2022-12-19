using Cinemachine;
using Signals.Game;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        private GameObject _target;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _signalBus.Subscribe<OnCameraInitSignal>(Init);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnCameraInitSignal>(Init);
        }

        private void Init()
        {
            _target = FindObjectOfType<PlayerController>().gameObject;
            _camera.Follow = _target.transform;
            _camera.LookAt = _target.transform;
        }
    }
}