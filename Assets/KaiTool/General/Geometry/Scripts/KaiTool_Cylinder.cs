using System;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Cylinder : KaiTool_BasicGraphy
    {
        public KaiTool_Line m_axis;
        public float m_radius = 1;

        public KaiTool_Cylinder(KaiTool_Line axis, float radius)
        {
            this.m_axis = axis;
            this.m_radius = radius;
        }

        public override void DrawGizmos(Color color)
        {
            var direction = m_axis.Direction;
            var up = KaiTool_BasicGraphy.GetPerpendicularVector(direction);
            if (m_axis != null)
            {
                var origin = m_axis.m_point0;
                var terminal = m_axis.m_point1;
                var circle0 = new KaiTool_SimpleCircle(origin.position, m_axis.Direction, m_radius);
                circle0.DrawGizmos(color);
                var circle1 = new KaiTool_SimpleCircle(terminal.position, m_axis.Direction, m_radius);
                circle1.DrawGizmos(color);
                var step = 8;
                for (int i = 0; i < step; i++)
                {
                    var startPoint = origin.position + Quaternion.AngleAxis((360f / step * i), direction) * up * m_radius;
                    var endPoint = terminal.position + Quaternion.AngleAxis((360f / step * i), direction) * up
                        * m_radius;
                    Gizmos.color = color;
                    Gizmos.DrawLine(startPoint, endPoint);

                }
            }
        }
    }
}