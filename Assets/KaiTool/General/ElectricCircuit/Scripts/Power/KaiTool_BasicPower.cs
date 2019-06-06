using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    public struct PowerEventArgs
    {

    }
    [ExecuteInEditMode]
    public abstract class KaiTool_BasicPower : KaiTool_BasicCircuitElement
    {
        [Header("Power")]
        [SerializeField]
        protected int m_isOn = 0;
        [SerializeField]
        private float m_currentOfPower;
        protected override void DrawGizmos()
        {
            //throw new System.NotImplementedException();
        }
        protected override bool JudgeElectrification()
        {
            return m_isOn > 0 ? true : false;
        }
        protected override void ResetCurrent()
        {
            //throw new System.NotImplementedException();
            m_current = m_currentOfPower;
        }

        public event Action<System.Object, PowerEventArgs> m_powerOnEventHandle;
        public event Action<System.Object, PowerEventArgs> m_powerOffEventHandle;


        public void PowerOn(PowerEventArgs e)
        {
            if (m_isOn == 0)
            {
                if (m_powerOnEventHandle != null)
                {
                    m_powerOnEventHandle(this, e);
                }
                m_isOn = 1;
            }
        }
        public void PowerOff(PowerEventArgs e)
        {
            if (m_isOn != 0)
            {
                if (m_powerOffEventHandle != null)
                {
                    m_powerOffEventHandle(this, e);
                }
                m_isOn = 0;
            }
        }
    }
}