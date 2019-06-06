using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    public abstract class KaiTool_BasicCircuitDevice : KaiTool_BasicCircuitElement
    {
        [Header("Connect", order = 0)]
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input;
        protected override void DrawGizmos()
        {
            if (m_input != null)
            {
                DrawGizmosWire(m_input.transform,m_wayPoints);
            }
        }
        protected override bool JudgeElectrification()
        {
            if (m_input != null)
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
            else
            {
                return m_isElectrified > 0 ? true : false;
            }
        }

        protected override void ResetCurrent()
        {
            // throw new System.NotImplementedException();
        }
    }
}