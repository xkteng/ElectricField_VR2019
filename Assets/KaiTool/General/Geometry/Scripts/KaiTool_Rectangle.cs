using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Rectangle : KaiTool_BasicGraphy
    {
        public Transform m_center;
        public float m_width;
        public float m_height;
        private KaiTool_Rectangle() { }
        public KaiTool_Rectangle(Transform center, float width, float height)
        {
            m_center = center;
            m_width = width;
            m_height = height;
        }
        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            if (m_center != null)
            {
                var centerPos = m_center.position;
                var points = new Vector3[4];
                points[0] = centerPos - m_center.right * m_width / 2 + m_center.up * m_height / 2;
                points[1] = centerPos + m_center.right * m_width / 2 + m_center.up * m_height / 2;
                points[2] = centerPos + m_center.right * m_width / 2 - m_center.up * m_height / 2;
                points[3] = centerPos - m_center.right * m_width / 2 - m_center.up * m_height / 2;
                for (int i = 0; i < points.Length; i++)
                {
                    Gizmos.DrawLine(points[i], points[(i + 1) % 4]);
                }
            }
        }
    }
}