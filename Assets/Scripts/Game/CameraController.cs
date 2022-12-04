using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        private GameObject _target;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            _target = FindObjectOfType<PlayerController>().gameObject;
            _camera.Follow = _target.transform;
            _camera.LookAt = _target.transform;
        }
    }
}