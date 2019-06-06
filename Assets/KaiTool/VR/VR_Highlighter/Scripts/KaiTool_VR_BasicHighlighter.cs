using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.VR.Highlighter
{
    public abstract class KaiTool_VR_BasicHighlighter : MonoBehaviour, IVRHighlighter
    {
        [SerializeField]
        private bool m_isHighlighted = false;
        [SerializeField]
        private UnityEvent m_onHighlighted = new UnityEvent();
        [SerializeField]
        private UnityEvent m_onUnhighlighted = new UnityEvent();

        public bool IsHighlighted => m_isHighlighted;

        public UnityEvent OnHighlighted => m_onHighlighted;

        public UnityEvent OnUnhighlighted => m_onUnhighlighted;

        public virtual void Highlight()
        {
            m_isHighlighted = true;
            m_onHighlighted.Invoke();
        }
        public virtual void Unhighlight()
        {
            m_isHighlighted = false;
            m_onUnhighlighted.Invoke();
        }
        protected virtual void Awake()
        {

        }
    }

    public interface IVRHighlighter
    {
        bool IsHighlighted { get; }
        UnityEvent OnHighlighted { get; }
        UnityEvent OnUnhighlighted { get; }
        void Highlight();
        void Unhighlight();

    }


}