using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Geometry
{
    [Serializable]
    public abstract class KaiTool_BasicGraphy
    {
        public static Vector3 GetPerpendicularVector(Vector3 vector)
        {
            Vector3 result;
            if (vector != Vector3.one)
            {
                result = Vector3.Cross(Vector3.one, vector).normalized;
            }
            else
            {
                result = Vector3.Cross(Vector3.up, vector).normalized;
            }
            return result;
        }
        public KaiTool_BasicGraphy() { }
        public abstract void DrawGizmos(Color color);
    }
}