using System;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Sphere : KaiTool_BasicGraphy
    {
        public Transform m_sphereCenter;
        public float m_radius;

        public KaiTool_Sphere(Transform center, float radius)
        {
            m_sphereCenter = center;
            m_radius = radius;
        }
        public override void DrawGizmos(Color color)
        {
            if (m_sphereCenter != null)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(m_sphereCenter.position, m_radius);
            }
        }
    }
}