using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Geometry
{
    [Serializable]
    public class KaiTool_Circle : KaiTool_BasicGraphy
    {
        /// <summary>
        /// The transform.forward of center is circle's normal.
        /// </summary>
        public Transform m_center;
        public float m_radius;
        public Vector3 Normal {
            get {
                return m_center.forward;
            }
        }
        public KaiTool_Circle(Transform center,float radius){
            m_center = center;
            m_radius = radius;
        }
        public override void DrawGizmos(Color color)
        {
            if (m_center!=null) {
                Gizmos.color = color;
                var step = 64;
                var arcLength = (2 * Mathf.PI / step) * m_radius;
                for (int i = 0; i < step; i++)
                {
                    var startPoint = m_center.position + (Quaternion.AngleAxis(-360f/step*i, m_center.transform.forward) * m_center.transform.up) * m_radius;
                    var dir = Quaternion.AngleAxis(-180f / step * (2*i+1),m_center.transform.forward) * m_center.transform.right;
                    var endPoint = startPoint + dir * arcLength;
                    Gizmos.DrawLine(startPoint,endPoint);
                }
            }
        }
    }
}