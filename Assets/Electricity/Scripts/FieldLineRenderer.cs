using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Electricity
{

    public class FieldLineRenderer : MonoBehaviour, IFieldLineRenderer
    {
        private LineRenderer m_lineRender;
        private float m_deltaLength;
        private float m_deltaPotential;
        private Vector3 m_start;
        private float m_minIntensity;
        private float m_maxIntensity;

        private void Awake()
        {
            m_lineRender = GetComponent<LineRenderer>();
        }

        public void RenderLine(IntensityFunction i_func, PotentialFunction p_func)
        {
            SetStart(transform.position);
            List<Vector3> points_list = new List<Vector3>();
            Vector3 point = m_start;
            float potentialAbs_Last_Record = Mathf.Abs(p_func(m_start));
            Vector3 intensity_Last_Record = i_func(m_start);
            Vector3 lastStep = Vector3.zero;

            while (intensity_Last_Record.magnitude > m_minIntensity && intensity_Last_Record.magnitude < m_maxIntensity)
            {
                points_list.Add(point);
                var b = false;
                var step = intensity_Last_Record.normalized * m_deltaLength;
                point += step;
                var temp_potentialAbs = Mathf.Abs(p_func(point));

                if (temp_potentialAbs > potentialAbs_Last_Record)
                {
                    b = true;
                    step *= -1f;
                    if (Vector3.Dot(step,lastStep)<0f)
                    {
                        break;
                    }
                    point += step * 2;
                }
                if (b)
                {
                    if (Vector3.Dot(step, lastStep) < 0f)
                    {
                        break;
                    }
                    potentialAbs_Last_Record = Mathf.Abs(p_func(point));
                }
                else
                {
                    potentialAbs_Last_Record = temp_potentialAbs;
                }
                lastStep = step;
                intensity_Last_Record = i_func(point);
            }
            m_lineRender.positionCount = points_list.Count;
            m_lineRender.SetPositions(points_list.ToArray());
        }

        public void SetDelta(float delta)
        {
            m_deltaLength = delta;
        }
        public void SetStart(Vector3 start)
        {
            m_start = start;
        }
        public void SetMinimumIntensity(float min)
        {
            m_minIntensity = min;
        }
        public void SetMaximumIntensity(float max)
        {
            m_maxIntensity = max;
        }

        public void Show()
        {
            m_lineRender.enabled = true;
        }

        public void Hide()
        {
            m_lineRender.enabled = false;
        }
    }
}