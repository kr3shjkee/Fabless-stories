using UnityEngine;

namespace Game
{
    public class MapLevelPosition
    {
        public readonly Vector2 LocalPosition;

        public MapLevelPosition(Vector2 localPosition)
        {
            LocalPosition = localPosition;
        }
    }
}