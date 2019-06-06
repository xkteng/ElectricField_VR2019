using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.PC.MouseInteraction
{
    public class KaiTool_MouseHighlighter : KaiTool_BasicMouseHighlighter
    {
        private enum HighlighterSelection
        {
            Default,
            Custom
        }
        [SerializeField]
        private Color m_highlighterColor = Color.white;
        [SerializeField]
        private HighlighterSelection m_selection = HighlighterSelection.Default;
        [SerializeField]
        private Highlighter[] m_highlighers;
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            switch (m_selection)
            {
                case HighlighterSelection.Default:
                    m_highlighers = GetComponentsInChildren<Highlighter>();
                    break;
                case HighlighterSelection.Custom:
                    break;
            }
        }
        private void InitEvent()
        {
            m_highlighted += (sender, e) =>
            {
                foreach (var item in m_highlighers)
                {
                    item.ConstantOn(m_highlighterColor);
                }
            };
            m_unhighlighted += (sender, e) =>
            {
                foreach (var item in m_highlighers)
                {
                    item.ConstantOff();
                }
            };
        }
    }
}