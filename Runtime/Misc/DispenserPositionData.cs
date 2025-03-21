using System;
using UnityEngine;

namespace Marx.Utilities
{
    public enum DirectionSign { negative, positive, none }
    public enum DirectionAxis { x = 0, y = 2, none = -1 }

    [Serializable]
    public class DispenserPositionData
    {
        public DirectionSign signDir;
        public DirectionAxis axisDir;
        public Vector3 offset;

        public DispenserPositionData(DirectionSign signDir, DirectionAxis axisDir)
        {
            this.signDir = signDir;
            this.axisDir = axisDir;
        }
    }
}