using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KaiTool.SplineInterpolation
{
    public abstract class KaiTool_BasicSpline
    {
        [SerializeField]
        protected Vector3[] m_originalPoints;
        public Vector3[] OriginalPoints
        {
            get
            {
                return m_originalPoints;
            }
            set
            {
                if (value.Length < 4)
                {
                    throw new Exception("The number of points belonging to the spline can not be less than 4.");
                }
                m_originalPoints = value;
            }
        }
        public KaiTool_BasicSpline(Vector3[] originalPoints)
        {
            if (originalPoints != null)
            {
                if (originalPoints.Length < 4)
                {
                    throw new Exception("The number of points belonging to the spline can not be less than 4.");
                }
                m_originalPoints = originalPoints;
            }
        }
        public abstract List<Vector3> GetSmoothSpline(int length);
        public static List<Vector3> GetBezierSpline(Vector3[] originalPoints, int totalStep) {
            var tempSpline = new KaiTool_BezierSpline(originalPoints);
            return tempSpline.GetSmoothSpline(totalStep);
        }
        public static List<Vector3> GetSegmentedBezierSpline(Vector3[] originalPoints ,int totalStep ) {
            var tempSpline = new KaiTool_SegmentedBezierSpline(originalPoints);
            return tempSpline.GetSmoothSpline(totalStep);
        }
        public static List<Vector3> GetSegmentedBezierSpline(Vector3[] originalPoints, int totalStep,int segmentLength)
        {
            var tempSpline = new KaiTool_SegmentedBezierSpline(originalPoints);
            return tempSpline.GetSmoothSpline(totalStep,segmentLength);
        }

        public static List<Vector3> GetLagrangeInterpolationSpline(Vector3[] originalPoints,int totalStep) {
            var tempSpline = new KaiTool_LagrangeInterpolationSpline(originalPoints);
            return tempSpline.GetSmoothSpline(totalStep);
        }
    }


    [Serializable]
    public class KaiTool_BezierSpline : KaiTool_BasicSpline
    {
        public KaiTool_BezierSpline(Vector3[] originalPoints) : base(originalPoints)
        {

        }
        public override List<Vector3> GetSmoothSpline(int totalStep)
        {
            var pointList = new List<Vector3>();
            for (int j = 0; j < totalStep; j++)
            {

                var finalPos = GetFittingPoint(m_originalPoints, j, totalStep);

                pointList.Add(finalPos);
            }
            return pointList;
        }
        private Vector3[] GetDeflated(Vector3[] originalPoints, int step, int totalStep)
        {
            var length = originalPoints.Length;
            if (length == 0)
            {
                throw new Exception("The length of originalPoints can not be zero.");
            }
            else if (length == 1)
            {
                return originalPoints;
            }
            else if (length >= 2)
            {
                var temp = new Vector3[length - 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = Vector3.Lerp(originalPoints[i + 1], originalPoints[i], (float)step / totalStep);
                }
                return temp;
            }
            else {
                return null;
            }
        }
        private Vector3 GetFittingPoint(Vector3[] originalPoints, int step, int totalStep)
        {
            var temp = originalPoints;
            while (temp.Length>1) {
                temp = GetDeflated(temp, step, totalStep);
            }
            return temp[0];

        }
    }
}