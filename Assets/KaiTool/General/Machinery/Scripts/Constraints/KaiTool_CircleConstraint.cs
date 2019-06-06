using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Machinery
{
    public class KaiTool_CircleConstraint : KaiTool_BasicConstraint
    {
        [SerializeField]
        protected KaiTool_Circle m_circle;
        protected override void DrawGizmos()
        {
            if (m_circle != null)
            {
                m_circle.DrawGizmos(m_gizmosColor);
            }
        }

        protected override void ResetPosition()
        {
            try
            {
                // throw new System.NotImplementedException();
                var vector = transform.position - m_circle.m_center.position;
                var vectorProjected = Vector3.ProjectOnPlane(vector, m_circle.Normal).normalized;
                transform.position = m_circle.m_center.position + vectorProjected * m_circle.m_radius;
            }
            catch (NullReferenceException e) {

            }
        }
    }
}
