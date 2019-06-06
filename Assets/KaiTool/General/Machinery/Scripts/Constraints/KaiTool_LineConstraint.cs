using KaiTool.Geometry;
using System;
using UnityEngine;
namespace KaiTool.Machinery
{
    public class KaiTool_LineConstraint : KaiTool_BasicConstraint
    {
        [Header("StraightLine")]
        [SerializeField]
        private KaiTool_Line m_line;

        protected override void DrawGizmos()
        {
            if(m_line != null) {
                m_line.DrawGizmos(m_gizmosColor);
            }
        }

        protected override void ResetPosition()
        {
            try {
                var point = m_line.m_point0;
                var dir = transform.position - point.position;
                transform.position = point.position + Vector3.Project(dir, m_line.Direction);
            }
            catch (NullReferenceException e) {

            }
        }
    }
}