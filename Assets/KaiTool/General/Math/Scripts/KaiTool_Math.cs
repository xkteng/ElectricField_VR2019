using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Math
{
    public static class KaiTool_Math
    {
        #region DERIVATIVE
        public static float GetDerivative(Func<float, float> f, float x, float deltaX)
        {
            var f0 = f(x);
            var f1 = f(x + deltaX);
            var d = (f1 - f0) / deltaX;
            return d;
        }
        public static float GetDerivative(Func<float, float> f, float x)
        {
            return GetDerivative(f, x, 0.00001f);
        }

        #endregion
        #region VECTOR3
        public static Vector3 GetScaledVector3(Vector3 vector, Vector3 scale)
        {
            var scaledVector = new Vector3(vector.x * scale.x, vector.y * scale.y, vector.z * scale.z);
            return scaledVector;
        }
        public static Vector3 GetRelativeVector3(Vector3 vector, Transform parent)
        {
            var relativeVector = vector.x * parent.right + vector.y * parent.up + vector.z * parent.forward;
            return relativeVector;
        }
        #endregion
    }
}
