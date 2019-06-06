using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.SplineInterpolation
{
    public class KaiTool_SegmentedBezierSpline : KaiTool_BasicSpline
    {
        public KaiTool_SegmentedBezierSpline(Vector3[] originalPoints) : base(originalPoints)
        {

        }
        public override List<Vector3> GetSmoothSpline(int totalSteps)
        {
            return GetSmoothSpline(totalSteps, 4);
        }
        public List<Vector3> GetSmoothSpline(int totalStep, int segmentLength)
        {
            var times = m_originalPoints.Length / segmentLength;
            var remainder = m_originalPoints.Length % segmentLength;
            var resultList = new List<Vector3>();
            var segmentStep = totalStep / times;
            for (int i = 0; i < remainder; i++)
            {
                resultList.Add(m_originalPoints[i]);
            }
            for (int i = 0; i < times; i++)
            {
                var tempPointArray = new Vector3[segmentLength];
                for (int j = 0; j < segmentLength; j++)
                {
                    tempPointArray[j] = m_originalPoints[remainder + (segmentLength-j-1) + i * segmentLength];
                }
                var tempList = GetBezierSpline(tempPointArray,segmentStep);
                resultList.AddRange(tempList);
            }
            return resultList;
        }
    }
}