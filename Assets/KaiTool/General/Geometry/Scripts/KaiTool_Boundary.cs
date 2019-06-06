using System;
using UnityEngine;

namespace KaiTool.Geometry
{
    [Flags]
    public enum BoundaryDirection
    {
        Forward = 1,
        Backward = 2,
        Leftward = 4,
        Rightward = 8,
        Upward = 16,
        Downward = 32
    }
    [Serializable]
    public class KaiTool_Boundary : KaiTool_BasicGraphy, ICloneable
    {
        [SerializeField]
        private Transform m_center;
        [SerializeField]
        private float m_forward = 1f;
        [SerializeField]
        private float m_backward = 1f;
        [SerializeField]
        private float m_leftward = 1f;
        [SerializeField]
        private float m_rightward = 1f;
        [SerializeField]
        private float m_upward = 1f;
        [SerializeField]
        private float m_downward = 1f;

        private KaiTool_Boundary() { }
        public KaiTool_Boundary(float[] lengthes)
        {
            m_forward = lengthes[0];
            m_backward = lengthes[1];
            m_leftward = lengthes[2];
            m_rightward = lengthes[3];
            m_upward = lengthes[4];
            m_downward = lengthes[5];
        }
        public Vector3[] Vertices
        {
            get
            {

                var points = new Vector3[8];
                points[0] = m_center.position + m_center.right * (-m_leftward) + m_center.up * (-m_downward) + m_center.forward * (-m_backward);
                points[1] = m_center.position + m_center.right * (-m_leftward) + m_center.up * (-m_downward) + m_center.forward * (m_forward);
                points[2] = m_center.position + m_center.right * (m_rightward) + m_center.up * (-m_downward) + m_center.forward * (m_forward);
                points[3] = m_center.position + m_center.right * (m_rightward) + m_center.up * (-m_downward) + m_center.forward * (-m_backward);
                points[4] = m_center.position + m_center.right * (-m_leftward) + m_center.up * (m_upward) + m_center.forward * (-m_backward);
                points[5] = m_center.position + m_center.right * (-m_leftward) + m_center.up * (m_upward) + m_center.forward * (m_forward);
                points[6] = m_center.position + m_center.right * (m_rightward) + m_center.up * (m_upward) + m_center.forward * (m_forward);
                points[7] = m_center.position + m_center.right * (m_rightward) + m_center.up * (m_upward) + m_center.forward * (-m_backward);
                return points;
            }
        }
        public Vector3 GetVertex(int index)
        {
            switch (index)
            {
                case 0:
                    return m_center.position + m_center.right * (-m_leftward) + m_center.up * (-m_downward) + m_center.forward * (-m_backward);
                case 1:
                    return m_center.position + m_center.right * (-m_leftward) + m_center.up * (-m_downward) + m_center.forward * (m_forward);
                case 2:
                    return m_center.position + m_center.right * (m_rightward) + m_center.up * (-m_downward) + m_center.forward * (m_forward);
                case 3:
                    return m_center.position + m_center.right * (m_rightward) + m_center.up * (-m_downward) + m_center.forward * (-m_backward);
                case 4:
                    return m_center.position + m_center.right * (-m_leftward) + m_center.up * (m_upward) + m_center.forward * (-m_backward);
                case 5:
                    return m_center.position + m_center.right * (-m_leftward) + m_center.up * (m_upward) + m_center.forward * (m_forward);
                case 6:
                    return m_center.position + m_center.right * (m_rightward) + m_center.up * (m_upward) + m_center.forward * (m_forward);
                case 7:
                    return m_center.position + m_center.right * (m_rightward) + m_center.up * (m_upward) + m_center.forward * (-m_backward);
                default:
                    return Vector3.zero;
            }
        }
        public void ChangeBoundary(BoundaryDirection direction, float delta)
        {
            switch (direction)
            {
                case BoundaryDirection.Forward:
                    m_forward = m_forward + delta;
                    break;
                case BoundaryDirection.Backward:
                    m_backward = m_backward + delta;
                    break;
                case BoundaryDirection.Leftward:
                    m_leftward = m_leftward + delta;
                    break;
                case BoundaryDirection.Rightward:
                    m_rightward = m_rightward + delta;
                    break;
                case BoundaryDirection.Upward:
                    m_upward = m_upward + delta;
                    break;
                case BoundaryDirection.Downward:
                    m_downward = m_downward + delta;
                    break;
            }
        }
        public float Forward
        {
            get
            {
                return m_forward;
            }

            set
            {
                m_forward = value;
            }
        }
        public float Backward
        {
            get
            {
                return m_backward;
            }

            set
            {
                m_backward = value;
            }
        }
        public float Leftward
        {
            get
            {
                return m_leftward;
            }

            set
            {
                m_leftward = value;
            }
        }
        public float Rightward
        {
            get
            {
                return m_rightward;
            }

            set
            {
                m_rightward = value;
            }
        }
        public float Upward
        {
            get
            {
                return m_upward;
            }

            set
            {
                m_upward = value;
            }
        }
        public float Downward
        {
            get
            {
                return m_downward;
            }

            set
            {
                m_downward = value;
            }
        }
        public Transform Center
        {
            get
            {
                return m_center;
            }

            set
            {
                m_center = value;
            }
        }
        public Vector3 GeometricalCenter
        {
            get
            {
                return m_center.position
                    + m_center.right * (m_rightward - m_leftward) / 2
                    + m_center.forward * (m_forward - m_backward) / 2
                    + m_center.up * (m_upward - m_downward) / 2;
            }
        }

        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            var points = Vertices;
            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(points[i], points[(i + 1) % 4]);
                Gizmos.DrawLine(points[i], points[(i + 4)]);
            }
            for (int i = 4; i < 8; i++)
            {
                Gizmos.DrawLine(points[i], points[(i + 1) % 4 + 4]);
            }

        }

        public object Clone()
        {
            KaiTool_Boundary bounday = new KaiTool_Boundary(new float[] { m_forward, m_backward, m_leftward, m_rightward, m_upward, m_downward });
            return bounday;
        }
    }
}
