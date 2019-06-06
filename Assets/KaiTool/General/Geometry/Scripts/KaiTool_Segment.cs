using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Segment : KaiTool_BasicGraphy
    {
        public Transform m_start;
        public Transform m_end;

        public KaiTool_Segment(Transform start, Transform end)
        {
            m_start = start;
            m_end = end;
        }
        public override void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(m_start.position, m_end.position);
        }
    }
}