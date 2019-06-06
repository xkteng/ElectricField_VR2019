using KaiTool.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.GizmosUtilities
{
    public class Gizmos_Test : MonoBehaviour
    {
        private static Color s_gizmos_color = Color.green;
        [Header("Bound")]
        [SerializeField]
        private Vector3 m_boundCenter = Vector3.zero;
        [SerializeField]
        private Vector3 m_boundSize = Vector3.one;
        [Header("Box")]
        [SerializeField]
        private Transform m_boxCenter;
        [SerializeField]
        private Vector3 m_boxSize = Vector3.one;
        [Header("Track")]
        [SerializeField]
        private Transform[] m_trackPoints;
        [SerializeField]
        private float m_pointSize = 0.1f;

        private void OnDrawGizmos()
        {
            KaiTool_Gizmos.DrawBox(m_boundCenter, m_boundSize, s_gizmos_color);
            KaiTool_Gizmos.DrawGizmos(new KaiTool_Box(m_boxCenter, m_boxSize), s_gizmos_color);
            KaiTool_Gizmos.DrawTrack(m_trackPoints,m_pointSize,s_gizmos_color);
        }
    }
}