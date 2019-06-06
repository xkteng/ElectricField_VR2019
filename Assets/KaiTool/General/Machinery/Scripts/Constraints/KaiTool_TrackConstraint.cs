using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Machinery
{
    [ExecuteInEditMode]
    public class KaiTool_TrackConstraint : KaiTool_BasicConstraint
    {
        [Header("Track")]
        [SerializeField]
        protected KaiTool_Track m_track;
        [SerializeField]
        protected int m_searchRange = 3;
        private int m_currentIndex = -1;
        private float m_currentDistance=Mathf.Infinity;
        protected override void DrawGizmos()
        {
            if (m_track != null)
            {
                m_track.DrawGizmos(m_gizmosColor);
            }
        }

        protected override void ResetPosition()
        {
            try
            {
                if (m_currentIndex == -1)
                {
                    m_currentIndex = GetIndexOfNearestTrackPoint();
                    transform.position = m_track.m_trackPoints[m_currentIndex].position;
                }
                else
                {
                    m_currentIndex = GetIndexOfNeareastTrackPoint(m_currentIndex, m_searchRange);
                    transform.position = m_track.m_trackPoints[m_currentIndex].position;
                }
            }
            catch (NullReferenceException e) {

            }

        }
        private int GetIndexOfNearestTrackPoint()
        {
            var min = Mathf.Infinity;
            var index = -1;
            for (int i = 0; i < m_track.m_trackPoints.Length; i++)
            {
                var dis = (transform.position - m_track.m_trackPoints[i].position).magnitude;
                if (dis < min)
                {
                    index = i;
                    min = dis;
                }
            }
           // distance = min;
            return index;
        }
        private int GetIndexOfNeareastTrackPoint(int[] indices)
        {
            var min = Mathf.Infinity;
            var index = -1;
            for (int i = 0; i < indices.Length; i++)
            {
                var dis = (transform.position - m_track.m_trackPoints[indices[i]].position).magnitude;
                if (dis < min)
                {
                    index = indices[i];
                    min = dis;
                }
            }
            return index;
        }
        private int GetIndexOfNeareastTrackPoint(int targetIndex, int range)
        {
            var indicesInRange = new int[2 * range + 1];
            for (int i = 0; i < indicesInRange.Length; i++)
            {
                var temp = (targetIndex - range * 2 + 1+i) % m_track.m_trackPoints.Length;
                temp = temp >= 0 ? temp : temp+m_track.m_trackPoints.Length;
                indicesInRange[i] = temp;
            }
            var index = GetIndexOfNeareastTrackPoint(indicesInRange);
            return index;
        }
    }
}