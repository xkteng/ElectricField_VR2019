using UnityEngine;
using KaiTool.Geometry;
using UnityEngine.Events;
using System;

namespace KaiTool.ElectricCircuit
{
    public struct FuseEventArgs { }
    public abstract class KaiTool_BasicFuse : KaiTool_BasicCircuitElement
    {

        [Header("Switch")]
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input;
        [SerializeField]
        private bool m_isWorking = true;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_blew;
        [SerializeField]
        private UnityEvent m_recovered;
        public Action<System.Object, FuseEventArgs> m_blewEventHandle;
        public Action<System.Object, FuseEventArgs> m_recoveredEventHandle;
        protected override void Init()
        {
            base.Init();
        }
        public void OnBlew(FuseEventArgs e)
        {
            if (m_isWorking)
            {
                m_isWorking = false;
                if (m_blew != null)
                {
                    m_blew.Invoke();
                }
                if (m_blewEventHandle != null)
                {
                    m_blewEventHandle(this, e);
                }
            }
        }
        public void OnRecovered(FuseEventArgs e)
        {
            if (!m_isWorking)
            {
                m_isWorking = true;
                if (m_recovered != null)
                {
                    m_recovered.Invoke();
                }
                if (m_recoveredEventHandle != null)
                {
                    m_recoveredEventHandle(this, e);
                }
            }
        }


        protected override void DrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if (m_isWorking)
            {
                Gizmos.DrawCube(transform.position, new Vector3(0.3f, 1f, 0.3f) * m_gizmosSize);
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, new Vector3(0.3f, 1f, 0.3f) * m_gizmosSize);
            }

            if (m_input != null)
            {
                Gizmos.color = m_gizmosColor;
                DrawGizmosWire(m_input.transform,m_wayPoints);
            }
            Gizmos.color = m_gizmosColor;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f * m_gizmosSize, new Vector3(0.4f, 0.4f, 0.4f) * m_gizmosSize);
            Gizmos.DrawWireCube(transform.position + Vector3.up * -0.5f * m_gizmosSize, new Vector3(0.4f, 0.4f, 0.4f) * m_gizmosSize);
        }

        protected override bool JudgeElectrification()
        {
            try
            {
                if (m_input.IsElectrified > 0 && m_isWorking)
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