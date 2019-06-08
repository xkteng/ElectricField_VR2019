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
        //private float m_minIntensity;
        //private float m_maxIntensity;
        private float m_minDistance;
        private float m_maxDistance;
        private float m_sign;
        private Transform m_generateEletric;
        private List<Transform> m_eletricsList;

        private void Awake()
        {
            m_lineRender = GetComponent<LineRenderer>();
        }

        public void RenderLine(IntensityFunction i_func)
        {
            SetStart(transform.position);
            List<Vector3> points_list = new List<Vector3>();
            Vector3 point = m_start;
            var distance_LastRecord = (m_start-m_generateEletric.position).magnitude;
            Vector3 intensity_Last_Record = i_func(m_start);
            Vector3 lastStep = Vector3.zero;
            while (IsContinueRender(point))
            {
                //times++;
                points_list.Add(point);
                var step = intensity_Last_Record.normalized * m_deltaLength;
                point += step*m_sign;
                //var dis_temp = (point-m_generateEletric.position).magnitude;
                intensity_Last_Record = i_func(point);
                //continue;

                //if (dis_temp>=distance_LastRecord)
                //{
                //    distance_LastRecord = dis_temp;
                //    intensity_Last_Record = i_func(point);
                //    continue;
                //}
                //else
                //{
                //    point -= step * 2;
                //    distance_LastRecord = (point-m_generateEletric.position).magnitude;
                //    intensity_Last_Record = i_func(point);
                //    continue;
                //}
            }
            m_lineRender.positionCount = points_list.Count;
            m_lineRender.SetPositions(points_list.ToArray());
        }
        private bool IsContinueRender(Vector3 pos) {
            var min = float.MaxValue;
            var max = float.MinValue;
            foreach (var item in m_eletricsList)
            {
                var dis = (pos - item.position).magnitude;
                min = min <= dis ? min : dis;
                max = max >=dis ? max : dis;
            }
            if (min>=m_minDistance&&max<=m_maxDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetDelta(float delta)
        {
            m_deltaLength = delta;
        }
        public void SetStart(Vector3 start)
        {
            m_start = start;
        }

        public void Show()
        {
            m_lineRender.enabled = true;
        }

        public void Hide()
        {
            m_lineRender.enabled = false;
        }

        public void SetMinDistance(float minDistance)
        {
            m_minDistance = minDistance;
        }

        public void SetMaxDistance(float maxDistance)
        {
            m_maxDistance = maxDistance;
        }

        public void SetElectricsList(List<Transform> electricsList)
        {
            m_eletricsList = electricsList;
        }

        public void SetGenertateElectric(Transform electric)
        {
            m_generateEletric = electric;
        }

        public void SetSign(float sign)
        {
            m_sign = sign;
        }
    }
}