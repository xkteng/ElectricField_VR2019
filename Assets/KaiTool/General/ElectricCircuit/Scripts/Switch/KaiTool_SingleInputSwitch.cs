using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.ElectricCircuit
{
    public struct SwitchEventArgs
    {
        public float m_delay;
    }
    [ExecuteInEditMode]
    public sealed class KaiTool_SingleInputSwitch : KaiTool_BasicCircuitElement
    {
        [Header("Switch")]
        [SerializeField]
        private KaiTool_BasicCircuitElement m_input;
        [SerializeField]
        private int m_isOn = 1;
        [Header("Event")]
        [SerializeField]
        private UnityEvent m_turnedOn;
        [SerializeField]
        private UnityEvent m_turnedOff;

        private Action<System.Object, SwitchEventArgs> m_turnedOnEventHandle;
        private Action<System.Object, SwitchEventArgs> m_turnedOffEventHandle;

        private Coroutine m_switchCoroutine;

        private int m_isOriginalOn;

        public int IsOn
        {
            get
            {
                return m_isOn;
            }
        }

        public UnityEvent TurnedOn
        {
            get
            {
                return m_turnedOn;
            }

            set
            {
                m_turnedOn = value;
            }
        }

        public UnityEvent TurnedOff
        {
            get
            {
                return m_turnedOff;
            }

            set
            {
                m_turnedOff = value;
            }
        }

        protected override void DrawGizmos()
        {

            if (m_input != null)
            {
                DrawGizmosWire(m_input.transform, m_wayPoints);
            }
            Gizmos.color = m_gizmosColor;
            if (m_isOn > 0)
            {
                Gizmos.DrawWireCube(transform.position, Vector3.one * m_gizmosSize);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position + Vector3.up * 0.05f * m_gizmosSize, new Vector3(0.3f, 0.5f, 0.3f) * m_gizmosSize);
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, Vector3.one * m_gizmosSize);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position + Vector3.up * 0.75f * m_gizmosSize, new Vector3(0.3f, 0.5f, 0.3f) * m_gizmosSize);
            }
        }
        protected override bool JudgeElectrification()
        {
            try
            {
                if (m_isOn > 0 && m_input.IsElectrified > 0)
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
        public void TurnOn(SwitchEventArgs e)
        {
            if (m_switchCoroutine == null)
            {
                m_switchCoroutine = StartCoroutine(TurnOnEnumerator(e));
            }
        }

        private IEnumerator TurnOnEnumerator(SwitchEventArgs e)
        {
            var delay = e.m_delay;
            if (delay != 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            if (m_isOn == 0)
            {
                m_isOn = 1;
                if (m_turnedOn != null)
                {
                    m_turnedOn.Invoke();
                }
                if (m_turnedOnEventHandle != null)
                {
                    m_turnedOnEventHandle(this, e);
                }
            }
            m_switchCoroutine = null;
        }
        [ContextMenu("TurnOn")]
        public void TurnOn()
        {
            var args = new SwitchEventArgs();
            TurnOn(args);
        }
        public void TurnOff(SwitchEventArgs e)
        {
            if (m_switchCoroutine == null)
            {
                m_switchCoroutine = StartCoroutine(TurnOffEnumerator(e));
            }
        }
        private IEnumerator TurnOffEnumerator(SwitchEventArgs e)
        {
            var delay = e.m_delay;
            if (delay != 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            if (m_isOn == 1)
            {
                m_isOn = 0;
                if (m_turnedOff != null)
                {
                    m_turnedOff.Invoke();
                }
                if (m_turnedOffEventHandle != null)
                {
                    m_turnedOffEventHandle(this, e);
                }
            }
            m_switchCoroutine = null;
        }
        [ContextMenu("TurnOff")]
        public void TurnOff()
        {
            var args = new SwitchEventArgs();
            TurnOff(args);
        }
        public void Toggle()
        {
            if (m_isOn > 0)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
        protected override void ResetCurrent()
        {
            m_current = m_input.Current;
        }

        protected override void Init()
        {
            base.Init();
        }
        private void InitVar()
        {
            m_isOriginalOn = m_isOn;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            /*
            if (m_isOriginalOn == 0)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
            */
        }

    }
}