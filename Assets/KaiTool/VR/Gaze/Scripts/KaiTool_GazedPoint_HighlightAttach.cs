using KaiTool.Utilities;
using KaiTool.VR.Highlighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.VR
{
    public sealed class KaiTool_GazedPoint_HighlightAttach : MonoBehaviour
    {
        private KaiTool_BasicGazedPoint m_gazed;
        private KaiTool_VR_CopyHighlighter m_highlighter;
        private void Awake()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_gazed = GetComponent<KaiTool_BasicGazedPoint>();
            m_highlighter = transform.GetSurvivalType<KaiTool_VR_CopyHighlighter>();
        }
        private void InitEvent()
        {
            m_gazed.m_gazerEnter.AddListener((gazer) =>
             {
                 m_highlighter.Highlight();
             });
            m_gazed.m_gazerExit.AddListener((gazer) =>
            {
                m_highlighter.Unhighlight();
            });
        }
    }
}