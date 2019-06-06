using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_SimpleCircle : KaiTool_BasicGraphy
    {
        public Vector3 m_center;
        public Vector3 m_normal;
        public float m_radius;
        private KaiTool_SimpleCircle()
        {
        }
        public KaiTool_SimpleCircle(Vector3 center, Vector3 normal, float radius)
        {
            m_center = center;
            m_normal = normal;
            m_radius = radius;
        }

        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            if (m_center != null)
            {
                Gizmos.color = color;
                var step = 64;
                var arcLength = (2 * Mathf.PI / step) * m_radius;
                Vector3 up, right;
                up = KaiTool_BasicGraphy.GetPerpendicularVector(m_normal);
                right = Vector3.Cross(up, m_normal).normalized;
                for (int i = 0; i < step; i++)
                {
                    var startPoint = m_center + (Quaternion.AngleAxis(-360f / step * i, m_normal) * up) * m_radius;
                    var dir = Quaternion.AngleAxis(-180f / step * (2 * i + 1), m_normal) * right;
                    var endPoint = startPoint + dir * arcLength;
                    Gizmos.DrawLine(startPoint, endPoint);
                }
            }
        }
    }
}