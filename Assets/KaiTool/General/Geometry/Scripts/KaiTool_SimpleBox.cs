using System;
using UnityEngine;

namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_SimpleBox : KaiTool_BasicGraphy
    {
        public Vector3 m_centerPosition;
        [SerializeField]
        private Vector3 m_forward;
        [SerializeField]
        private Vector3 m_up;
        public float m_width = 1f;
        public float m_length = 1f;
        public float m_height = 1f;
        public KaiTool_SimpleBox(Vector3 center, Vector3 size)
        {
            m_centerPosition = center;
            m_width = size.x;
            m_length = size.z;
            m_height = size.y;
            //InitializDirection
            m_forward = Vector3.forward;
            m_up = Vector3.up;
        }
        public Vector3 Forward
        {
            get
            {
                return m_forward.normalized;
            }
        }
        public Vector3 Up
        {
            get
            {
                return m_up.normalized;
            }
        }
        public Vector3 Right
        {
            get
            {
                return Vector3.Cross(m_up, m_forward).normalized;
            }
        }
        private Vector3[] Vertices
        {
            get
            {
                var points = new Vector3[8];
                var forward = Forward;
                var up = Up;
                var right = Right;
                points[0] = m_centerPosition - m_width / 2f * right - m_length / 2f * forward - m_height / 2f * up;
                points[1] = m_centerPosition - m_width / 2f * right + m_length / 2f * forward - m_height / 2f * up;
                points[2] = m_centerPosition + m_width / 2f * right + m_length / 2f * forward - m_height / 2f * up;
                points[3] = m_centerPosition + m_width / 2f * right - m_length / 2f * forward - m_height / 2f * up;
                points[4] = m_centerPosition - m_width / 2f * right - m_length / 2f * forward + m_height / 2f * up;
                points[5] = m_centerPosition - m_width / 2f * right + m_length / 2f * forward + m_height / 2f * up;
                points[6] = m_centerPosition + m_width / 2f * right + m_length / 2f * forward + m_height / 2f * up;
                points[7] = m_centerPosition + m_width / 2f * right - m_length / 2f * forward + m_height / 2f * up;
                return points;
            }
        }
        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            var ver = Vertices;
            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(ver[i], ver[(i + 1) % 4]);
                Gizmos.DrawLine(ver[i], ver[i + 4]);
            }
            for (int i = 4; i < 8; i++)
            {
                Gizmos.DrawLine(ver[i], ver[(i + 1) % 4 + 4]);
            }
        }
    }
}