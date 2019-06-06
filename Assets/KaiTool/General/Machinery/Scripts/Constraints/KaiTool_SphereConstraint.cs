using UnityEngine;
using KaiTool.Geometry;
using System;

namespace KaiTool.Machinery
{
    public class KaiTool_SphereConstraint : KaiTool_BasicConstraint
    {
        [Header("SphereConstraint")]
        [SerializeField]
        protected KaiTool_Sphere m_sphere;
        protected override void Init()
        {
            base.Init();
        }
        protected override void ResetPosition()
        {
            try
            {
                var centerPos = m_sphere.m_sphereCenter.position;
                var dir = transform.position - centerPos;
                transform.position = centerPos + dir.normalized * m_sphere.m_radius;
            }
            catch (NullReferenceException e)
            {

            }
        }

        protected override void DrawGizmos()
        {
            if (m_sphere != null)
            {
                m_sphere.DrawGizmos(m_gizmosColor);
            }
        }
    }
}