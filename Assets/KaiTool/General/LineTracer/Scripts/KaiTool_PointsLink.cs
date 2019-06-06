using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.LineTracer
{
    public class KaiTool_PointsLink : MonoBehaviour
    {
        [Header("Points")]
        [SerializeField]
        private Transform[] m_points;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        private Color m_gizmosColor = Color.green;
        [SerializeField]
        private float m_gizmosSize = 1f;

        public Transform[] Points
        {
            get
            {
                return m_points;
            }
        }
        public int PointsCount
        {
            get
            {
                return m_points.Length;
            }
        }

        public bool IsDrawGizmos
        {
            get
            {
                return m_isDrawGizmos;
            }

            set
            {
                m_isDrawGizmos = value;
            }
        }

        public Vector3 GetPosition(int index)
        {
            return m_points[index].position;
        }
        public Vector3[] GetPositions()
        {
            var positions = new Vector3[m_points.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = m_points[i].position;
            }
            return positions;
        }

        private void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                for (int i = 0; i < m_points.Length; i++)
                {
                    Gizmos.DrawWireSphere(m_points[i].position, 0.05f * m_gizmosSize);
                }
                if (m_points.Length > 1)
                {
                    for (int i = 0; i < m_points.Length - 1; i++)
                    {
                        Gizmos.DrawLine(m_points[i].position, m_points[i + 1].position);
                    }
                }
            }
        }
    }
}