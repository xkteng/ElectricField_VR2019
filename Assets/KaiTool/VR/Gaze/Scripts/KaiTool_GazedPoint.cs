using KaiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.VR
{
    public class KaiTool_GazedPoint : KaiTool_BasicGazedPoint
    {
        [Header("Attaches")]
        [SerializeField]
        private bool m_useHighlight = true;
        [SerializeField]
        private bool m_useloadingTip = true;
        [SerializeField]
        private bool m_useToolTip = true;
        [SerializeField]
        private string m_tooltip_Text = "Default";
        [SerializeField]
        private int m_tooltip_fontSize = 50;

        protected override void Awake()
        {
            base.Awake();
            InitVar();
        }
        private void InitVar()
        {
            if (m_useHighlight)
            {
                var highlight_Attach = transform.GetSurvivalType<KaiTool_GazedPoint_HighlightAttach>();
            }
            if (m_useloadingTip)
            {
                var loadingTip_Attach = transform.GetSurvivalType<KaiTool_GazedPoint_LoadingTipAttach>();
            }
            if (m_useToolTip)
            {
                var toolTip_Attach = transform.GetSurvivalType<KaiTool_GazedPoint_ToolTipAttach>();
                toolTip_Attach.SetArgs(m_tooltip_Text, m_tooltip_fontSize);
            }
        }
    }
}