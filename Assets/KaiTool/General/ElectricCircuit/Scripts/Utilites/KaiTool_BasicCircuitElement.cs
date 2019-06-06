using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using KaiTool.Geometry;

namespace KaiTool.ElectricCircuit
{

    public struct CircuitElementEventArgs
    {
        public float m_current;
    }
    public abstract class KaiTool_BasicCircuitElement : MonoBehaviour
    {
        [SerializeField]
        //[HideInInspector]
        protected int m_isElectrified = 0;
        [SerializeField]
        [HideInInspector]
        private float m_intervalTime = 0.03f;
        [Header("Gizmos")]
        [SerializeField]
        protected float m_gizmosSize = 1f;
        [SerializeField]
        protected EnumGizmosType m_gizmosType = EnumGizmosType.Default;
        [SerializeField]
        protected Color m_gizmosColor = Color.green;
        [SerializeField]
        protected Transform[] m_wayPoints;

        protected float m_current;
        protected float m_voltage;
        private Coroutine m_judgeElectrificationCoroutine;
        [Header("Event")]
        [SerializeField]
        private UnityEvent m_electrified;
        [SerializeField]
        private UnityEvent m_unelectrified;
        public Action<System.Object, CircuitElementEventArgs> m_electrifiedEventHandle;
        public Action<System.Object, CircuitElementEventArgs> m_unelectrifiedEventHandle;
        public int IsElectrified
        {
            get
            {
                return m_isElectrified;
            }
        }
        public float Current
        {
            get
            {
                return m_current;
            }
        }
        public float Voltage
        {
            get
            {
                return m_voltage;
            }
        }

        public UnityEvent Electrified
        {
            get
            {
                return m_electrified;
            }

            set
            {
                m_electrified = value;
            }
        }

        public UnityEvent Unelectrified
        {
            get
            {
                return m_unelectrified;
            }

            set
            {
                m_unelectrified = value;
            }
        }

        protected virtual void OnElectrified(CircuitElementEventArgs e)
        {
            if (m_isElectrified == 0)
            {
                m_isElectrified = 1;
                ResetCurrent();
                if (m_electrified != null)
                {
                    m_electrified.Invoke();
                }
                if (m_electrifiedEventHandle != null)
                {
                    m_electrifiedEventHandle(this, e);
                }
            }
        }
        protected virtual void OnUnElectrified(CircuitElementEventArgs e)
        {
            if (m_isElectrified > 0)
            {
                m_isElectrified = 0;
                m_current = 0f;
                if (m_unelectrified != null)
                {
                    m_unelectrified.Invoke();
                }
                if (m_unelectrifiedEventHandle != null)
                {
                    m_unelectrifiedEventHandle(this, e);
                }
            }
        }

        private void Awake()
        {
            Init();
        }
        protected virtual void OnEnable()
        {
            //Init();
            m_judgeElectrificationCoroutine = StartCoroutine(JudgeElectrifiedEnumerator());
        }
        protected virtual void Init()
        {
        }

        protected abstract bool JudgeElectrification();
        protected abstract void ResetCurrent();
        protected abstract void DrawGizmos();
        private void OnDrawGizmos()
        {
            DrawGizmos();
        }
        protected virtual void DrawGizmosWire(Transform input, Transform[] wayPoints)
        {
            Gizmos.color = Color.green;
            switch (m_gizmosType)
            {
                case EnumGizmosType.Default:
                    Gizmos.DrawLine(input.transform.position, transform.position);
                    break;
                case EnumGizmosType.Custom:
                    if (wayPoints.Length > 0)
                    {
                        Gizmos.DrawLine(input.transform.position, wayPoints[0].position);
                        for (int i = 0; i < wayPoints.Length - 1; i++)
                        {
                            Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);
                        }
                        Gizmos.DrawLine(wayPoints[wayPoints.Length - 1].position, transform.position);
                    }
                    else
                    {
                        Gizmos.DrawLine(input.transform.position, transform.position);
                    }
                    break;
            }
        }
        private IEnumerator JudgeElectrifiedEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                if (JudgeElectrification())
                {
                    if (m_isElectrified == 0)
                    {
                        var args = new CircuitElementEventArgs();
                        OnElectrified(args);
                    }
                }
                else
                {
                    if (m_isElectrified > 0)
                    {
                        var args = new CircuitElementEventArgs();
                        OnUnElectrified(args);
                    }
                }
                yield return wait;
            }
        }

    }
}