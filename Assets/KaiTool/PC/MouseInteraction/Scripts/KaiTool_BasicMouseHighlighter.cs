using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KaiTool.PC.MouseInteraction
{

    enum EnumHighlightType
    {
        Hovered,
        LeftButtonPressed,
        LeftButtonClicked,
        RightButtonPressed,
        RightButtonClicked,
        DraggedOnPlane
    }
    public struct HighlighterEventArgs
    {
        public float m_duration;
    }
    [RequireComponent(typeof(KaiTool_MouseInteractiveObject))]
    public abstract class KaiTool_BasicMouseHighlighter : MonoBehaviour
    {
        [Header("Highlighter")]
        [SerializeField]
        private EnumHighlightType m_highlightType = EnumHighlightType.Hovered;
        protected KaiTool_MouseInteractiveObject m_interactiveObject;

        protected void Awake()
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
            m_interactiveObject = GetComponent<KaiTool_MouseInteractiveObject>();
        }
        private void InitEvent()
        {
            switch (m_highlightType)
            {
                case EnumHighlightType.Hovered:
                    m_interactiveObject.HoveredIn += (sender, e) =>
                    {
                        OnHighlighted();
                    };
                    m_interactiveObject.HoveredOut += (sender, e) =>
                    {
                        OnUnhighlighted();
                    };
                    break;
                case EnumHighlightType.LeftButtonPressed:
                    m_interactiveObject.LeftButtonPressed += (sender, e) =>
                    {
                        OnHighlighted();
                    };
                    m_interactiveObject.LeftButtonReleased += (sender, e) =>
                    {
                        OnUnhighlighted();
                    };
                    break;
                case EnumHighlightType.LeftButtonClicked:
                    m_interactiveObject.LeftButtonReleased += (sender, e) =>
                    {
                        var args = new HighlighterEventArgs();
                        args.m_duration = 0.3f;
                        OnHighlighted(args);
                    };
                    break;
                case EnumHighlightType.RightButtonPressed:
                    m_interactiveObject.RightButtonPressed += (sender, e) =>
                    {
                        OnHighlighted();
                    };
                    m_interactiveObject.RightButtonReleased += (sender, e) =>
                    {
                        OnUnhighlighted();
                    };
                    break;
                case EnumHighlightType.RightButtonClicked:
                    m_interactiveObject.RightButtonReleased += (sender, e) =>
                    {
                        var args = new HighlighterEventArgs();
                        args.m_duration = 0.3f;
                        OnHighlighted(args);
                    };
                    break;
                case EnumHighlightType.DraggedOnPlane:
                    m_interactiveObject.HoveredIn += (sender, e) =>
                    {
                        OnHighlighted();
                    };
                    m_interactiveObject.LeftButtonPressed += (sender, e) =>
                    {
                        OnHighlighted();
                    };
                    m_interactiveObject.HoveredOut += (sender, e) =>
                    {
                        if (!m_interactiveObject.IsLeftButtonSelected) {
                            OnUnhighlighted();
                        }
                    };
                    m_interactiveObject.LeftButtonReleased += (sender, e) =>
                    {
                        OnUnhighlighted();
                    };
                    break;
            }
        }
        public Action<System.Object, HighlighterEventArgs> m_highlighted;
        public Action<System.Object, HighlighterEventArgs> m_unhighlighted;
        protected virtual void OnHighlighted(HighlighterEventArgs e)
        {
            if (e.m_duration > 0f)
            {
                Invoke("OnUnhighlighted", e.m_duration);

            }
            if (m_highlighted != null)
            {
                m_highlighted(this, e);
            }
        }
        protected virtual void OnUnhighlighted(HighlighterEventArgs e)
        {
            if (m_unhighlighted != null)
            {
                m_unhighlighted(this, e);
            }
        }
        private void OnHighlighted()
        {
            OnHighlighted(new HighlighterEventArgs());
        }
        private void OnUnhighlighted()
        {
            OnUnhighlighted(new HighlighterEventArgs());
        }
    }
}
