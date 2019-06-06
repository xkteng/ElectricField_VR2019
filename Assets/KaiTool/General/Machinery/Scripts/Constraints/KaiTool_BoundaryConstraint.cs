using UnityEngine;
using KaiTool.Geometry;
namespace KaiTool.Machinery
{
    public class KaiTool_BoundaryConstraint : KaiTool_BasicConstraint
    {
        [Header("BoundaryContraint")]
        [SerializeField]
        private Transform m_target;
        [SerializeField]
        private KaiTool_Boundary m_boundary;
        protected override void DrawGizmos()
        {
            m_boundary.DrawGizmos(m_gizmosColor);
        }

        protected override void ResetPosition()
        {
            var vector = m_target.position - m_boundary.Center.position;
            var center = m_boundary.Center;
            var z = Vector3.Dot(vector, center.forward);
            var y = Vector3.Dot(vector, center.up);
            var x = Vector3.Dot(vector, center.right);

            if (z >= 0)
            {
                z = z < m_boundary.Forward ? z : m_boundary.Forward;
            }
            else
            {
                z = z > -m_boundary.Backward ? z : -m_boundary.Backward;
            }


            if (y >= 0)
            {
                y = y < m_boundary.Upward ? y : m_boundary.Upward;
            }
            else
            {
                y = y > -m_boundary.Downward ? y : -m_boundary.Downward;
            }


            if (x >= 0)
            {
                x = x < m_boundary.Rightward ? x : m_boundary.Rightward;
            }
            else
            {
                x = x > -m_boundary.Leftward ? x : -m_boundary.Leftward;
            }

            m_target.position = m_boundary.Center.position + center.forward * z + center.up * y + center.right * x;
        }
    }
}