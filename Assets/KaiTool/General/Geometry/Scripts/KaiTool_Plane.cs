using System;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Plane : KaiTool_BasicGraphy
    {
        public Transform m_origin;
        public Vector3 Normal
        {
            get
            {
                return m_origin.forward;
            }
        }
        public override void DrawGizmos(Color color)
        {
            if (m_origin != null)
            {
                Gizmos.color = color;
                var size = m_origin.transform.localScale.x;
                var rectangle = new KaiTool_Rectangle(m_origin,10f*size,10f*size);
                rectangle.DrawGizmos(color);
            }
        }
    }
}