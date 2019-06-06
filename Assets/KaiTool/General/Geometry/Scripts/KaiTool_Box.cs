using System;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Box : KaiTool_BasicGraphy
    {
        public Transform m_center;
        public float m_width = 1f;
        public float m_length = 1f;
        public float m_height = 1f;

        public KaiTool_Box(Transform center, Vector3 size)
        {
            m_center = center;
            m_width = size.x;
            m_length = size.z;
            m_height = size.y;
        }
        private Vector3[] Vertices
        {
            get
            {
                var points = new Vector3[8];
                var forward = m_center.forward;
                var up = m_center.up;
                var right = m_center.right;
                points[0] = m_center.position - m_width / 2f * right - m_length / 2f * forward - m_height / 2f * up;
                points[1] = m_center.position - m_width / 2f * right + m_length / 2f * forward - m_height / 2f * up;
                points[2] = m_center.position + m_width / 2f * right + m_length / 2f * forward - m_height / 2f * up;
                points[3] = m_center.position + m_width / 2f * right - m_length / 2f * forward - m_height / 2f * up;
                points[4] = m_center.position - m_width / 2f * right - m_length / 2f * forward + m_height / 2f * up;
                points[5] = m_center.position - m_width / 2f * right + m_length / 2f * forward + m_height / 2f * up;
                points[6] = m_center.position + m_width / 2f * right + m_length / 2f * forward + m_height / 2f * up;
                points[7] = m_center.position + m_width / 2f * right - m_length / 2f * forward + m_height / 2f * up;
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
        public Vector3 GetProjectedDirection(Transform target)
        {
            var vec = target.position - m_center.position;
            var projected = new float[3];
            var directions = new Vector3[3] { m_center.right, m_center.up, m_center.forward };
            var edges = new float[3] { m_width, m_height, m_length };
            projected[0] = Vector3.Dot(vec, m_center.right);
            projected[1] = Vector3.Dot(vec, m_center.up);
            projected[2] = Vector3.Dot(vec, m_center.forward);
            for (int i = 0; i < projected.Length; i++)
            {
                var times = Mathf.Abs(projected[i] / (edges[i]));
                if (Mathf.Abs((projected[(i + 1) % 3]) / times) <= edges[(i + 1) % 3] && (Mathf.Abs(projected[(i + 2) % 3]) / times) <= edges[(i + 2) % 3])
                {
                    return directions[i] * Mathf.Sign(projected[i]);
                }
            }
            return Vector3.zero;
        }
    }
}