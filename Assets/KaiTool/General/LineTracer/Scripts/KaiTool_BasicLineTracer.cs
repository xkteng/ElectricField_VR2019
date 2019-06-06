using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.LineTracer
{
    public abstract class KaiTool_BasicLineTracer : MonoBehaviour
    {
        [Header("Lines")]
        [SerializeField]
        protected Material m_material;
        [SerializeField]
        protected Color m_color = Color.white;
        [SerializeField]
        protected float m_width = 0.1f;
        [SerializeField]
        protected KaiTool_PointsLink[] m_links;
        [Header("Auto")]
        [SerializeField]
        private bool m_isAutoCreate = true;
        [SerializeField]
        private bool m_isAutoDestroy = true;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_createLine;
        [SerializeField]
        private UnityEvent m_updateLine;
        [SerializeField]
        private UnityEvent m_destroyLine;
        [Header("Twinkle")]
        [SerializeField]
        private float m_duration = 0.5f;
        [SerializeField]
        private float m_times = 3f;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;

        protected GameObject m_objectCreated;
        private Coroutine m_twinkleCoroutine;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            if (m_material == null)
            {
                m_material = Resources.Load<Material>("Materials/Red");
            }
        }
        private void InitEvent() { }


        private void OnEnable()
        {
            if (m_isAutoCreate)
            {
                OnCreateLine();
            }
        }

        private void OnDisable()
        {
            if (m_isAutoDestroy)
            {
                OnDestroyLine();
            }
        }

        public virtual void OnCreateLine()
        {
            if (m_createLine != null)
            {
                m_createLine.Invoke();
            }
        }
        public virtual void OnUpdateLine()
        {
            if (m_updateLine != null)
            {
                m_updateLine.Invoke();
            }
        }
        public virtual void OnDestroyLine()
        {
            if (m_destroyLine != null)
            {
                m_destroyLine.Invoke();
            }
        }
        public void Twinkle()
        {
            if (m_twinkleCoroutine == null)
            {
                m_twinkleCoroutine = StartCoroutine(TwinkleEnumerator());
            }
        }

        private IEnumerator TwinkleEnumerator()
        {
            var wait = new WaitForSeconds(m_duration / 2f);
            if (m_objectCreated == null)
            {
                OnCreateLine();
            }
            for (int i = 0; i < m_times; i++)
            {
                m_objectCreated.SetActive(true);
                yield return wait;
                m_objectCreated.SetActive(false);
                yield return wait;
            }
            OnDestroyLine();
            m_twinkleCoroutine = null;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                print("Twinkle");
                Twinkle();
            }
        }
        private void OnValidate()
        {
            foreach (var item in m_links)
            {
                item.IsDrawGizmos = m_isDrawGizmos;
            }
        }
    }
}