using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiTool.Math;
namespace KaiTool.SplineInterpolation
{
    public class KaiTool_LagrangeInterpolationSpline : KaiTool_BasicSpline
    {
        public KaiTool_LagrangeInterpolationSpline(Vector3[] originalPoints) : base(originalPoints)
        {
        }

        public override List<Vector3> GetSmoothSpline(int length)
        {
            var delta = (m_originalPoints[m_originalPoints.Length - 1].x - m_originalPoints[0].x) / (length-1);
            var result = new List<Vector3>();
            for (int i = 0; i < length; i++)
            {
                var tempx = m_originalPoints[0].x + i * delta;
                var tempy = YMultiNomial(tempx);
                var tempz = ZMultiNomial(tempx);
                result.Add(new Vector3(tempx,tempy,tempz));
            }
            return result;
        }
        private float Function(float x)
        {
            var result = 1f;
            for (int i = 0; i < m_originalPoints.Length ; i++)
            {
                result *= (x - m_originalPoints[i].x);

            }
            return result;
        }
       
        private float GetDerivative(float x)
        {
            Func<float, float> yFunc = null;
            yFunc += Function;
            var dy = KaiTool_Math.GetDerivative(yFunc, x);
            return dy;
        }
       
        private float YMultiNomial(float x) {
            var result = 0f;
            for (int i = 0; i < m_originalPoints.Length; i++)
            {
                result += m_originalPoints[i].y * (Function(x)) / ( (x-m_originalPoints[i].x) * GetDerivative(m_originalPoints[i].x) );
            }
            return result;
        }
        private float ZMultiNomial(float x) {
            var result = 0f;
            for (int i = 0; i < m_originalPoints.Length; i++)
            {
                result += m_originalPoints[i].z * (Function(x) / ((x - m_originalPoints[i].x) * GetDerivative(m_originalPoints[i].x)));
            }
            return result;
        }
    }
}
