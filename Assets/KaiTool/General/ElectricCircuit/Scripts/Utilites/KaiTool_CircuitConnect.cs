using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    [ExecuteInEditMode]
    public class KaiTool_CircuitConnect : KaiTool_BasicCircuitElement
    {
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input;
        protected override void DrawGizmos()
        {
            if (m_input != null)
            {
                DrawGizmosWire(m_input.transform,m_wayPoints);
            }
            Gizmos.color = m_gizmosColor;
            if (m_isElectrified == 0)
            {
                Gizmos.DrawWireSphere(transform.position, m_gizmosSize);
            }
            else {
                Gizmos.DrawSphere(transform.position, m_gizmosSize);
            }
        }

        protected override bool JudgeElectrification()
        {
            try
            {
                if (m_input.IsElectrified > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }
        }

        protected override void ResetCurrent()
        {
            m_current = m_input.Current;
        }

    }
}