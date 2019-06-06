using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Machinery
{
    public class KaiTool_PlaneConstraint : KaiTool_BasicConstraint
    {
        [SerializeField]
        protected KaiTool_Plane m_plane;

        protected override void DrawGizmos()
        {
            if (m_plane != null)
            {
                m_plane.DrawGizmos(m_gizmosColor);
            }
        }

        protected override void ResetPosition()
        {
            try
            {
                var vec = transform.position - m_plane.m_origin.position;
                var projectionOnNormal = Vector3.Project(vec, m_plane.Normal);
                transform.position = transform.position - projectionOnNormal;
            }
            catch (NullReferenceException e) {
            }
        }
    }
}