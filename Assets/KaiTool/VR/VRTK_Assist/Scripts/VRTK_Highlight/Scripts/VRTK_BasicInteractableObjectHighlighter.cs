using UnityEngine;
using VRTK;
using System;

namespace KaiTool.VR.VRTK_Assistant
{
    public struct HighlighterEventArgs
    {

    }
    public interface IInteractableObjectHighlighter
    {
        VRTK_InteractableObject InterObject { get; }
        Action<UnityEngine.Object, HighlighterEventArgs> Highlighted { get; }
        Action<UnityEngine.Object, HighlighterEventArgs> Unhighlighted { get; }
    }
    [RequireComponent(typeof(VRTK_InteractableObject))]
    public abstract class VRTK_BasicInteractableObjectHighlighter : MonoBehaviour, IInteractableObjectHighlighter
    {
        [Header("HighlightedFIelds", order = 1)]
        [SerializeField]
        protected bool m_isOnWhenTouched = false;
        [SerializeField]
        protected bool m_isOnWhenGrabbed = false;
        [SerializeField]
        protected bool m_isOnWhenUsed = false;
        protected VRTK_InteractableObject m_interObject;
        protected event Action<UnityEngine.Object, HighlighterEventArgs> m_highlighted;
        protected event Action<UnityEngine.Object, HighlighterEventArgs> m_unhighlighted;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        public VRTK_InteractableObject InterObject
        {
            get
            {
                return m_interObject;
            }
        }

        public Action<UnityEngine.Object, HighlighterEventArgs> Highlighted
        {
            get
            {
                return m_highlighted;
            }
        }

        public Action<UnityEngine.Object, HighlighterEventArgs> Unhighlighted
        {
            get
            {
                return m_unhighlighted;
            }
        }

        protected virtual void OnHighlighted(UnityEngine.Object sender, HighlighterEventArgs e)
        {
            if (m_highlighted != null)
            {
                m_highlighted(sender, e);
            }
        }
        protected virtual void OnUnhighlighted(UnityEngine.Object sender, HighlighterEventArgs e)
        {
            if (m_unhighlighted != null)
            {
                m_unhighlighted(sender, e);
            }
        }

        private void InitVar()
        {
            m_interObject = GetComponent<VRTK_InteractableObject>();
        }
        private void InitEvent()
        {
            if (m_isOnWhenTouched)
            {
                m_interObject.InteractableObjectTouched += (sender, e) =>
                {
                    OnHighlighted(this, new HighlighterEventArgs());
                };
                m_interObject.InteractableObjectUntouched += (sender, e) =>
                {
                    OnUnhighlighted(this, new HighlighterEventArgs());
                };
            }
            if (m_isOnWhenGrabbed)
            {
                m_interObject.InteractableObjectGrabbed += (sender, e) =>
                {
                    OnHighlighted(this, new HighlighterEventArgs());
                };
                m_interObject.InteractableObjectUngrabbed += (sender, e) =>
                {
                    OnUnhighlighted(this, new HighlighterEventArgs());
                };
            }
            if (m_isOnWhenUsed)
            {
                m_interObject.InteractableObjectUsed += (sender, e) =>
                {
                    OnHighlighted(this, new HighlighterEventArgs());
                };
                m_interObject.InteractableObjectUnused += (sender, e) =>
                {
                    OnUnhighlighted(this, new HighlighterEventArgs());
                };
            }
        }


    }

}