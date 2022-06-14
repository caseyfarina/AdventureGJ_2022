using UnityEngine;

namespace OccaSoftware.Buto
{
    internal static class ButoCommon
    {
        internal static void CheckLightCount(int c, int max, Object o)
        {
            if (c > max)
                Debug.LogWarning("Too many enabled Buto Lights in Scene (" + c + ", Max 8). Remove or disable excess to avoid undefined behavior. \nThere are " + c + " Buto fog-enabled additional lights in scene, which is more than the maximum of 8 fog-enabled additional lights in scene. \nRemove excessive fog-enabled lights from scene to avoid undefined behavior.", o);
        }
    }
}
