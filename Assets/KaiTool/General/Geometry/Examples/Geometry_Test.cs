using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Geometry
{
    public class Geometry_Test : MonoBehaviour
    {
        static Color s_gizmos_color = Color.green;
        [SerializeField]
        private KaiTool_Box m_box;
        [SerializeField]
        private KaiTool_Circle m_circle;
        [SerializeField]
        private KaiTool_Cylinder m_cylinder;
        [SerializeField]
        private KaiTool_Line m_line;
        [SerializeField]
        private KaiTool_Plane m_plane;
        [SerializeField]
        private KaiTool_Rectangle m_rectangle;
        [SerializeField]
        private KaiTool_SimpleBox m_simpleBox;
        [SerializeField]
        private KaiTool_SimpleCircle m_simpleCircle;
        [SerializeField]
        private KaiTool_Sphere m_sphere;
        [SerializeField]
        private KaiTool_Track m_track;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            m_box.DrawGizmos(s_gizmos_color);
            m_circle.DrawGizmos(s_gizmos_color);
            m_cylinder.DrawGizmos(s_gizmos_color);
            m_line.DrawGizmos(s_gizmos_color);
            m_plane.DrawGizmos(s_gizmos_color);
            m_rectangle.DrawGizmos(s_gizmos_color);
            m_simpleBox.DrawGizmos(s_gizmos_color);
            m_simpleCircle.DrawGizmos(s_gizmos_color);
            m_sphere.DrawGizmos(s_gizmos_color);
            m_track.DrawGizmos(s_gizmos_color);
        }
    }
}