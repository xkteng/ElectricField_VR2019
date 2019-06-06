//***********************************************
//By Kai
//Description:KaiTool_Utility
//
//
//***********************************************
using UnityEngine;

namespace KaiTool.Utilities
{
    public static class KaiTool_Utility
    {
        public static T GetSurvivalType<T>(this Transform transform) where T : Component
        {
            var t = transform.GetComponent<T>();
            if (t == null)
            {
                t = transform.gameObject.AddComponent<T>();
            }
            return t;
        }
    }
}