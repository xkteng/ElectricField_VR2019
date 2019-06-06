using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    public class KaiTool_MultiWayJoint : KaiTool_BasicCircuitElement
    {
        [SerializeField]
        private KaiTool_BasicCircuitElement[] m_inputs;

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < m_inputs.Length; i++)
            {
                Gizmos.DrawLine(m_inputs[i].transform.position, transform.position);
            }
            Gizmos.color = m_gizmosColor;
            if (m_isElectrified == 1)
            {
                Gizmos.DrawCube(transform.position, Vector3.one * m_gizmosSize);
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, Vector3.one * m_gizmosSize);
            }
        }

        protected override bool JudgeElectrification()
        {
            var temp = false;
            for (int i = 0; i < m_inputs.Length; i++)
            {
                if (m_inputs[i].IsElectrified > 0)
                {
                    temp = true;
                    break;
                }
            }
            return temp;
        }

        protected override void ResetCurrent()
        {
            var sum = 0f;
            for (int i = 0; i < m_inputs.Length; i++)
            {
                sum += m_inputs[i].Current;
            }
            m_current = sum;
        }
    }
}