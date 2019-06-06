using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.KaiTween
{
    public struct ObjectTransitorEventArgs
    {
        public float m_duration;
    }
    public class KaiTool_ObjectTransitor : MonoBehaviour
    {

        [Header("Time")]
        [SerializeField]
        protected float m_delay = 0f;
        [SerializeField]
        protected float m_duration = 1f;
        [Header("Gizmos")]
        [SerializeField]
        protected bool m_isDrawGizmos = true;
        [SerializeField]
        protected Color m_gizmosColor = Color.yellow;
        [SerializeField]
        protected float m_gizmosSize = 1f;

        private void Awake()
        {
            Init();
        }
        protected virtual void Init() { }
        protected Coroutine m_TransitionCoroutine;
        private Action<System.Object, ObjectTransitorEventArgs> m_startIn;
        private Action<System.Object, ObjectTransitorEventArgs> m_endIn;
        private Action<System.Object, ObjectTransitorEventArgs> m_startOut;
        private Action<System.Object, ObjectTransitorEventArgs> m_endOut;

        public Action<object, ObjectTransitorEventArgs> StartIn
        {
            get
            {
                return m_startIn;
            }
            set
            {
                m_startIn = value;
            }
        }

        public Action<object, ObjectTransitorEventArgs> EndIn
        {
            get
            {
                return m_endIn;
            }
            set
            {
                m_endIn = value;
            }
        }

        public Action<object, ObjectTransitorEventArgs> StartOut
        {
            get
            {
                return m_startOut;
            }
            set
            {
                m_startOut = value;
            }
        }

        public Action<object, ObjectTransitorEventArgs> EndOut
        {
            get
            {
                return m_endOut;
            }
            set
            {
                m_endOut = value;
            }
        }

        public void OnStartIn()
        {
            var args = new ObjectTransitorEventArgs();
            OnStartIn(args);
        }

        public void OnStartOut()
        {
            var args = new ObjectTransitorEventArgs();
            OnStartOut(args);
        }

        public virtual void OnStartIn(ObjectTransitorEventArgs e)
        {
            if (m_startIn != null)
            {
                m_startIn(this, e);
            }
        }
        public virtual void OnEndIn(ObjectTransitorEventArgs e)
        {
            if (m_endIn != null)
            {
                m_endIn(this, e);
            }
        }
        public virtual void OnStartOut(ObjectTransitorEventArgs e)
        {
            if (m_startOut != null)
            {
                m_startOut(this, e);
            }
        }
        public virtual void OnEndOut(ObjectTransitorEventArgs e)
        {
            if (m_endOut != null)
            {
                m_endOut(this, e);
            }
        }
        protected virtual void OnDrawGizmos()
        {

        }
    }
}