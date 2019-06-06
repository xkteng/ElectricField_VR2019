using KaiTool.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.RTS_GameTool
{
    public class KaiTool_MouseDragger_OnPlatform_WithinRange : KaiTool_MouseDragger_OnPlatform
    {
        [SerializeField]
        private Transform m_anchor;
        [SerializeField]
        private float m_range = 3f;
        protected override bool IsDraggable(Vector3 targetPosition)
        {
            var vector = m_anchor.position - targetPosition;
            var project = Vector3.ProjectOnPlane(vector, transform.up);
            var projectLength = project.magnitude;
            if (projectLength < m_range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            var circle = new KaiTool_Circle(m_anchor, m_range);
            circle.DrawGizmos(Color.green);
        }

    }
}