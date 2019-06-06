using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.RTS_GameTool
{
    public abstract class KaiTool_BasicPlatform : MonoBehaviour
    {
        [Header("GridLayout")]
        [SerializeField]
        protected KaiTool_GridLayOut m_gridLayout;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_selected;
        [SerializeField]
        private UnityEvent m_unselected;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        private Color m_gizmosColor = Color.blue;
        [SerializeField]
        private float m_gizmosSize = 1f;


        public Vector3 GetFloorGridPoint(Vector3 point)
        {
            var gridPoint = m_gridLayout.GetFloorGridPoint(point);
            return gridPoint;
        }

        public Vector3 GetNearestGridPoint(Vector3 point)
        {
            var nearestPoint = m_gridLayout.GetNearestGridPoint(point);
            return nearestPoint;
        }



        private GameObject m_cursor;

        public Transform Origin
        {
            get
            {
                return m_gridLayout.m_origin;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                var width = m_gridLayout.Width;
                var length = m_gridLayout.Length;
                var right = m_gridLayout.m_origin.right;
                var forward = m_gridLayout.m_origin.forward;
                var x_unit = m_gridLayout.m_x_Unit;
                var y_unit = m_gridLayout.m_y_Unit;
                for (int i = 0; i < m_gridLayout.m_row + 1; i++)
                {
                    Gizmos.DrawLine(m_gridLayout.m_origin.position + forward * y_unit * i,
                        m_gridLayout.m_origin.position + right * width + forward * y_unit * i);
                }
                for (int i = 0; i < m_gridLayout.m_column + 1; i++)
                {
                    Gizmos.DrawLine(m_gridLayout.m_origin.position + right * x_unit * i,
                        m_gridLayout.m_origin.position + forward * length + right * x_unit * i);
                }
            }
        }
        private void FixedUpdate()
        {
            // Detect();
        }
        private void Detect()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                var pos = m_gridLayout.GetFloorGridPoint(hit.point);
                if (!m_cursor)
                {
                    m_cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Cursor"), pos, transform.rotation);
                }
                else
                {
                    m_cursor.transform.position = pos;
                }
            }
            else
            {
                if (m_cursor)
                {
                    DestroyImmediate(m_cursor);
                }
            }
        }

        public void OnSelected()
        {
            if (m_selected != null)
            {
                m_selected.Invoke();
            }
        }

        public void OnUnselected()
        {
            if (m_unselected != null)
            {
                m_unselected.Invoke();
            }
        }


    }
}