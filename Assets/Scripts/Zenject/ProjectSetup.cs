using System;
using Loading;
using UnityEngine;

namespace Zenject
{
    public class ProjectSetup : IInitializable, IDisposable
    {
        private readonly ProjectSettings _projectSettings;

        private readonly SaveSystem _saveSystem;

        public ProjectSetup(ProjectSettings projectSettings, SaveSystem saveSystem)
        {
            _projectSettings = projectSettings;
            _saveSystem = saveSystem;
        }

        public void Initialize()
        {
            Application.targetFrameRate = _projectSettings.TargetFps;
            Input.multiTouchEnabled = _projectSettings.IsMultiTouch;
            Screen.autorotateToLandscapeLeft = _projectSettings.IsScreenRotation;
            Screen.autorotateToLandscapeRight = _projectSettings.IsScreenRotation;
        }

        public void Dispose()
        {
            
        }
    }
}