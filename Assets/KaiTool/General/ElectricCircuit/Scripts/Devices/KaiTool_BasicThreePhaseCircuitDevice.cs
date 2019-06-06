using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.ElectricCircuit
{
    public class KaiTool_BasicThreePhaseCircuitDevice : KaiTool_BasicCircuitElement
    {
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input_0;
        [SerializeField]
        private Transform[] m_wayPoints_0;
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input_1;
        [SerializeField]
        private Transform[] m_wayPoints_1;
        protected override bool JudgeElectrification()
        {
            if (m_input_0.IsElectrified == 1 || m_input_1.IsElectrified == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void ResetCurrent()
        {
            //throw new System.NotImplementedException();
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = m_gizmosColor;
            if (m_input_0 != null)
            {
                DrawGizmosWire(m_input_0.transform, m_wayPoints_0);
            }
            if (m_input_1 != null)
            {
                DrawGizmosWire(m_input_1.transform, m_wayPoints_1);
            }
            if (m_isElectrified == 0)
            {
                Gizmos.DrawWireSphere(transform.position, 0.1f * m_gizmosSize);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, 0.1f * m_gizmosSize);
            }
        }
    }
}