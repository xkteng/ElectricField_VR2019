using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Track : KaiTool_BasicGraphy
    {
        public Transform[] m_trackPoints;
        public float m_pointSize = 0.1f;

        public KaiTool_Track(Transform[] trackPoints, float pointSize)
        {
            m_trackPoints = trackPoints;
            m_pointSize = pointSize;
        }
        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            for (int i = 0; i < m_trackPoints.Length - 1; i++)
            {
                Gizmos.DrawSphere(m_trackPoints[i].position, m_pointSize);
                Gizmos.DrawLine(m_trackPoints[i].position, m_trackPoints[i + 1].position);
            }
            Gizmos.DrawSphere(m_trackPoints[m_trackPoints.Length - 1].position, m_pointSize);
        }
    }
}