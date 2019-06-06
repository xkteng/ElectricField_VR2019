using System;
using UnityEngine;

namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Line : KaiTool_BasicGraphy
    {
        public Transform m_point0;
        public Transform m_point1;
        public KaiTool_Line(Transform point0, Transform point1)
        {
            this.m_point0 = point0;
            this.m_point1 = point1;
        }
        public Vector3 Direction
        {
            get
            {
                try
                {
                    return (m_point1.position - m_point0.position).normalized;
                }
                catch (NullReferenceException e)
                {
                    return Vector3.zero;
                }
            }
        }
        public override void DrawGizmos(Color color)
        {
            if (m_point0 != null && m_point1 != null)
            {
                Gizmos.color = color;
                var length = (m_point1.position - m_point0.position).magnitude;
                if (m_point0 != null && m_point1 != null)
                {
                    Gizmos.DrawRay(m_point0.position, Direction * 2 * length);
                    Gizmos.DrawRay(m_point0.position, -Direction * 1 * length);
                }
            }
        }
    }

}