using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ElectricCircuit
{
    public class KaiTool_Motor : KaiTool_CircuitDevice
    {
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }

        private void InitEvent()
        {
            m_electrifiedEventHandle += (sender, e) =>
            {
                // print("Motor On");
            };
            m_unelectrifiedEventHandle += (sender, e) =>
            {
                // print("Motor Off");
            };
        }

    }
}