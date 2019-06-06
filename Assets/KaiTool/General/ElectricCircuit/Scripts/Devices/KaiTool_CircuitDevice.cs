using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    public class KaiTool_CircuitDevice : KaiTool_BasicCircuitDevice
    {
        protected override void DrawGizmos()
        {
            base.DrawGizmos();
            Gizmos.color = m_gizmosColor;
            if (m_isElectrified == 0)
            {
                Gizmos.DrawWireSphere(transform.position, 0.1f * m_gizmosSize);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, 0.1f * m_gizmosSize);
            }
        }
        private void OnDisable()
        {
            m_isElectrified = 0;
        }
    }
}