using System;
using UnityEngine;

namespace Zenject
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Configs/ProjectSettings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        [SerializeField] private int targetFps = 60;
        [SerializeField] private bool isMultiTouch = false;
        [SerializeField] private bool isScreenRotation = false;

        public int TargetFps => targetFps;

        public bool IsMultiTouch => isMultiTouch;

        public bool IsScreenRotation => isScreenRotation;
        
    }
}