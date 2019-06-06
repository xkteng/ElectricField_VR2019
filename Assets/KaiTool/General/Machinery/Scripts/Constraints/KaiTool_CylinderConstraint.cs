using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Machinery
{
    public class KaiTool_CylinderConstraint : KaiTool_BasicConstraint
    {
        [SerializeField]
        protected KaiTool_Cylinder m_cylinder;

        protected override void DrawGizmos()
        {
            if (m_cylinder != null)
            {
                m_cylinder.m_axis.DrawGizmos(m_gizmosColor);
                m_cylinder.DrawGizmos(m_gizmosColor);
            }
        }
        protected override void ResetPosition()
        {
            try
            {
                var vector = transform.position - m_cylinder.m_axis.m_point0.position;
                var h = Vector3.Project(vector, m_cylinder.m_axis.Direction);
                var l = vector - h;
                transform.position = m_cylinder.m_axis.m_point0.position + h + l.normalized * m_cylinder.m_radius;
            }
            catch (NullReferenceException e)
            {

            }
        }
    }
}