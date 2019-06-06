using System;
using UnityEngine;

namespace KaiTool.RTS_GameTool
{
    [Serializable]
    public class KaiTool_GridLayOut
    {
        public Transform m_origin;
        public float m_x_Unit = 1f;
        public float m_y_Unit = 1f;
        public int m_row = 10;
        public int m_column = 10;
        public float Width
        {
            get
            {
                return m_x_Unit * m_column;
            }
        }
        public float Length
        {
            get
            {
                return m_y_Unit * m_row;
            }
        }
        public Vector3 GetFloorGridPoint(Vector3 point)
        {
            var x = Mathf.FloorToInt(Vector3.Dot((point - m_origin.position), m_origin.right) / m_x_Unit);
            var y = Mathf.FloorToInt(Vector3.Dot((point - m_origin.position), m_origin.forward) / m_y_Unit);
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (x > m_column)
            {
                x = m_column;
            }
            if (y > m_row)
            {
                y = m_row;
            }
                return m_origin.position
                    + x * m_x_Unit * m_origin.right
                    + y * m_y_Unit * m_origin.forward;
        }
        public Vector3 GetNearestGridPoint(Vector3 point)
        {
            var project_X = Vector3.Dot((point - m_origin.position), m_origin.right);
            var project_Y = Vector3.Dot((point - m_origin.position), m_origin.forward);
            var x = Mathf.FloorToInt(project_X / m_x_Unit);
            var y = Mathf.FloorToInt(project_Y / m_y_Unit);
            var remainder_x = (project_X % m_x_Unit) / m_x_Unit;
            var remainder_y = (project_Y % m_y_Unit) / m_y_Unit;

            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }

            if (remainder_x > 0.5f)
            {
                x++;
            }
            if (remainder_y > 0.5f)
            {
                y++;
            }

            if (x > m_column)
            {
                x = m_column;
            }
            if (y > m_row)
            {
                y = m_row;
            }

            return m_origin.position
                + x * m_x_Unit * m_origin.right
                + y * m_y_Unit * m_origin.forward;

        }
        public Vector3 GetGridPoint(int x, int y)
        {
            if (x < 0 | y < 0)
            {
                throw new Exception("coordinates can not be less than zero.");
            }
            var gridPoint = m_origin.position + m_origin.right * x * m_x_Unit + m_origin.forward * y * m_y_Unit;
            return gridPoint;
        }



    }
}